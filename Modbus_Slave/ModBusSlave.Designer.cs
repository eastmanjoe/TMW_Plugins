using System;
  
namespace ModbusSimulatorSlave
{
  partial class FormMBSimSlave
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.gb_data_file = new System.Windows.Forms.GroupBox();
      this.data_file_name = new System.Windows.Forms.TextBox();
      this.data_file_import = new System.Windows.Forms.Button();
      this.button_fill_mb_map = new System.Windows.Forms.Button();
      this.gb_mb_outputs = new System.Windows.Forms.GroupBox();
      this.dataGrid_mb_registers = new System.Windows.Forms.DataGridView();
      this.data_refresh_timer = new System.Windows.Forms.Timer(this.components);
      this.gb_data_imported = new System.Windows.Forms.GroupBox();
      this.dataGrid_imported = new System.Windows.Forms.DataGridView();
      this.gb_mb_session = new System.Windows.Forms.GroupBox();
      this.open_mb_session = new System.Windows.Forms.CheckBox();
      this.label5 = new System.Windows.Forms.Label();
      this.text_total_mb_devices = new System.Windows.Forms.TextBox();
      this.cfg_mb_per_port = new System.Windows.Forms.NumericUpDown();
      this.cfg_start_mb_addr = new System.Windows.Forms.NumericUpDown();
      this.label4 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.cfg_tcp_port = new System.Windows.Forms.NumericUpDown();
      this.gb_mb_data = new System.Windows.Forms.GroupBox();
      this.checkBox3 = new System.Windows.Forms.CheckBox();
      this.text_current_row = new System.Windows.Forms.TextBox();
      this.label_current_row = new System.Windows.Forms.Label();
      this.gb_data_file.SuspendLayout();
      this.gb_mb_outputs.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid_mb_registers)).BeginInit();
      this.gb_data_imported.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid_imported)).BeginInit();
      this.gb_mb_session.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.cfg_mb_per_port)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cfg_start_mb_addr)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cfg_tcp_port)).BeginInit();
      this.gb_mb_data.SuspendLayout();
      this.SuspendLayout();
      // 
      // gb_data_file
      // 
      this.gb_data_file.Controls.Add(this.data_file_name);
      this.gb_data_file.Controls.Add(this.data_file_import);
      this.gb_data_file.Location = new System.Drawing.Point(8, 8);
      this.gb_data_file.Name = "gb_data_file";
      this.gb_data_file.Size = new System.Drawing.Size(744, 48);
      this.gb_data_file.TabIndex = 2;
      this.gb_data_file.TabStop = false;
      this.gb_data_file.Text = "Data File";
      // 
      // data_file_name
      // 
      this.data_file_name.BackColor = System.Drawing.SystemColors.Window;
      this.data_file_name.Location = new System.Drawing.Point(8, 16);
      this.data_file_name.Name = "data_file_name";
      this.data_file_name.Size = new System.Drawing.Size(576, 20);
      this.data_file_name.TabIndex = 0;
      // 
      // data_file_import
      // 
      this.data_file_import.BackColor = System.Drawing.SystemColors.ControlDark;
      this.data_file_import.FlatAppearance.BorderColor = System.Drawing.Color.Green;
      this.data_file_import.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
      this.data_file_import.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.data_file_import.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.data_file_import.Location = new System.Drawing.Point(592, 16);
      this.data_file_import.Name = "data_file_import";
      this.data_file_import.Size = new System.Drawing.Size(144, 24);
      this.data_file_import.TabIndex = 13;
      this.data_file_import.Text = "Import Data File";
      this.data_file_import.UseVisualStyleBackColor = false;
      this.data_file_import.Click += new System.EventHandler(this.DataFileImport);
      // 
      // button_fill_mb_map
      // 
      this.button_fill_mb_map.BackColor = System.Drawing.Color.Orange;
      this.button_fill_mb_map.FlatAppearance.BorderColor = System.Drawing.Color.Green;
      this.button_fill_mb_map.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
      this.button_fill_mb_map.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
      this.button_fill_mb_map.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.button_fill_mb_map.Location = new System.Drawing.Point(160, 24);
      this.button_fill_mb_map.Name = "button_fill_mb_map";
      this.button_fill_mb_map.Size = new System.Drawing.Size(144, 24);
      this.button_fill_mb_map.TabIndex = 16;
      this.button_fill_mb_map.Text = "Increment Data Row";
      this.button_fill_mb_map.UseVisualStyleBackColor = false;
      this.button_fill_mb_map.Click += new System.EventHandler(this.FillModbusMapWithData);
      // 
      // gb_mb_outputs
      // 
      this.gb_mb_outputs.Controls.Add(this.dataGrid_mb_registers);
      this.gb_mb_outputs.Location = new System.Drawing.Point(8, 576);
      this.gb_mb_outputs.Name = "gb_mb_outputs";
      this.gb_mb_outputs.Size = new System.Drawing.Size(912, 208);
      this.gb_mb_outputs.TabIndex = 12;
      this.gb_mb_outputs.TabStop = false;
      this.gb_mb_outputs.Text = "Modbus Register Output Values";
      // 
      // dataGrid_mb_registers
      // 
      this.dataGrid_mb_registers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGrid_mb_registers.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGrid_mb_registers.Location = new System.Drawing.Point(3, 16);
      this.dataGrid_mb_registers.Name = "dataGrid_mb_registers";
      this.dataGrid_mb_registers.Size = new System.Drawing.Size(906, 189);
      this.dataGrid_mb_registers.TabIndex = 0;
      // 
      // gb_data_imported
      // 
      this.gb_data_imported.Controls.Add(this.dataGrid_imported);
      this.gb_data_imported.Location = new System.Drawing.Point(8, 344);
      this.gb_data_imported.Name = "gb_data_imported";
      this.gb_data_imported.Size = new System.Drawing.Size(912, 208);
      this.gb_data_imported.TabIndex = 13;
      this.gb_data_imported.TabStop = false;
      this.gb_data_imported.Text = "Imported Data";
      // 
      // dataGrid_imported
      // 
      this.dataGrid_imported.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGrid_imported.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataGrid_imported.Location = new System.Drawing.Point(3, 16);
      this.dataGrid_imported.Name = "dataGrid_imported";
      this.dataGrid_imported.Size = new System.Drawing.Size(906, 189);
      this.dataGrid_imported.TabIndex = 0;
      // 
      // gb_mb_session
      // 
      this.gb_mb_session.Controls.Add(this.open_mb_session);
      this.gb_mb_session.Controls.Add(this.label5);
      this.gb_mb_session.Controls.Add(this.text_total_mb_devices);
      this.gb_mb_session.Controls.Add(this.cfg_mb_per_port);
      this.gb_mb_session.Controls.Add(this.cfg_start_mb_addr);
      this.gb_mb_session.Controls.Add(this.label4);
      this.gb_mb_session.Controls.Add(this.label2);
      this.gb_mb_session.Controls.Add(this.label1);
      this.gb_mb_session.Controls.Add(this.cfg_tcp_port);
      this.gb_mb_session.Location = new System.Drawing.Point(8, 64);
      this.gb_mb_session.Name = "gb_mb_session";
      this.gb_mb_session.Size = new System.Drawing.Size(744, 168);
      this.gb_mb_session.TabIndex = 14;
      this.gb_mb_session.TabStop = false;
      this.gb_mb_session.Text = "ModBus Session";
      // 
      // open_mb_session
      // 
      this.open_mb_session.Appearance = System.Windows.Forms.Appearance.Button;
      this.open_mb_session.BackColor = System.Drawing.Color.Lime;
      this.open_mb_session.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.open_mb_session.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
      this.open_mb_session.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.open_mb_session.Location = new System.Drawing.Point(8, 24);
      this.open_mb_session.Name = "open_mb_session";
      this.open_mb_session.Size = new System.Drawing.Size(144, 24);
      this.open_mb_session.TabIndex = 35;
      this.open_mb_session.Text = "Open Modbus Session";
      this.open_mb_session.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.open_mb_session.UseVisualStyleBackColor = false;
      this.open_mb_session.CheckedChanged += new System.EventHandler(this.SessionManagement);
      // 
      // label5
      // 
      this.label5.Location = new System.Drawing.Point(488, 24);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(176, 23);
      this.label5.TabIndex = 33;
      this.label5.Text = "Total Number of ModBus Devices";
      this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // text_total_mb_devices
      // 
      this.text_total_mb_devices.BackColor = System.Drawing.SystemColors.Info;
      this.text_total_mb_devices.Location = new System.Drawing.Point(672, 24);
      this.text_total_mb_devices.Name = "text_total_mb_devices";
      this.text_total_mb_devices.Size = new System.Drawing.Size(56, 20);
      this.text_total_mb_devices.TabIndex = 26;
      this.text_total_mb_devices.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // cfg_mb_per_port
      // 
      this.cfg_mb_per_port.Location = new System.Drawing.Point(392, 72);
      this.cfg_mb_per_port.Name = "cfg_mb_per_port";
      this.cfg_mb_per_port.Size = new System.Drawing.Size(56, 20);
      this.cfg_mb_per_port.TabIndex = 32;
      this.cfg_mb_per_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // cfg_start_mb_addr
      // 
      this.cfg_start_mb_addr.Location = new System.Drawing.Point(392, 48);
      this.cfg_start_mb_addr.Name = "cfg_start_mb_addr";
      this.cfg_start_mb_addr.Size = new System.Drawing.Size(56, 20);
      this.cfg_start_mb_addr.TabIndex = 31;
      this.cfg_start_mb_addr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label4
      // 
      this.label4.Location = new System.Drawing.Point(168, 48);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(216, 23);
      this.label4.TabIndex = 29;
      this.label4.Text = "Starting ModBus Address";
      this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label2
      // 
      this.label2.Location = new System.Drawing.Point(168, 24);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(216, 23);
      this.label2.TabIndex = 27;
      this.label2.Text = "Starting TCP Port";
      this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // label1
      // 
      this.label1.Location = new System.Drawing.Point(168, 72);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(216, 23);
      this.label1.TabIndex = 26;
      this.label1.Text = "Number of ModBus Devices Per TCP Port";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // cfg_tcp_port_start
      // 
      this.cfg_tcp_port.Location = new System.Drawing.Point(392, 24);
      this.cfg_tcp_port.Maximum = new decimal(new int[] {
                  10000,
                  0,
                  0,
                  0});
      this.cfg_tcp_port.Name = "cfg_tcp_port_start";
      this.cfg_tcp_port.Size = new System.Drawing.Size(56, 20);
      this.cfg_tcp_port.TabIndex = 21;
      this.cfg_tcp_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // gb_mb_data
      // 
      this.gb_mb_data.Controls.Add(this.checkBox3);
      this.gb_mb_data.Controls.Add(this.text_current_row);
      this.gb_mb_data.Controls.Add(this.label_current_row);
      this.gb_mb_data.Controls.Add(this.button_fill_mb_map);
      this.gb_mb_data.Location = new System.Drawing.Point(8, 240);
      this.gb_mb_data.Name = "gb_mb_data";
      this.gb_mb_data.Size = new System.Drawing.Size(744, 96);
      this.gb_mb_data.TabIndex = 37;
      this.gb_mb_data.TabStop = false;
      this.gb_mb_data.Text = "ModBus Data";
      // 
      // checkBox3
      // 
      this.checkBox3.Appearance = System.Windows.Forms.Appearance.Button;
      this.checkBox3.BackColor = System.Drawing.Color.Lime;
      this.checkBox3.FlatAppearance.BorderColor = System.Drawing.Color.Black;
      this.checkBox3.FlatAppearance.CheckedBackColor = System.Drawing.Color.Red;
      this.checkBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.checkBox3.Location = new System.Drawing.Point(8, 24);
      this.checkBox3.Name = "checkBox3";
      this.checkBox3.Size = new System.Drawing.Size(144, 24);
      this.checkBox3.TabIndex = 36;
      this.checkBox3.Text = "Start Data Loop";
      this.checkBox3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      this.checkBox3.UseVisualStyleBackColor = false;
      // 
      // text_current_row
      // 
      this.text_current_row.BackColor = System.Drawing.SystemColors.Info;
      this.text_current_row.Location = new System.Drawing.Point(672, 24);
      this.text_current_row.Name = "text_current_row";
      this.text_current_row.Size = new System.Drawing.Size(56, 20);
      this.text_current_row.TabIndex = 17;
      this.text_current_row.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      // 
      // label_current_row
      // 
      this.label_current_row.Location = new System.Drawing.Point(520, 24);
      this.label_current_row.Name = "label_current_row";
      this.label_current_row.Size = new System.Drawing.Size(144, 23);
      this.label_current_row.TabIndex = 18;
      this.label_current_row.Text = "Current Imported Data Row";
      this.label_current_row.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // FormMBSimSlave
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Control;
      this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
      this.ClientSize = new System.Drawing.Size(929, 793);
      this.Controls.Add(this.gb_mb_data);
      this.Controls.Add(this.gb_mb_session);
      this.Controls.Add(this.gb_data_imported);
      this.Controls.Add(this.gb_mb_outputs);
      this.Controls.Add(this.gb_data_file);
      this.DoubleBuffered = true;
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FormMBSimSlave";
      this.ShowIcon = false;
      this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
      this.Text = "Modbus Slave Simulator";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SimulatorFormClosing);
      this.gb_data_file.ResumeLayout(false);
      this.gb_data_file.PerformLayout();
      this.gb_mb_outputs.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid_mb_registers)).EndInit();
      this.gb_data_imported.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dataGrid_imported)).EndInit();
      this.gb_mb_session.ResumeLayout(false);
      this.gb_mb_session.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.cfg_mb_per_port)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cfg_start_mb_addr)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cfg_tcp_port)).EndInit();
      this.gb_mb_data.ResumeLayout(false);
      this.gb_mb_data.PerformLayout();
      this.ResumeLayout(false);
    }
    private System.Windows.Forms.CheckBox checkBox3;
    private System.Windows.Forms.GroupBox gb_mb_data;
    private System.Windows.Forms.NumericUpDown cfg_tcp_port;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.NumericUpDown cfg_start_mb_addr;
    private System.Windows.Forms.NumericUpDown cfg_mb_per_port;
    private System.Windows.Forms.TextBox text_total_mb_devices;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.CheckBox open_mb_session;
    private System.Windows.Forms.GroupBox gb_mb_session;
    private System.Windows.Forms.TextBox text_current_row;
    private System.Windows.Forms.Label label_current_row;
    private System.Windows.Forms.Button button_fill_mb_map;


    #endregion
    
    private System.Windows.Forms.DataGridView dataGrid_imported;
    private System.Windows.Forms.GroupBox gb_data_imported;
    private System.Windows.Forms.DataGridView dataGrid_mb_registers;
    private System.Windows.Forms.TextBox data_file_name;
    private System.Windows.Forms.Button data_file_import;
    private System.Windows.Forms.Timer data_refresh_timer;
    private System.Windows.Forms.GroupBox gb_mb_outputs;
    private System.Windows.Forms.GroupBox gb_data_file;
  }
}