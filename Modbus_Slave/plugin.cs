using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using TMW.TH.UserPluginInterface;

using TMW.SCL;
using TMW.SCL.MB;

namespace ModbusSimulatorSlave
{
  public partial class sMBSimulator : UserControl, ITHUserPlugin
  {
    public sMBSimulator()
    {
      InitializeComponent();
    }


    private void smbsimbutton_Click(object sender, EventArgs e)
    {
      StartSim();
    }

    private void StartSim()
    {
      Form form = Application.OpenForms["sMBSim"];
      if (form == null)
      {
        this.m_openWindows.Add("sMBSim");
        ModbusSimulatorSlave.FormMBSimSlave sim = new ModbusSimulatorSlave.FormMBSimSlave();
        sim.Show();
      }
      else
      {
        form.Activate();
      }
    }
    
    private String GetNextSimName()
    {
      this.nextSim++;
      return ("sMBSim" + nextSim.ToString());
    }
    
    private void StartSimChannelSessionId(String sessionId)
    {
      ModbusSimulatorSlave.FormMBSimSlave sim = new ModbusSimulatorSlave.FormMBSimSlave();
      sim.Name = GetNextSimName();
      sim.SessionId = sessionId;
      
      this.m_openWindows.Add(sim.Name);

      sim.Show();
    }

    private void StartSimPort(String connectString)
    {
      ModbusSimulatorSlave.FormMBSimSlave sim = new ModbusSimulatorSlave.FormMBSimSlave();
      String[] pieces = connectString.Split(':');
      if (pieces.Length == 2)
      {
        sim.IP = pieces[0];
        sim.Port = Convert.ToUInt16(pieces[1]);
      }
      else if (pieces.Length == 1)
      {
        sim.IP = pieces[0];
      }
      else
      {
        sim.Dispose();
        StartSim();
        return;
      }

      sim.Name = GetNextSimName();
      this.m_openWindows.Add(sim.Name);

      sim.Show();
    }

    
    #region ITHUserPlugin Members

    private ITHUserPluginHost _Host = null;
    private string _Author = "Triangle MicroWorks Inc.";
    private string _Description = 
      "\n" +
      "Simulates a Modbus Slave Device\n"
    ;
    private string _Version = "1.0.0";

    public String PluginName 
    {
      get { return "Modbus Slave Simulator"; }
    }

    public void SetTargets(int channelId, int sessionId, int sectorId)
    {
    }

    /// <summary>
    /// Save workspace settings
    /// </summary>
    /// <returns>settings specific to plugin</returns>
    public String SaveWorkspace()
    {
      return null;
    }

    /// <summary>
    /// Load workspace
    /// </summary>
    /// <param name="stateString">load specific settings to plugin</param>
    public void LoadWorkspace(String stateString)
    {
    }

    /// <summary>
    /// This method is called the user double clicks on the command from 
    /// the command window. This can be used to configure the command
    /// or take input from the user.  
    /// </summary>
    public void Configure()
    {
    }

    public void CloseWorkspace()
    {
      // close any open windows simulators
      foreach (String s in this.m_openWindows)
      {
        Form form = Application.OpenForms[s];
        if (form != null)
          form.Close();
      }
    }

    public void PluginDispose()
    {
      CloseWorkspace();
    }

    private string _CommandName = "smbsim";
    public string CommandName
    {
      get
      {
        return _CommandName;
      }
    }

    private string[] _CommandOptions = { "open", "connect" };
    public string[] CommandOptions
    {
      get
      {
        return _CommandOptions;
      }
    }
    
    // Since this opens a dnp channel/session it does not really matter if it runs in a slave dnp window.
    private CommandWindowTargetEnum _commandWindowTargetMask =
        CommandWindowTargetEnum.MODBUS
      | CommandWindowTargetEnum.MASTER
      | CommandWindowTargetEnum.SLAVE;

    public CommandWindowTargetEnum CommandWindowTargetMask
    {
      get { return _commandWindowTargetMask; }
    }

    public string Description
    {
      get
      {
        return _Description;
      }
    }

    public string Author
    {
      get
      {
        return _Author;
      }
    }

    public ITHUserPluginHost Host
    {
      get
      {
        return _Host;
      }
      set
      {
        _Host = value;
      }
    }

    public void Initialize()
    {
      
    }

    public UserControl MainInterface
    {
      get
      {
        return this;
      }
    }

    public string Version
    {
      get
      {
        return _Version;
      }
    }

    public void LoadState(String stateString)
    {

    }

    public void SaveState(String stateString)
    {

    }
    void showHelp()
    {
      this.Host.WriteCmdLine("Help for plugin command: " + CommandName + "\n\n", this);
      this.Host.WriteCmdLine("  " + CommandOptions[0] + "      - start\n", this);
    }

    public void DoPluginCmd(String[] args)
    {
      if (args.Length <= 1)
      {
        showHelp();
        return;
      }

      if (args[1] == CommandOptions[0])
      {
        if (args.Length == 3)
          StartSimPort(args[2]);
        else
          StartSim();
      }
      else if (args[1] == CommandOptions[1])
      {
        if (args.Length == 3)
          StartSimChannelSessionId(args[2]);
        else
          showHelp();
      }
      else
        showHelp(); 
    }

    #endregion

    private int nextSim = 0; 
    private System.Collections.Generic.List<String> m_openWindows = new List<string>();
  }
}