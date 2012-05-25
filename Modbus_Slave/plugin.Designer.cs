namespace ModbusSimulatorSlave
{
  partial class sMBSimulator
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.smbsimbutton = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // smbsimbutton
      // 
      this.smbsimbutton.Location = new System.Drawing.Point(31, 218);
      this.smbsimbutton.Name = "smbsimbutton";
      this.smbsimbutton.Size = new System.Drawing.Size(138, 23);
      this.smbsimbutton.TabIndex = 0;
      this.smbsimbutton.Text = "Start Modbus Slave Simulator";
      this.smbsimbutton.UseVisualStyleBackColor = true;
      this.smbsimbutton.Click += new System.EventHandler(this.smbsimbutton_Click);
      // 
      // textBox1
      // 
      this.textBox1.BackColor = System.Drawing.SystemColors.Window;
      this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.textBox1.Location = new System.Drawing.Point(31, 37);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.ReadOnly = true;
      this.textBox1.Size = new System.Drawing.Size(617, 175);
      this.textBox1.TabIndex = 1;
      this.textBox1.Text = "This example simulates a Modbus Slave device.  Press the \"Start Modbus Slave Simulator\" button\r\nto" +
          " start the simulator.";
      // 
      // sMBSimulator
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.SystemColors.Window;
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.smbsimbutton);
      this.Name = "sMBSimulator";
      this.Size = new System.Drawing.Size(732, 627);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button smbsimbutton;
    private System.Windows.Forms.TextBox textBox1;
  }
}
