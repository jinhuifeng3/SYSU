require('./style.css')
require('web3')



let accounts
let log_state
let current_user
let provider
var storage = window.localStorage
let abi = [{"constant":false,"inputs":[],"name":"refundAll","outputs":[],"payable":false,"stateMutability":"nonpayable","type":"function"},{"constant":false,"inputs":[{"name":"nums","type":"int256"}],"name":"buyTickets","outputs":[],"payable":true,"stateMutability":"payable","type":"function"},{"constant":true,"inputs":[],"name":"getPersonalInfo","outputs":[{"name":"","type":"uint256[]"}],"payable":false,"stateMutability":"view","type":"function"},{"constant":true,"inputs":[],"name":"getTotal_Available_Price_WinnerTicketsNum","outputs":[{"name":"TotalTickets","type":"int256"},{"name":"AvailableTickets","type":"int256"},{"name":"Price","type":"uint256"},{"name":"WinnerTickets","type":"uint256"}],"payable":false,"stateMutability":"view","type":"function"},{"constant":true,"inputs":[],"name":"getWinners","outputs":[{"name":"","type":"int256[]"}],"payable":false,"stateMutability":"view","type":"function"},{"inputs":[{"name":"price","type":"uint256"},{"name":"num","type":"int256"},{"name":"winners","type":"uint256"}],"payable":true,"stateMutability":"payable","type":"constructor"}]

if (typeof web3 !== 'undefined') {
    window.web3 = new Web3(web3.currentProvider)
  } else {
    provider = new Web3.providers.HttpProvider('http://127.0.0.1:8545')
    window.web3 = new Web3(provider)}
    web3.eth.getAccounts(function(err,accs){
    if(err){
        alert(err)
    }else{
        accounts = accs
        storage.setItem("users",accounts)
        console.log("All users:"+accounts)
    }
})

window.showAllLottery=function(){
    let public_ops
    let allJ = window.allLottery()
    console.log("All Lottery:"+allJ)
    if(allJ!=null){
        for(let i=0;i<allJ.length;i++){
            if(allJ[i]!=null){
                for(let j=0;j<allJ[i].length;j++){
                    if(allJ[i][j]!=null)
                        public_ops += '<option>' + allJ[i][j].addr + '</option>'
                }
            }
        }
    }  
    document.getElementById('public_ops').innerHTML=public_ops
}

window.allLottery = function(){
    let usersArr = new Array()
    let allUsers = storage.getItem("users"); 
    let head_index = 0
    let end_index = 42
    while(end_index <= allUsers.length){
        usersArr.push(allUsers.substring(head_index,end_index))
        head_index = end_index + 1
        end_index = head_index + 42
    }
    let retJ = []
    for(let i =0; i<usersArr.length;i++){
        retJ.push(window.getIssuedLottery(usersArr[i]))
    }
    return retJ
}
window.getIssuedLottery = function(aUser){
    let userIssued = storage.getItem(aUser+"ISSUE")
    let j = JSON.parse(userIssued)
    return j
}

window.showInfo = function(){
    let options
    let issueJ = window.getIssuedLottery(current_user)
    if(issueJ!=null){
        for(let i=0;i<issueJ.length;i++){
            if(issueJ[i]!=null)
                options += '<option>' + issueJ[i].addr + '</option>'
        }
    }
    document.getElementById('iss_op').innerHTML = options
    web3.eth.getBalance(current_user,function(err,value){
        let temp = current_user+'<br>'+'Balance:'+value/1000000000000000000+" ether"
        document.getElementById('basic').innerHTML = temp
    })
    let owns
    let buy = window.getOwnedLottery(current_user)
    if(buy != null){
        for(let i=0;i<buy.length;i++){
            owns+='<option>' + buy[i] + '</option>'
        }
    }
    document.getElementById('buy_op').innerHTML = owns
    document.getElementById('personal').style.display="block"

}


window.log = function(){
    if(document.getElementById('login').innerHTML == 'Log in'){
        window.login()
    }else{
        window.logout()
    }
}
window.login = function(){
    let addr = document.getElementById('user').value
    for(let i=0; i<accounts.length;i++){
        if(accounts[i] == addr){
            current_user = addr
            log_state = true
            window.showInfo()
            document.getElementById('login').innerHTML = 'Log out'
            document.getElementById('reminder').style.display="none"
            alert('Log in successfully!')
            window.showAllLottery()
            return
        }
    }
    alert('Invalid user!')
}

window.logout = function(){
    current_user = null
    log_state = false
    document.getElementById('user').value = ''
    document.getElementById('reminder').style.display="block"
    document.getElementById('personal').style.display="none"
    document.getElementById('login').innerHTML = 'Log in'
    alert('Log out successfully!')
}


window.learnMore = function(){
    if(!log_state){
        alert('Please log in first!')
        return
    }
    let index = document.getElementById('public_ops').selectedIndex
    let ad = document.getElementById('public_ops').options[index].text
    console.log(ad)
    var result = web3.eth.contract(abi).at(ad).getTotal_Available_Price_WinnerTicketsNum()
    alert("Total Tickets' Number:"+result[0]+'\n'+"Available Tickets' Number:"+result[1]+'\n'+"Price For One:"+result[2]+' ether'+'\n'+"Winner Tickets' Number: "+result[3])
}
window.getOwnedLottery = function(aBuyer){
    let ret = []
    let userOwn = storage.getItem(aBuyer+"OWN")
    let head_index = 0
    let end_index = 42
    if(userOwn != null){
        while(end_index <= userOwn.length){
            ret.push(userOwn.substring(head_index,end_index))
            head_index = end_index
            end_index = head_index + 42
        }
    }
    return ret
}
window.buy = function(){
    if(!log_state){
        alert('Please log in first!')
        return
    }
    let index = document.getElementById('public_ops').selectedIndex
    let ad = document.getElementById('public_ops').options[index].text
    let ticketsNum = document.getElementById('buyamount').value
    let result = web3.eth.contract(abi).at(ad).getTotal_Available_Price_WinnerTicketsNum()
    let c = 1000000000000000000*result[2]*ticketsNum
    var r=confirm("You are determined to buy the lottery at cost of "+result[2]*ticketsNum+" ether")
    if(r == false){
        alert('You canceled.')
        return
    }else{
        alert('You sent a transaction')
    }
    web3.eth.contract(abi).at(ad).buyTickets(ticketsNum,{from:current_user,value:c,gas:3000000},function(err,obj){
        document.getElementById('buyamount').value=""
        if(err){
            console.log(err)
        }else{
            let original = storage.getItem(current_user+"OWN")
            if (original == null){
                original = ad
            }else{
                if(original.indexOf(ad) == -1)
                    original += ad
            }
            storage.setItem(current_user+"OWN",original)
            console.log(obj)
            showInfo()
        }
    })
}
window.check = function(){
    let index = document.getElementById('buy_op').selectedIndex
    let ad = document.getElementById('buy_op').options[index].text
    let tickets = web3.eth.contract(abi).at(ad).getPersonalInfo({from:current_user})
    alert("You own:"+tickets+" as serial numbers on this kind of lottery")
}
window.cancel = function(){
    let index = document.getElementById('iss_op').selectedIndex
    let ad = document.getElementById('iss_op').options[index].text
    web3.eth.contract(abi).at(ad).refundAll({from:current_user},function(err,obj){
        if(!err){
            let str = storage.getItem(current_user+"ISSUE")
            let json = JSON.parse(str)
            console.log(ad)
            for (let i = 0; i < json.length; i++) {
                if(json[i]!=null){
                    if(json[i].addr == ad){
                        delete json[i]
                        console.log(json)
                        let tmp = JSON.stringify(json)
                        storage.setItem(current_user+"ISSUE",tmp)
                        showInfo()
                        showAllLottery()
                        break
                    }  
                }                  
            }
            console.log(json)
        }
    })
}
window.getWinners = function(){
    let index = document.getElementById('buy_op').selectedIndex
    let ad = document.getElementById('buy_op').options[index].text
    web3.eth.contract(abi).at(ad).getWinners(function(err,obj){
        if(err){
            console.log(err)
        }else{
            console.log(obj)
            if(obj[0] == -1){
                alert("The lottery has been canceled and your money is returned.")
            }else{
                alert("Winnners: "+ obj)
            }
        }
    })
}
window.issue = function(){
    if(!log_state){
        alert('Please log in first!')
        return
    }
    var price = document.getElementById('price').value
    var num = document.getElementById('num').value
    var winners = document.getElementById('winners').value
    var drawContract = web3.eth.contract(abi);
    var draw = drawContract.new(
        price,
        num,
        winners,
        {
            from: current_user, 
            data: '0x608060405260006002556000600560006101000a81548160ff021916908315150217905550604051606080610b4e833981018060405281019080805190602001909291908051906020019092919080519060200190929190505050336000806101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550826003819055508160018190555080600481905550505050610a8d806100c16000396000f30060806040526004361061006d576000357c0100000000000000000000000000000000000000000000000000000000900463ffffffff16806338e771ab146100725780634fd43b3f1461008957806381f539ff146100a9578063b7ef6dc514610115578063df15c37e14610155575b600080fd5b34801561007e57600080fd5b506100876101c1565b005b6100a760048036038101908080359060200190929190505050610313565b005b3480156100b557600080fd5b506100be610546565b6040518080602001828103825283818151815260200191508051906020019060200280838360005b838110156101015780820151818401526020810190506100e6565b505050509050019250505060405180910390f35b34801561012157600080fd5b5061012a61069f565b6040518085815260200184815260200183815260200182815260200194505050505060405180910390f35b34801561016157600080fd5b5061016a6106c9565b6040518080602001828103825283818151815260200191508051906020019060200280838360005b838110156101ad578082015181840152602081019050610192565b505050509050019250505060405180910390f35b6000806000809054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff163373ffffffffffffffffffffffffffffffffffffffff1614158061022c5750600560009054906101000a900460ff165b156102365761030f565b6002543073ffffffffffffffffffffffffffffffffffffffff163181151561025a57fe5b049150600090505b6002548112156102f3576008600082815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff166108fc839081150290604051600060405180830381858888f193505050501580156102e5573d6000803e3d6000fd5b508080600101915050610262565b6001600560006101000a81548160ff0219169083151502179055505b5050565b600080600060035484029250670de0b6b3a764000091508183029250823410806103495750600560009054906101000a900460ff165b8061035957506002546001540384135b156103aa573373ffffffffffffffffffffffffffffffffffffffff166108fc349081150290604051600060405180830381858888f193505050501580156103a4573d6000803e3d6000fd5b50610540565b3373ffffffffffffffffffffffffffffffffffffffff166108fc8434039081150290604051600060405180830381858888f193505050501580156103f2573d6000803e3d6000fd5b50600090505b83811015610529573360086000600254815260200190815260200160002060006101000a81548173ffffffffffffffffffffffffffffffffffffffff021916908373ffffffffffffffffffffffffffffffffffffffff160217905550600760003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600001600254908060018154018082558091505090600182039060005260206000200160009091929091909150555060066000908060018154018082558091505090600182039060005260206000209060209182820401919006909192909190916101000a81548160ff0219169083151502179055505060026000815480929190600101919050555080806001019150506103f8565b600154600254141561053f5761053e336107f8565b5b5b50505050565b6060806000600760003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff168152602001908152602001600020600001805490506040519080825280602002602001820160405280156105bf5781602001602082028038833980820191505090505b509150600090505b600760003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000018054905081101561069757600760003373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff1681526020019081526020016000206000018181548110151561066357fe5b9060005260206000200154828281518110151561067c57fe5b906020019060200201818152505080806001019150506105c7565b819250505090565b60008060008060006002546001540390506001548160035460045494509450945094505090919293565b6060806000806004546040519080825280602002602001820160405280156107005781602001602082028038833980820191505090505b50925060015460025414156107965760009150600090505b60025481121561078e576001151560068281548110151561073557fe5b90600052602060002090602091828204019190069054906101000a900460ff16151514156107815780838380600101945081518110151561077257fe5b90602001906020020181815250505b8080600101915050610718565b8293506107f2565b600560009054906101000a900460ff16156107f1577fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff8360008151811015156107db57fe5b90602001906020020181815250508293506107f2565b5b50505090565b600080600080600560009054906101000a900460ff1615801561081e5750600154600254145b15610a5a57600092506004543073ffffffffffffffffffffffffffffffffffffffff163181151561084b57fe5b0491505b600454831015610a3e57600154428686604051602001808481526020018373ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff166c0100000000000000000000000002815260140182815260200193505050506040516020818303038152906040526040518082805190602001908083835b6020831015156108fc57805182526020820191506020810190506020830392506108d7565b6001836020036101000a03801982511681845116808217855250505050505090500191505060405180910390206001900481151561093657fe5b0690506000151560068281548110151561094c57fe5b90600052602060002090602091828204019190069054906101000a900460ff1615151415610a3157600160068281548110151561098557fe5b90600052602060002090602091828204019190066101000a81548160ff0219169083151502179055506008600082815260200190815260200160002060009054906101000a900473ffffffffffffffffffffffffffffffffffffffff1673ffffffffffffffffffffffffffffffffffffffff166108fc839081150290604051600060405180830381858888f19350505050158015610a27573d6000803e3d6000fd5b5082806001019350505b838060010194505061084f565b6001600560006101000a81548160ff0219169083151502179055505b50505050505600a165627a7a72305820b73e68785bf35b5c885fc698a29ec9909f1a7c9d897806eed64110fc8781de530029', 
            gas: '4700000'
        }, function (e, contract){
        document.getElementById('price').value = ""
        document.getElementById('num').value = ""
        document.getElementById('winners').value = ""
        console.log(e, contract);
        if (typeof contract.address !== 'undefined') {
            console.log('Contract mined! address: ' + contract.address + ' transactionHash: ' + contract.transactionHash);
            let newItem = {addr:contract.address,per:price,total:num,lucky:winners}
            let original = storage.getItem(current_user+"ISSUE")
            let json = JSON.parse(original)
            if (original == null)
                json=[]
            json.push(newItem)
            let str = JSON.stringify(json)
            storage.setItem(current_user+"ISSUE",str)
            alert('Successfully! The contract address is'+contract.address)
            window.showAllLottery()
            window.showInfo()
        }
    })
}


