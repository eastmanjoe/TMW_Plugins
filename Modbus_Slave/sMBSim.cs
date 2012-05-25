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

namespace ModbusSimulatorSlave
{
  public partial class FormMBSimSlave : Form
  {
    public FormMBSimSlave()
    {
      InitializeComponent();
      
      Connect();
      
      BuildDatabase();
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
    
    void StartSlaveClick(object sender, EventArgs e)
    {
      
      this.data_refresh_timer.Enabled = true;
      this.data_refresh_timer.Start();
    }
    
    private void Disconnect()
    {
      if (this.SessionId == null)
      {
        if (this.MbSlaveSession != null)
          this.MbSlaveSession.CloseSession();

        if (this.Channel != null)
          this.Channel.CloseChannel();
      }

      this.Channel = null;
      this.MbSlaveSession = null;
      this.SessionId = null;
    }
    
    private void Connect()
    {
      // if we are already connected disconnect
      if (this.Channel != null)
        Disconnect();
      
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
          Channel = new MBChannel(TMW_CHANNEL_OR_SESSION_TYPE.SLAVE);
          Channel.Type = WINIO_TYPE.TCP;
          Channel.WinTCPipPort = Port;
          Channel.WinTCPipAddress = IP;
          Channel.WinTCPmode = TCP_MODE.SERVER;
          Channel.Name = "sMBSim_" + Port;
          Channel.Protocol = TMW_PROTOCOL.MB;
          Channel.OpenChannel();
          
          MbSlaveSession = new SMBSession(Channel);
          MbSlaveSession.SlaveAddress = 1;
          MbSlaveSession.Name = "sMBSim_" + MbSlaveSession.SlaveAddress;
          MbSlaveSession.OpenSession();
        } 
        catch (Exception ex)
        {
          MessageBox.Show("Error Opening: " + ex.Message);
          Channel = null;
          MbSlaveSession = null;
          //Database = null;
          Close();
          
          return;
        }
      }
    }
    
    private void BuildDatabase()
    {
      SimulatorDatabase = MbSlaveSession.SimDatabase as SMBSimDatabase;
      
      SimulatorDatabase.Clear();
      
      SMBSimHReg hold_reg_point;
      
      for (ushort i = 0; i < 2000; i++)
      {
        hold_reg_point = SimulatorDatabase.addHreg(i, 0);
      }
      
    }
    
    void DataFileImport(object sender, EventArgs e)
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
    
    public DataTable ParseCSV(string path)
    {
      
      if (!File.Exists(path))
        return null;
      
      string full = Path.GetFullPath(path);
      string file = Path.GetFileName(full);
      string dir = Path.GetDirectoryName(full);
      
      //create a buffer datatable
      DataTable dtable = new DataTable();      
      
      //Open File
      StreamReader csv_file = File.OpenText(full);
      
      string file_line = csv_file.ReadLine();
      string[] file_header = file_line.Split(',');
      
      DataRow drow = dtable.NewRow();
      
      for (int i = 0; i < file_header.Length; i++)
      {
        dtable.Columns.Add(file_header[i]);
      }
      
      while (csv_file.EndOfStream == false)
      {
        file_line = csv_file.ReadLine();
        file_line = file_line.ToLower();
        string[] buffer_str = file_line.Split(',');
        
        drow = dtable.NewRow();
        
        for (int i = 0; i < buffer_str.Length; i++)
        {
          drow[file_header[i]] = buffer_str[i];
        }
        
        dtable.Rows.Add(drow);
      }      
      return dtable;
    }
    
    private DataTable PopulateMBMap(int row_number)
    //private int PopulateMBMap(int row_number)
    {
      if (row_number == Convert.ToInt32(CsvDataTable.Rows.Count.ToString()))
        return null;
      
      
      //SMBSimHReg hreg_pt;
      
      //determine the size of the data set
      int row_count = Convert.ToInt32(CsvDataTable.Rows.Count.ToString());
      int column_count = Convert.ToInt32(CsvDataTable.Columns.Count.ToString());
      
      ushort mb_register_start;
      int mb_register_count;
      ushort[] mb_register_data = new ushort[2];
      
      DataTable mb_data = new DataTable();
      mb_data.Columns.Add("MB Register");
      mb_data.Columns.Add("MB Data");      
      
      DataRow drow = mb_data.NewRow();
      
      Int16 buffer_int16;
      UInt16 buffer_uint16;

      //check the type of register
      for (int i = 1; i < (column_count); i++)
      {
        mb_register_start = Convert.ToUInt16(CsvDataTable.Rows[1][i]);
        
        if (CsvDataTable.Rows[row_number][i].ToString() != "")
        {
          //calculate modbus register values for data
          switch (CsvDataTable.Rows[0][i].ToString())
          {
          case "float":
              mb_register_count = 2;
              mb_register_data = ModbusRegConverterFloat(Convert.ToSingle(CsvDataTable.Rows[row_number][i].ToString()));
              break;
  
          case "sint32":
              mb_register_count = 2;
              mb_register_data = ModbusRegConverterInt32(Convert.ToInt32(CsvDataTable.Rows[row_number][i].ToString()));
              break;
          case "uint32":
              mb_register_count = 2;
              mb_register_data = ModbusRegConverterUInt32(Convert.ToUInt32(CsvDataTable.Rows[row_number][i].ToString()));
              break;
          case "sint16":
              mb_register_count = 1;
              buffer_int16 = Convert.ToInt16(CsvDataTable.Rows[row_number][i].ToString());
              mb_register_data[0] = (ushort)(buffer_int16);
              mb_register_data[1] = 0;
              break;
          case "uint16":
              mb_register_count = 1;
              buffer_uint16 = Convert.ToUInt16(CsvDataTable.Rows[row_number][i].ToString());
              mb_register_data[0] = (ushort)(buffer_uint16);
              mb_register_data[1] = 0;
              break;
          default:
            mb_register_count = 1;
            mb_register_data[0] = (ushort)0;
            mb_register_data[1] = 0;
          	break;
          }
        }
        else
        {
          mb_register_count = 1;
          mb_register_data[0] = (ushort)0;
          mb_register_data[1] = 0;
        }
        
        
        if (mb_register_count == 2)
        {
          drow["MB Register"] = mb_register_start;
          drow["MB Data"] = mb_register_data[0];
          mb_data.Rows.Add(drow);
          
          drow = mb_data.NewRow();
          drow["MB Register"] = mb_register_start + 1;
          drow["MB Data"] = mb_register_data[1];
          mb_data.Rows.Add(drow);
          
          SimulatorDatabase.setHregValue((ushort)(mb_register_start), mb_register_data[0]);
          SimulatorDatabase.setHregValue((ushort)(mb_register_start + 1), mb_register_data[1]);
        }
        else
        {
          drow["MB Register"] = mb_register_start;
          drow["MB Data"] = mb_register_data[0];
          mb_data.Rows.Add(drow);
          
          SimulatorDatabase.setHregValue((ushort)(mb_register_start), mb_register_data[1]);
        }
        
        drow = mb_data.NewRow();
      }
      
      return mb_data;
    }
        
    private void FillModbusMapWithData(object sender, System.EventArgs e)
    {
      DataTable MbDataTable = new DataTable();
      
      text_current_row.Text = Convert.ToString(Convert.ToInt32(text_current_row.Text) + 1);
      current_csv_row += 1;
      
      if (current_csv_row == Convert.ToInt32(CsvDataTable.Rows.Count.ToString()))
      {
        text_current_row.Text = "1";
        current_csv_row = 2;
      }
        
      MbDataTable = PopulateMBMap(current_csv_row);
      
      
      dataGrid_mb_registers.DataSource = MbDataTable.DefaultView;
    }
    
    private ushort[] ModbusRegConverterFloat(float input)
    {
      byte[] _mb_reg_bytes = new byte[4];
      ushort[] _mb_reg = new ushort[2];
      
      _mb_reg_bytes = BitConverter.GetBytes(input);
      
      _mb_reg[0] = (ushort)(BitConverter.ToInt16(_mb_reg_bytes, 0));
      _mb_reg[1] = (ushort)(BitConverter.ToInt16(_mb_reg_bytes, 2));
      
      return _mb_reg;
    }
    
    private ushort[] ModbusRegConverterInt32(Int32 input)
    {
      byte[] _mb_reg_bytes = new byte[4];
      ushort[] _mb_reg = new ushort[2];
      
      _mb_reg_bytes = BitConverter.GetBytes(input);
      
      _mb_reg[0] = (ushort)(BitConverter.ToInt16(_mb_reg_bytes, 0));
      _mb_reg[1] = (ushort)(BitConverter.ToInt16(_mb_reg_bytes, 2));
      
      return _mb_reg;
    }
    
    private ushort[] ModbusRegConverterUInt32(UInt32 input)
    {
      byte[] _mb_reg_bytes = new byte[4];
      ushort[] _mb_reg = new ushort[2];
      
      _mb_reg_bytes = BitConverter.GetBytes(input);
      
      _mb_reg[0] = (ushort)(BitConverter.ToUInt32(_mb_reg_bytes, 0));
      _mb_reg[1] = (ushort)(BitConverter.ToUInt32(_mb_reg_bytes, 2));
      
      return _mb_reg;
    }
    
    private void SimulatorFormClosing(object sender, FormClosingEventArgs e)
    {
      isFormClosing = true;
      this.data_refresh_timer.Stop();
      Disconnect();      
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
    
    private String m_sessionId = null;
    private String m_ip = "*.*.*.*";
    private UInt16 m_port = 502;
   
    private MBChannel Channel;
    private SMBSession MbSlaveSession;
    private SMBSimDatabase SimulatorDatabase;
    
    private int current_csv_row;
    private DataTable CsvDataTable;
    private Boolean isFormClosing = false; 
    
  }
}