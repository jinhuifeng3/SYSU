`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/25 21:30:09
// Design Name: 
// Module Name: selector5
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


module selector5(A,B,signal,out);
  input [4:0] A, B;
  input signal;
  output reg [4:0] out;
  always@(A or B or signal)
    out <= (signal == 1'b0 ? A : B);
endmodule