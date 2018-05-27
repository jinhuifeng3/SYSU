`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/26 09:42:48
// Design Name: 
// Module Name: selector3
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

module selector3(in0,in1,in2,out,PCSrc);
  input [31:0] in0,in1,in2;
  input [1:0] PCSrc;
  output reg [31:0] out;
  always@(PCSrc or in0 or in1 or in2)begin
    case(PCSrc)
      2'b00: out = in0;
      2'b01: out = in1;
      2'b10: out = in2;
    endcase
  end
endmodule
