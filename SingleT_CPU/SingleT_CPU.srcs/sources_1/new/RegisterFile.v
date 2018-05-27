`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/23 19:10:21
// Design Name: 
// Module Name: RegisterFile
// Project Name: 
// Target Devices: 
// Tool Versions: 
// Description: 
// 
// Dependencies: 
// 
// Revision:
// Revision 0.01 - File Created
// Additional Comments:
// 
//////////////////////////////////////////////////////////////////////////////////


module RegisterFile(ReadReg1,ReadReg2,ReadData1,ReadData2,WriteReg,WriteData,WE,CLK);
  input WE,CLK;
  input [4:0] ReadReg1,ReadReg2,WriteReg;
  input [31:0] WriteData;
  output [31:0] ReadData1,ReadData2;
  reg [31:0] register [0:31];
  
  initial begin
    register[0] = 0;
  end
  
  assign ReadData1 = register[ReadReg1];
  assign ReadData2 = register[ReadReg2];
  
  always @(posedge CLK) begin
    if((WE == 1) && (WriteReg != 5'b00000)) begin
      register[WriteReg] = WriteData;
    end
  end  
endmodule
