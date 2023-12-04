// Get funds from users and withdraw fund for owner
// Set minimum funding value in USD

//Chain link are only watching on Goerli testnet and real networks, there's no chainlink in JS EVM

// SPDX-License-Identifier: MIT
pragma solidity ^0.8.8;
import "./PriceConverterLib.sol";

error NotOwner();


contract WobbleStore {
    address public immutable owner; 
    constructor() {
        owner = msg.sender;
    }
    using PriceConverterLib for uint256;
    uint256 public constant minUSD = 0 * 1e18; //18 decimals

    address[] public funders;

    mapping(address => uint256) public addressToAmount;

    function getOwner() public view returns (address) {
        return owner;
    }

    function fund() public payable {
        msg.value;
        require(msg.value.getConversionPrice() > minUSD, "Didnt send enough");
        funders.push(msg.sender);
        addressToAmount[msg.sender] = msg.value;
    }

    function withdraw() public onlyOnwer {
        for (uint256 i = 0; i < funders.length; i = i + 1) {
            address funder = funders[i];
            addressToAmount[funder] = 0;
        }
        funders = new address[](0);
        payable(msg.sender).transfer(address(this).balance);
        (bool callSuccess, bytes memory data) = payable(msg.sender).call{
            value: address(this).balance
        }("");
        require(callSuccess, "Call failed");
    }

    modifier onlyOnwer() {
        if (msg.sender != owner) {
            revert NotOwner();
        }
        _; 
    }

    receive() external payable {
        fund();
    }

    fallback() external payable {
        fund();
    }
}