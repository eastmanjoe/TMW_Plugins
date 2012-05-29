using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using TMW.SCL;
using TMW.SCL.MB;
using TMW.SCL.MB.Slave;
using TMW.SCL.ProtocolAnalyzer;

using ModbusSimulatorSlave;

namespace ModbusSimulatorSlave
{
  public partial class FormMBSimSlave : Form
  {
    public FormMBSimSlave()
    {
      InitializeComponent();
      
      this.cfg_mb_per_port.Value = 1;
      this.cfg_start_mb_addr.Value = 1;
      this.cfg_tcp_port.Value = 502;
    }
    

    
/// <summary>
/// Closes the channel and session
/// </summary>
    private void CloseSession()
    {
        if (this.MbSlaveSession != null)
          this.MbSlaveSession.CloseSession();
  
        this.MbSlaveSession = null;
    }
    
    
    
/// <summary>
/// Closes the channel
/// </summary>
    private void CloseChannel()
    {
      
      if (this.Channel != null)
        this.Channel.CloseChannel();

      this.Channel = null;
    }
    
    
    
/// <summary>
/// Open Channel
/// </summary>
/// <param name="port"></param>
/// <param name="total_ports"></param>
    private string OpenChannel(ushort port)
    {
        // if we are already connected disconnect
        if (this.Channel != null)
        {
          if (this.Channel.Name == "sMBSim_" + port)
          {
            CloseSession();
            CloseChannel();
          }          
        }
          
        try
        {
          Channel = new MBChannel(TMW_CHANNEL_OR_SESSION_TYPE.SLAVE);
          Channel.Type = WINIO_TYPE.TCP;
          Channel.WinTCPipPort = Convert.ToUInt16(port);
          Channel.WinTCPipAddress = IP;
          Channel.WinTCPmode = TCP_MODE.SERVER;
          Channel.Name = "sMBSim_" + port;
          Channel.Protocol = TMW_PROTOCOL.MB;
          Channel.OpenChannel();
          
          return Channel.Name;
        } 
        catch (Exception ex)
        {
          MessageBox.Show("Error Opening: " + ex.Message);
          Channel = null;
          Close();
          
          return null;
        }
    }
    
    

    private void OpenSession(string channel, ushort mb_addr)
    {
      // See if we are supposed to connect to an existing session
      if (this.SessionId != null)
      {
        this.MbSlaveSession = (SMBSession)TMWSession.LookupSession(Convert.ToUInt32(this.SessionId));
        if (this.MbSlaveSession != null)
        {
          this.Channel = (MBChannel)this.MbSlaveSession.Channel;
          this.Channel.SessionCloseEvent += new TMWChannel.SessionCloseEventDelegate(SessionCloseEvent);
        }
        else
        {
          throw new Exception("Unable to load Modbus Slave Simulator. Unable to find session.");
        }
      }
      else
      {
        try
        {
          MbSlaveSession = new SMBSession(Channel);
          MbSlaveSession.SlaveAddress = mb_addr;
          MbSlaveSession.Name = "sMBSim_" + mb_addr;
          MbSlaveSession.OpenSession();
        } 
        catch (Exception ex)
        {
          MessageBox.Show("Error Opening: " + ex.Message);
          Channel = null;
          MbSlaveSession = null;
          Close();
          
          return;
        }
      }
    }
    

    
    
/// <summary>
/// Determine to either open or close the session and channels
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
    private void SessionManagement(object sender, EventArgs e)
    {
      string channel_name;
      ushort tcp_port = Convert.ToUInt16(cfg_tcp_port.Value);
      ushort mb_addr_start = Convert.ToUInt16(cfg_start_mb_addr.Value);
      
      //Determine the direction of the state change
      if (this.open_mb_session.Checked)
      {
          channel_name = OpenChannel(tcp_port);
          
          for (int c = 0; c < cfg_mb_per_port.Value; c++)
          {
            OpenSession(channel_name, mb_addr_start);
            InitializeModbusMap();
            mb_addr_start += 1;
          }
      }
      else
      {
          CloseSession();
          CloseChannel();
      }
    }
    
    
    
/// <summary>
/// Gets the CSV filename to be imported into the database
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
    private void DataFileImport(object sender, EventArgs e)
    {
      //open fiale dialog box to selct .csv file
      OpenFileDialog fdlg = new OpenFileDialog();
      
      //settings for dialog box
      fdlg.Title = "Select file";
      fdlg.InitialDirectory = @"c:\";
      fdlg.FileName = data_file_name.Text;
      fdlg.Filter = "CSV Files(*.csv)|*.csv|All Files(*.*)|*.*";
      fdlg.FilterIndex = 1;
      fdlg.RestoreDirectory = true;
      
      if (fdlg.ShowDialog() == DialogResult.OK)
      {
          data_file_name.Text = fdlg.FileName;
          Import(fdlg.FileName);
      }
    }
    
    
    
/// <summary>
/// Imports the file passed in
/// </summary>
/// <param name="path"></param>
    private void Import(string path)
    {
      if (data_file_name.Text.Trim() != string.Empty)
      {
        try
        {
            CsvDataTable = ParseCSV(path);
            dataGrid_imported.DataSource = CsvDataTable.DefaultView;
            text_current_row.Text = "0";
            current_csv_row = 1;
            
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message.ToString());
        }
      }
    }
    
    
/// <summary>
/// Parses the CSV file at the location passed in
/// </summary>
/// <param name="path"></param>
/// <returns></returns>
    private DataTable ParseCSV(string path)
    {
      
      if (!File.Exists(path))
        return null;
      
      string full = Path.GetFullPath(path);
      string file = Path.GetFileName(full);
      string dir = Path.GetDirectoryName(full);
      
      //create a buffer datatable
      DataTable dtable = new DataTable();      
      
      //open file
      StreamReader csv_file = File.OpenText(full);
      
      //read first line to determine device name and byte order
      string file_line = csv_file.ReadLine();
      string[] _buffer_str = file_line.Split(',');
      
      string[] _device_name = _buffer_str[0].Split(':');
      string[] _byte_order = _buffer_str[1].Split(':');
      
      //write the device name and byte order to the screen
      text_device_name.Text = _device_name[1].Trim();
      text_byte_order.Text = _byte_order[1].Trim();
      
      //format byte order string to all lower case and replace space with underscore
      _byte_order[1] = _byte_order[1].ToLower();
      _byte_order[1] = _byte_order[1].Trim();
      byte_order = _byte_order[1].Replace(' ', '_');
      
      //read second line to determine column headers
      file_line = csv_file.ReadLine();
      string[] file_header = file_line.Split(',');
      
      DataRow drow = dtable.NewRow();
      
      for (int i = 0; i < file_header.Length; i++)
      {
        dtable.Columns.Add(file_header[i]);
      }
      
      while (csv_file.EndOfStream == false)
      {
        //read next line to import the data, convert all strings to lower case
        file_line = csv_file.ReadLine();
        file_line = file_line.ToLower();
        _buffer_str = file_line.Split(',');
        
        drow = dtable.NewRow();
        
        for (int i = 0; i < _buffer_str.Length; i++)
        {
          drow[file_header[i]] = _buffer_str[i];
        }
        
        dtable.Rows.Add(drow);
      }      
      return dtable;
    }
    
    
/// <summary>
/// Sets up the Modbus Map, includes all types of registers
/// </summary>
    private void InitializeModbusMap()
    {
      SimulatorDatabase = MbSlaveSession.SimDatabase as SMBSimDatabase;
      
      SimulatorDatabase.Clear();
      
      SMBSimHReg hold_reg_point;
      
      for (ushort i = 0; i < 41000; i++)
      {
        hold_reg_point = SimulatorDatabase.addHreg(i, 0);
      }
    }
    
    
    
    
    private void FillModbusMapWithData(object sender, System.EventArgs e)
    {
      if (this.open_mb_session.Checked) 
      {
        DataTable MbDataTable = new DataTable();
      
        text_current_row.Text = Convert.ToString(Convert.ToInt32(text_current_row.Text) + 1);
        current_csv_row += 1;
        
        if (current_csv_row == Convert.ToInt32(CsvDataTable.Rows.Count.ToString()))
        {
          text_current_row.Text = "1";
          current_csv_row = 2;
        }
        
        for (uint i = 0; i < cfg_mb_per_port.Value; i++)
        {
          MbDataTable = PopulateModbusMap(current_csv_row, CsvDataTable, byte_order);          
        }

        dataGrid_mb_registers.DataSource = MbDataTable.DefaultView;
      }
      
    }
    
    
    
    
    private void SimulatorFormClosing(object sender, FormClosingEventArgs e)
    {
      isFormClosing = true;
      this.data_refresh_timer.Stop();
      CloseSession();
      CloseChannel();    
    }
    
    // If session is closed from another interface, 
    // stop accessing the session. In fact close this GUI.
    int SessionCloseEvent(TMWSession s1)
    {
      this.data_refresh_timer.Stop();
      if (!isFormClosing) // if its not already being closed by us
        Close();
      return 0;
    }
    
    public String IP
    {
      get { return m_ip; }
      set { m_ip = value; }
    }

    public UInt16 Port
    {
      get { return m_port; }
      set { m_port = value; }
    }
  
    public String SessionId
    {
      get { return m_sessionId; }
      set { m_sessionId = value; }
    }
    
    private String m_sessionId = null;
    private String m_ip = "*.*.*.*";
    private UInt16 m_port = 502;
    private string byte_order;
    
    private MBChannel Channel;
    private SMBSession MbSlaveSession;
    private SMBSimDatabase SimulatorDatabase;
   
    private int current_csv_row;
    private DataTable CsvDataTable;
    private Boolean isFormClosing = false; 
    
  }
}