`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/24 14:49:34
// Design Name: 
// Module Name: PC
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


module PC(in_pc,out_pc,CLK,Reset,PCWre);
  input wire CLK, PCWre, Reset;
  input wire [31:0] in_pc;
  output reg [31:0] out_pc;
  always @(posedge CLK)begin
    if(Reset) begin
      out_pc = 0;
    end else if(PCWre) begin
      out_pc = in_pc;
    end else if(!PCWre) begin
      out_pc = out_pc;
    end
  end
endmodule
