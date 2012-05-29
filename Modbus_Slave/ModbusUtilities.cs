/*
 * Created by SharpDevelop.
 * User: jeastman
 * Date: 05/29/2012
 * Time: 10:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
  /// <summary>
  /// Methods to convert floats, signed and unsigned longs to modbus register unsigned
  /// 16-bit integers
  /// </summary>
  //public class ModbusUtilities
  partial class FormMBSimSlave
  {
    public ushort[] ModbusRegConverter(float input, string byte_order)
    {
      byte[] _mb_reg_bytes = new byte[4];
      ushort[] _mb_reg = new ushort[2];
      
      _mb_reg_bytes = BitConverter.GetBytes(input);
      
      switch (byte_order) 
      {
        case "big_endian":
          _mb_reg[1] = BitConverter.ToUInt16(_mb_reg_bytes, 0);
          _mb_reg[0] = BitConverter.ToUInt16(_mb_reg_bytes, 2);
          break;
        case "little_endian":
          _mb_reg[0] = BitConverter.ToUInt16(_mb_reg_bytes, 0);
          _mb_reg[1] = BitConverter.ToUInt16(_mb_reg_bytes, 2);
          break;
        default:
          
        	break;
      }
      
      
      return _mb_reg;
    }
    
    
    
    
    public ushort[] ModbusRegConverter(Int32 input, string byte_order)
    {
      byte[] _mb_reg_bytes = new byte[4];
      ushort[] _mb_reg = new ushort[2];
      
      _mb_reg_bytes = BitConverter.GetBytes(input);
      
      switch (byte_order) 
      {
        case "big_endian":
          _mb_reg[1] = BitConverter.ToUInt16(_mb_reg_bytes, 0);
          _mb_reg[0] = BitConverter.ToUInt16(_mb_reg_bytes, 2);
          break;
        case "little_endian":
          _mb_reg[0] = BitConverter.ToUInt16(_mb_reg_bytes, 0);
          _mb_reg[1] = BitConverter.ToUInt16(_mb_reg_bytes, 2);
          break;
        default:
          
        	break;
      }
      
      return _mb_reg;
    }
    
    
    
    
    
    public ushort[] ModbusRegConverter(UInt32 input, string byte_order)
    {
      byte[] _mb_reg_bytes = new byte[4];
      ushort[] _mb_reg = new ushort[2];
      
      _mb_reg_bytes = BitConverter.GetBytes(input);
      
      switch (byte_order) 
      {
        case "big_endian":
          _mb_reg[1] = BitConverter.ToUInt16(_mb_reg_bytes, 0);
          _mb_reg[0] = BitConverter.ToUInt16(_mb_reg_bytes, 2);
          break;
        case "little_endian":
          _mb_reg[0] = BitConverter.ToUInt16(_mb_reg_bytes, 0);
          _mb_reg[1] = BitConverter.ToUInt16(_mb_reg_bytes, 2);
          break;
        default:
          
        	break;
      }
      
      return _mb_reg;
    }
    
    
    
/// <summary>
/// Uses the data from the imported CSV file to fill the Modbus Map registers
/// </summary>
/// <param name="row_number"></param>
/// <returns></returns>
    public DataTable PopulateModbusMap(int row_number, DataTable csv_data, string byte_order)
    {
      if (row_number == Convert.ToInt32(csv_data.Rows.Count.ToString()))
        return null;
      
      //determine the size of the data set
      int row_count = Convert.ToInt32(csv_data.Rows.Count.ToString());
      int column_count = Convert.ToInt32(csv_data.Columns.Count.ToString());
      
      ushort mb_register_start;
      int mb_register_count;
      ushort[] _mb_register_data = new ushort[2];
      
      DataTable mb_data = new DataTable();
      mb_data.Columns.Add("MB Register");
      mb_data.Columns.Add("MB Data");      
      
      DataRow drow = mb_data.NewRow();
      
      //check the type of register
      for (int i = 1; i < (column_count); i++)
      {
        mb_register_start = Convert.ToUInt16(csv_data.Rows[1][i]);
        
        if (csv_data.Rows[row_number][i].ToString() != "")
        {
          //calculate modbus register values for data
          switch (csv_data.Rows[0][i].ToString())
          {
          case "float":
              mb_register_count = 2;
              _mb_register_data = ModbusRegConverter(Convert.ToSingle(csv_data.Rows[row_number][i].ToString()), byte_order);
              //mb_register_data = ModbusRegConverter(Convert.ToSingle(CsvDataTable.Rows[row_number][i].ToString()), text_byte_order.Text);
              break;
  
          case "sint32":
              mb_register_count = 2;
              _mb_register_data = ModbusRegConverter(Convert.ToInt32(csv_data.Rows[row_number][i].ToString()), byte_order);
              //mb_register_data = ModbusRegConverterInt32(Convert.ToInt32(CsvDataTable.Rows[row_number][i].ToString()));
              break;
          case "uint32":
              mb_register_count = 2;
              _mb_register_data = ModbusRegConverter(Convert.ToUInt32(csv_data.Rows[row_number][i].ToString()), byte_order);
              //mb_register_data = ModbusRegConverterUInt32(Convert.ToUInt32(CsvDataTable.Rows[row_number][i].ToString()));
              break;
          case "sint16":
              mb_register_count = 1;
              _mb_register_data[0] = Convert.ToUInt16(csv_data.Rows[row_number][i].ToString());
              _mb_register_data[1] = 0;
              break;
          case "uint16":
              mb_register_count = 1;
              _mb_register_data[0] = Convert.ToUInt16(csv_data.Rows[row_number][i].ToString());
              _mb_register_data[1] = 0;
              break;
          default:
            mb_register_count = 1;
            _mb_register_data[0] = 0;
            _mb_register_data[1] = 0;
          	break;
          }
        }
        else
        {
          mb_register_count = 1;
          _mb_register_data[0] = 0;
          _mb_register_data[1] = 0;
        }
        
        
        if (mb_register_count == 2)
        {
          drow["MB Register"] = mb_register_start;
          drow["MB Data"] = _mb_register_data[0];
          mb_data.Rows.Add(drow);
          
          drow = mb_data.NewRow();
          drow["MB Register"] = mb_register_start + 1;
          drow["MB Data"] = _mb_register_data[1];
          mb_data.Rows.Add(drow);
          
          SimulatorDatabase.setHregValue(mb_register_start, _mb_register_data[0]);
          SimulatorDatabase.setHregValue((ushort)(mb_register_start + 1), _mb_register_data[1]);
        }
        else
        {
          drow["MB Register"] = mb_register_start;
          drow["MB Data"] = _mb_register_data[0];
          mb_data.Rows.Add(drow);
          
          SimulatorDatabase.setHregValue(mb_register_start, _mb_register_data[0]);
        }
        
        drow = mb_data.NewRow();
      }
      
      return mb_data;
    }
  }
}