`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/25 21:19:13
// Design Name: 
// Module Name: PCAddNum
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


module PCAddNum(in_pc,out_pc,addImm);
  input [31:0] in_pc, addImm;
  output [31:0] out_pc;
  assign out_pc = in_pc + addImm * 4;
endmodule
