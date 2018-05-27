`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/26 10:00:46
// Design Name: 
// Module Name: Main
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


module Main(CLK,Reset,ins,nextAddr,aluRESULT,writeDATA,readDATA1,readDATA2,extendDATA,DATAOUT,aluOP,ZERO,PCSRC,INSOP);
  input CLK,Reset;
  output [31:0] ins,nextAddr,aluRESULT,writeDATA,readDATA1,readDATA2,extendDATA,DATAOUT;
  output [5:0] INSOP;
  output [2:0] aluOP;
  output [1:0] PCSRC;
  output ZERO;
  wire InsMemRW,PCWre,ExtSel,DBDataSrc,mWR,mRD,ALUSrcB,ALUSrcA,RegWre,RegDst,zero;
  wire [1:0] PCSrc;
  wire [2:0] ALUOp;
  wire [4:0] RFOUT;
  wire [31:0] PC,PCOUT,PC4,InsMEMOUT,signOUT,jOUT,ALUAOUT,ALUBOUT,RFOUT1,RFOUT2,PCAddOUT,resultOUT,DataOUT,WriteData;
  
  assign ins = InsMEMOUT;
  assign nextAddr = PCOUT;
  assign aluRESULT = resultOUT;
  assign writeDATA = WriteData;
  assign readDATA1 = RFOUT1;
  assign readDATA2 = RFOUT2;
  assign extendDATA = signOUT;
  assign DATAOUT = DataOUT;
  assign aluOP = ALUOp;
  assign ZERO = zero;
  assign PCSRC = PCSrc;
  assign INSOP = InsMEMOUT[31:26];
  
  RegisterFile registerFile(InsMEMOUT[25:21],InsMEMOUT[20:16],RFOUT1,RFOUT2,RFOUT,WriteData,RegWre,CLK);
  ALU alu(ALUAOUT,ALUBOUT,ALUOp,zero,resultOUT);
  ControlUnit controlUnit(InsMEMOUT[31:26],zero,InsMemRW,PCWre,ExtSel,DBDataSrc,mWR,mRD,ALUSrcA,ALUSrcB,PCSrc,ALUOp,RegWre,RegDst);
  DataMEM dataMEM(resultOUT,RFOUT2,DataOUT,CLK,mRD,mWR);
  InsMEM insMEM(PCOUT,InsMemRW,InsMEMOUT);
  Jump j(PC4,InsMEMOUT[25:0],jOUT);
  PC pc(PC,PCOUT,CLK,Reset,PCWre);
  PCAddNum pcAddNum(PC4,PCAddOUT,signOUT);
  PcAddFour pcAddFour(PCOUT,PC4);
  selector3 s3(PC4,PCAddOUT,jOUT,PC,PCSrc);
  selector32 DBChoose(resultOUT,DataOUT,DBDataSrc,WriteData);
  selector32 ALUB(RFOUT2,signOUT,ALUSrcB,ALUBOUT);
  selector32_5 ALUA(RFOUT1,InsMEMOUT[10:6],ALUSrcA,ALUAOUT);
  selector5 RF(InsMEMOUT[20:16],InsMEMOUT[15:11],RegDst,RFOUT);
  signExtend sign(InsMEMOUT[15:0],signOUT,ExtSel);
endmodule
