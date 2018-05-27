`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/26 18:27:04
// Design Name: 
// Module Name: singleCPU_sim
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


module singleCPU_sim();
  reg CLK;
  reg Reset;
  wire [31:0] ins;
  wire [31:0] nextAddr;
  wire [31:0] aluRESULT;
  wire [31:0] writeDATA;
  wire [31:0] readDATA1;
  wire [31:0] readDATA2;
  wire [31:0] extendDATA;
  wire [31:0] DATAOUT;
  wire [5:0] INSOP;
  wire [2:0] aluOP;
  wire [1:0] PCSRC;
  wire ZERO;
  Main uut(
    .CLK(CLK),
    .Reset(Reset),
    .ins(ins),
    .nextAddr(nextAddr),
    .aluRESULT(aluRESULT),
    .writeDATA(writeDATA),
    .readDATA1(readDATA1),
    .readDATA2(readDATA2),
    .extendDATA(extendDATA),
    .DATAOUT(DATAOUT),
    .aluOP(aluOP),
    .ZERO(ZERO),
    .PCSRC(PCSRC),
    .INSOP(INSOP));
    initial begin
      CLK = 0;
      Reset = 1;
      #50;
        CLK = ~CLK;
      #50;
        Reset = 0;
      forever #50 begin
        CLK = ~CLK;
      end
    end
endmodule
