using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CSE445Project2
{
    public delegate void orderEvent();
    class Publisher
    {
        //initialize varibales
        private Int32 locCharge, numBooks;
        private double bookPrice, prevPrice;
        public event priceCutEvent priceCut;
        public event callBackEvent callBack;
        private string id;
        private MultiCellBuffer mb;
        private double taxPercent = .1;


        public Publisher(MultiCellBuffer m)
        {
            //get random loc charge, book price, and num books
            Random rng = new Random();
            locCharge = rng.Next(5, 20);
            bookPrice = rng.Next(150,200);
            numBooks = rng.Next(400, 600);
            mb = m;
        }

        //set ids
        public void setId(string i) { id = i; }

        public Int32 getLocCharge()
        {
            return locCharge;
        }
        
        //adjust price function
        public bool adjustPrice(int numOrders, int weekDay, int remainingBooks, int cutCount)
        {
            //Store the previous price
            prevPrice = bookPrice;

            //check all num orders and give different price values- for less th\an 15, over 45
            
            if (numOrders < 15)
            {
                bookPrice = bookPrice * .90;
            }
            else if (numOrders > 45)
            {
                bookPrice = bookPrice * 1.10;
            }
            //check what day of the week it is and change price accordingly
            if (weekDay == 1 || weekDay == 3)
            {
                bookPrice = bookPrice * .90;
            }
            else if (weekDay == 5 || weekDay == 6)
            {
                bookPrice = bookPrice * 1.10;
            }
            //check reaminder books if less then 20 or greater than 50 and adjust price
            if (remainingBooks > 50)
            {
                bookPrice = bookPrice * .90;
            }
            else if (remainingBooks < 20)
            {
                bookPrice = bookPrice * 1.10;
            }

            if(bookPrice < prevPrice)
            {
                Console.WriteLine("PRICE CUT EVENT: Publisher-{0}, cut #{1}", id, cutCount);
                priceCut(bookPrice, id);
                return true;
            }
            return false;
        }

        public void publisherFunc()
        {
            
            int cutCount = 0;
            int weekday = 1;

            while(cutCount < 10)
            {
                //call adjust price given the needed parameters
                bool cut = adjustPrice(mb.getOrderNumber(Int32.Parse(id)), weekday, numBooks, cutCount);
                //Thread.Sleep(1500);

                if (cut)
                {
                    cutCount++;
                }
                 
                if(weekday == 7)
                {
                    weekday = 1;
                }
                else
                {
                    weekday++;
                }
            }


            Console.WriteLine("Publisher {0} terminated.", id);
        }

        public void OrderProcessing()
        {
            string encodedString = mb.getObject(id);
            //check to see if the retrieved object pertains to the current publisher
            //if so, process order
            if(encodedString != "ERROR")
            {
                OrderClass order = Coders.decode(encodedString);
                Console.WriteLine("Publisher {0} is processing order {1} sent from Bookstore {2}", order.getRecieverID(), order.getOrderNumber(), order.getSenderId());
               
                double totalCharge = (order.getUnitPrice() * order.getAmount()) * (1 + taxPercent) + getLocCharge();
                string validity = Bank.validate(order.getCardNo(), totalCharge);

                if(validity == "valid")
                {
                    numBooks -= order.getAmount();
                }
                callBack(validity, order.getSenderId());
            }
        }
    }
}
