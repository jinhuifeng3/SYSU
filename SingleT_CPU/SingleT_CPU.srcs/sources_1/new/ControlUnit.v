`timescale 1ns / 1ps
//////////////////////////////////////////////////////////////////////////////////
// Company: 
// Engineer: 
// 
// Create Date: 2018/05/24 17:33:49
// Design Name: 
// Module Name: ControlUnit
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


module ControlUnit(decode,zero,InsMemRW,PCWre,ExtSel,DBDataSrc,mWR,mRD,ALUSrcA,ALUSrcB,PCsrc,ALUOp,RegWre,RegDst);
  input [5:0] decode;
  input zero;
  output reg InsMemRW;
  output reg PCWre;
  output reg ExtSel;
  output reg DBDataSrc;
  output reg mWR;
  output reg mRD;
  output reg ALUSrcA;
  output reg ALUSrcB;
  output reg [1:0]PCsrc;
  output reg [2:0] ALUOp;
  output reg RegWre;
  output reg RegDst;
  
  initial begin
    InsMemRW = 1;
    PCWre = 1;
    ExtSel = 0;
    DBDataSrc = 0;
    mWR = 0;
    mRD = 0;
    ALUSrcA = 0;
    ALUSrcB = 0;
    PCsrc = 00;
    ALUOp = 000;
    RegWre = 0;
    RegDst = 1;
  end
  
  always@(decode or zero) begin
  case(decode)
    6'b000000: // add
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 0;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 0;
      PCsrc = 00;
      ALUOp = 000;
      RegWre = 1;
      RegDst = 1;
    end
    6'b000001:// addi
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 1;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 1;
      PCsrc = 00;
      ALUOp = 000;
      RegWre = 1;
      RegDst = 0;
    end
    6'b000010:// sub
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 0;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 0;
      PCsrc = 00;
      ALUOp = 001;
      RegWre = 1;
      RegDst = 1;
    end
    6'b010000://ori
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 0;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 1;
      PCsrc = 00;
      ALUOp = 011;
      RegWre = 1;
      RegDst = 0;
    end
    6'b010001://and
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 0;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 0;
      PCsrc = 00;
      ALUOp = 100;
      RegWre = 1;
      RegDst = 1;
    end
    6'b010010://or
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 0;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 0;
      PCsrc = 00;
      ALUOp = 011;
      RegWre = 1;
      RegDst = 1;
    end
    6'b011000://sll
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 0;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 1;
      ALUSrcB = 0;
      PCsrc = 00;
      ALUOp = 010;
      RegWre = 1;
      RegDst = 1;
    end
    6'b011011://slti
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 1;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 1;
      PCsrc = 00;
      ALUOp = 110;
      RegWre = 1;
      RegDst = 0;
    end
    6'b100110://sw
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 1;
      DBDataSrc = 0;
      mWR = 1;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 1;
      PCsrc = 00;
      ALUOp = 100;
      RegWre = 0;
      RegDst = 0;
    end
    6'b100111://lw
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 1;
      DBDataSrc = 1;
      mWR = 0;
      mRD = 1;
      ALUSrcA = 0;
      ALUSrcB = 1;
      PCsrc = 00;
      ALUOp = 000;
      RegWre = 1;
      RegDst = 0;
    end
    6'b110000://beq
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 1;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 0;
      if(zero == 0)begin
        PCsrc = 00;
      end
      else if(zero == 1)begin
        PCsrc = 01;
      end
      ALUOp = 001;
      RegWre = 0;
      RegDst = 0;
    end
    6'b110001://bne
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 1;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 0;
      if(zero == 0)begin
        PCsrc = 01;
      end
      else if(zero == 1)begin
        PCsrc = 00;
      end
      ALUOp = 001;
      RegWre = 0;
      RegDst = 0;
    end
    6'b111000://j
    begin
      InsMemRW = 1;
      PCWre = 1;
      ExtSel = 0;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 0;
      PCsrc = 10;
      ALUOp = 000;
      RegWre = 0;
      RegDst = 0;
    
    end
    6'b111111://halt
    begin
      InsMemRW = 0;
      PCWre = 0;
      ExtSel = 0;
      DBDataSrc = 0;
      mWR = 0;
      mRD = 0;
      ALUSrcA = 0;
      ALUSrcB = 0;
      PCsrc = 00;
      ALUOp = 000;
      RegWre = 0;
      RegDst = 0;
    end
    endcase
  end                                                    
endmodule
