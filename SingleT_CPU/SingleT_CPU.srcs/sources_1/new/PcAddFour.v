`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/24 15:05:08
// Design Name: 
// Module Name: PcAddFour
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


module PcAddFour(in_pc,out_pc);
  input wire [31:0] in_pc;
  output wire [31:0] out_pc;
  assign out_pc[31:0] = in_pc[31:0] + 4;
endmodule
