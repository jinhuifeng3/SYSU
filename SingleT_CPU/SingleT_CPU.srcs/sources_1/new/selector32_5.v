`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/25 21:33:59
// Design Name: 
// Module Name: selector32_5
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


module selector32_5(A,B,signal,out);
  input [31:0] A;
  input [4:0] B;
  input signal;
  output reg [31:0] out;
  always@(A or B or signal)
    out <= (signal == 1'b0 ? A : {{27{1'b0}},B[4:0]});
endmodule

