using System;
using System.Threading;
namespace CSE445Project2
{
    //Add a parameter to the priceCutEvent for the Publisher
    public delegate void priceCutEvent(double pr, string pubId); // Define a delegate 
    public delegate void callBackEvent(string confirmation, string bsId);
    public class BookStore
    {
        static Random rng = new Random(); // To generate random numbers 
        //public static event priceCutEvent priceCut; // Link event to delegate
        private static Int32 cardNumber;
        private string id;
        private static Int32 bookPrice = 10;
        private MultiCellBuffer mb;
        public event orderEvent orderCreated;
        public static Int32 orderCount;

        //getters/setters
        public string getId() { return id; }
        public Int32 getCardNumber() { return cardNumber; }
        public void setId(string i) { id = i; }
        public void setCardNumber() { cardNumber = Bank.registerCreditCard(); }
        public Int32 getPrice() { return bookPrice; }

        //give money to each bookstore after registering their credit card
        public BookStore(MultiCellBuffer m)
        {
            Random rng = new Random();
            setCardNumber();
            Bank.deposit(cardNumber, rng.Next(1700000, 2323000));
            mb = m;
        }
        //event handler for price cuts
        //creates order
        public void bookOnSale(double price, string pubId)
        {
            Int32 numBooks;
            if (price <= 75)
            {
                numBooks = 100;
            }
            else if (price > 75 && price < 150)
            {
                numBooks = 50;
            }
            else
            {
                numBooks = 25;
            }
            orderCount++;
            OrderClass order = new OrderClass(id, cardNumber, pubId, numBooks, price, orderCount);
            string obj = Coders.encode(order);


            mb.setObject(obj);
            Console.WriteLine("ORDER {3}: Bookstore {0} is creating an order for {1} books from Publisher {2} at {4}", id, numBooks, pubId, order.getOrderNumber(), order.getTimeStamp());
            orderCreated();
        }

        //sends order confirmation to bookstore
        public void confirmation(string confirmation, string bsId)
        {
            if(id == bsId)
                Console.WriteLine(confirmation);
        }
        
    }
    
}
