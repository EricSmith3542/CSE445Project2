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
        private string recieverID;
        private Int32 amount;
        private double unitPrice;
        private string timeStamp;
        private Int32 orderNumber;
        
        public OrderClass(string senderId, Int32 cardNo, string recieverID, Int32 amount, double unitPrice, Int32 num) //constructor
        {
            this.senderId = senderId;
            
            this.cardNo = cardNo;
            
            this.recieverID = recieverID;
            
            this.amount = amount;
            
            this.unitPrice = unitPrice;

            orderNumber = num;

            timeStamp = DateTime.Now.ToString("h:mm:ss tt");
        }

        public OrderClass(string senderId, Int32 cardNo, string recieverID, Int32 amount, double unitPrice, Int32 num, string time) //constructor
        {
            this.senderId = senderId;

            this.cardNo = cardNo;

            this.recieverID = recieverID;

            this.amount = amount;

            this.unitPrice = unitPrice;

            orderNumber = num;

            timeStamp = time;
        }
        //getters
        public Int32 getOrderNumber()
        {
            return orderNumber;
        }
        public string getTimeStamp()
        {
            return timeStamp;
        }
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
            return unitPrice;
        }
        
        
    }
}
