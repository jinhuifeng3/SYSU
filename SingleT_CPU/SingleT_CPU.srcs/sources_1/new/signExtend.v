`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/24 15:37:21
// Design Name: 
// Module Name: signExtend
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


module signExtend(in_num,out_num,ExtSel);
  input wire [15:0] in_num;
  input wire ExtSel;
  output reg [31:0] out_num;
  initial begin
    out_num = 0;
  end
  always @(in_num or ExtSel)begin
    if(ExtSel) begin
      out_num <= {{16{in_num[15]}}, in_num[15:0]};
    end
  end
endmodule
