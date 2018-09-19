#include "orderclass.hpp"
using System;

namespace CSE445Project2
{
    
    public class OrderClass
    {
        //private vars
    /*
     - senderId: the identity of the sender, you can use thread name or thread id.
     - cardNo: an integer that represents a credit card number.
     - receiverID: the identity of the receiver, you can use thread name or a unique name defined for a publisher.
     - amount: an integer that represents the number of books to order.
     - unit price: a double that represents the unit price of the book received from the publisher.
     */
        private string senderId;
        private Int32 cardNo;
        private string revieverID;
        private Int32 amount;
        private double unitPrice,
        
        public OrderClass(string senderId, Int32 cardNo, Int32 amount) //constructor
        {
            this.senderId = senderId;
            
            this.cardNo = cardNo;
            
            this.recieverID = recieverID;
            
            this.amount = amount;
            
            this.unitPrice = unitPrice;
            
        }
        //getters
  
        public string getSenderId()
        {
            return senderId;
        }
        public Int32 getCardNo()
        {
            return cardNo;
        }
        
        public string getRecieverID()
        {
            return recieverID;
        }
        public Int32 getAmount()
        {
            return amount;
        }
        public double getUnitPrice()
        {
            return uniteprice;
        }
        
        
    }
}
