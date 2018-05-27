`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/24 16:49:27
// Design Name: 
// Module Name: DataMEM
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


module DataMEM(DAddr,DataIn,DataOut,CLK,RD,WR);
  input [31:0] DataIn;
  input [31:0] DAddr;
  input CLK,RD,WR;
  output reg[31:0] DataOut;
  reg[7:0] Memory[0:63];
  initial begin
    DataOut = 0;
  end;
  always @(negedge CLK)begin
    if(RD && !WR) begin
      DataOut[31:24] = Memory[DAddr + 3];
      DataOut[23:16] = Memory[DAddr + 2];
      DataOut[15:8] = Memory[DAddr + 1];
      DataOut[7:0] = Memory[DAddr];
    end 
    if(WR && !RD) begin
      Memory[DAddr + 3] = DataIn[31:24];
      Memory[DAddr + 2] = DataIn[23:16];
      Memory[DAddr + 1] = DataIn[15:8];
      Memory[DAddr] = DataIn[7:0];
    end
  end
endmodule
