`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/26 09:21:36
// Design Name: 
// Module Name: Jump
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


module Jump(in_pc,addr,out_pc);
  input [31:0] in_pc;
  input [25:0] addr;
  output reg [31:0] out_pc;
  always @(in_pc or addr) begin
    out_pc[31:28] = in_pc[31:28];
    out_pc[27:2] = addr[25:0];
    out_pc[1:0] = 00;
  end
endmodule
