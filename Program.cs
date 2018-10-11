using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace CSE445Project2
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 5;
            int K = 3;
            MultiCellBuffer multiCellBuffer = new MultiCellBuffer();
            Thread[] threads = new Thread[N];
   
            //Create K Publishers and corresponding threads
            Publisher[] pubArray = new Publisher[N];
            for(int i = 0; i < K; i++)
            {
                Publisher pub = new Publisher(multiCellBuffer);
                pub.setId("" + i);
                pubArray[i] = pub;

                Thread pubThread = new Thread(pub.publisherFunc);
                threads[i] = pubThread;
                Console.WriteLine("Publisher Thread {0} created.", i);
                
            }

            //Create N Bookstores and create associations between events and their handlers
            BookStore[] bsArray = new BookStore[N];
            for (int i = 0; i < N; i++)
            {
                BookStore bs = new BookStore(multiCellBuffer);
                bs.setId("" + i);
                bsArray[i] = bs;

                Console.WriteLine("Bookstore {0} created.", i);

                //Set event handlers
                for(int j = 0; j < K; j++)
                {
                    bs.orderCreated += new orderEvent(pubArray[j].OrderProcessing);
                    pubArray[j].priceCut += new priceCutEvent(bs.bookOnSale);
                    pubArray[j].callBack += new callBackEvent(bs.confirmation);
                }
            }

            //Begin all Publisher threads
            for (int i = 0; i < K; i++)
            {
                threads[i].Start();
                Console.WriteLine("Publisher Thread {0} started.", i);
            }
        }
    }
}
