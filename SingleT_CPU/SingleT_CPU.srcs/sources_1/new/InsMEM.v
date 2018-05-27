`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/24 15:09:44
// Design Name: 
// Module Name: InsMEM
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


module InsMEM(IAddr,RW,IDataOut);
  input RW;
  input [31:0] IAddr;
  output reg[31:0] IDataOut;
  reg [7:0] memory [0:127];
  initial begin
    $readmemb("../../../test.coe",memory);
    IDataOut = 0;
  end
  always@ (IAddr or RW) begin
    if(RW)begin
      IDataOut[31:24] = memory[IAddr];
      IDataOut[23:16] = memory[IAddr + 1];
      IDataOut[15:8] = memory[IAddr + 2];
      IDataOut[7:0] = memory[IAddr + 3];
    end
  end
endmodule
