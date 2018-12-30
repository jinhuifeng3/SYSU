pragma solidity ^0.4.22;


contract Draw {
  
  address proposer;
  uint TotalTicketsNum;
  uint ReleasedTicketsNum = 0;
  uint TicketPrice;
  uint WinnersNum;
  bool executed = false;
  bool []TicketsState;
  
  address [] public addrs;

  struct Participant{
    uint []tickets;
  }
  
  struct ret{
      string STATE;
      uint TICKET_PRICE;
      uint TOTAL_TICKETS;
      uint TICKETS_AVAILABLE;
      uint LUCKY_TICKETS_NUM;
  }
    
  constructor(uint price,uint num, uint winners)  public payable{
      if(winners >= num)
        return;
      proposer = msg.sender;
      TicketPrice = price;
      TotalTicketsNum = num;
      WinnersNum = winners;
  }
  
  
  function getTotal_Available_Price_WinnerTicketsNum() public view returns(uint b,uint c,uint d,uint e){
      uint available = TotalTicketsNum - ReleasedTicketsNum;
      return (TotalTicketsNum,available,TicketPrice,WinnersNum);
  }

  mapping(address => Participant) participants;
  mapping (uint => address) ticketsOwner;
  
  function buyTickets(uint nums)  public view{
    uint cost = nums * TicketPrice;
    if(msg.value < cost || executed 
      || nums > TotalTicketsNum - ReleasedTicketsNum) return;
    msg.sender.transfer(msg.value - cost);
    for(uint i = 0; i < nums; i++)
    {
      ticketsOwner[ReleasedTicketsNum] = msg.sender;
      participants[msg.sender].tickets.push(ReleasedTicketsNum);
      
      addrs.push(msg.sender);
      TicketsState.push(false);
      ReleasedTicketsNum++;
    }
    if(ReleasedTicketsNum == TotalTicketsNum)
      prizesGiving(msg.sender);
  }

    
  function prizesGiving(address trigger) public payable{
    if(!executed && ReleasedTicketsNum == TotalTicketsNum){
      uint rand;
      uint numsGiven = 0;
      uint prize = address(this).balance / WinnersNum;
      while (numsGiven < WinnersNum){
        uint temp = uint(keccak256(abi.encodePacked(now,trigger,rand))) % TotalTicketsNum;
        if(TicketsState[temp] == false){
          TicketsState[temp] = true;

          ticketsOwner[temp].transfer(prize);
          numsGiven++;
        }
        rand++;
      }
      executed = true;
    }
  }

    
  function refundAll() public{
    if(msg.sender != proposer || executed) return;
    uint refund = this.balance/ReleasedTicketsNum;
    for(uint i = 0; i < ReleasedTicketsNum; i++){
      ticketsOwner[i].transfer(refund);
    }
    executed = true;    
  }
  
  
  function getWinners()  public view returns (uint[] memory) {
    uint[] memory WINNERS_INDEX = new uint[](WinnersNum);
    uint temp = 0;
    for(uint i = 0; i < ReleasedTicketsNum; i++){
      if(TicketsState[i] == true)
        WINNERS_INDEX[temp] = i;
        temp++;
    }
    return WINNERS_INDEX;
  }

  function getPersonalInfo() public view returns(uint[] memory) {
    uint[] memory TICKETS = new uint[](participants[msg.sender].tickets.length);
    for (uint i=0; i<participants[msg.sender].tickets.length; i++)
      TICKETS[i] = participants[msg.sender].tickets[i];
    return TICKETS; 
  }
}