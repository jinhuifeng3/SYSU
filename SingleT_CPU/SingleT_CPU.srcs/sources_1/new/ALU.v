`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/24 16:12:14
// Design Name: 
// Module Name: ALU
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


module ALU(A,B,ALUOP,zero,result);
  input [31:0] A,B;
  input [2:0] ALUOP;
  output zero;
  output reg [31:0] result;
  initial begin
    result = 0;
  end
  assign zero = result ? 0 : 1;
  always @(A or B or ALUOP)begin
    case(ALUOP)
      3'b000: result = A + B;
      3'b001: result = A - B;
      3'b010: result = B << A;
      3'b011: result = A | B;
      3'b100: result = A & B;
      3'b101: result = (A < B) ? 1:0;
      3'b110: result = (((A < B) && (A[31] == B[31]))||((A[31] == 1 && B[31] == 0))) ? 1:0; 
      3'b111: result = (~A & B) | (A & ~B);
    endcase;
  end
endmodule
