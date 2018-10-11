
using System;
using System.Threading;

namespace CSE445Project2
{
    
   public class MultiCellBuffer
    {
        private int N = 5;
        //gets N from BookStore supposed to be 5
        private int n = 3;
        private int numOfUsedCells = -1;
        private int[] orderNums;

        private string[] objArray; //= new OrderClass();
        private Semaphore sem;
        private ReaderWriterLock loque = new ReaderWriterLock();

        //Initialize the MultiCellBuffer
        public MultiCellBuffer()
        {
            loque.AcquireWriterLock(Timeout.Infinite);
            try
            {
                sem = new Semaphore(n, n);
                objArray = new string[n];
                for (int i = 0; i < n; i++)
                {
                    objArray[i] = "";
                }

                orderNums = new int[N];
            }
            finally
            {
                loque.ReleaseWriterLock();
            }
        }
        
        //Fill a cell with a string of an encoded order
        public void setObject(string encodedOrder)
        {
            sem.WaitOne();
            loque.AcquireWriterLock(Timeout.Infinite);
            try
            {
                numOfUsedCells++;
                objArray[numOfUsedCells] = encodedOrder;
                string[] split = encodedOrder.Split(',');
                string pubId = split[2].Substring(split[2].IndexOf(":") + 1);
                orderNums[Int32.Parse(pubId)]++;
            }
            finally
            {
                loque.ReleaseWriterLock();
            }
        }

        //Get the total number of orders that have been created for the publisher with id = index
        public int getOrderNumber(int index)
        {
            return orderNums[index];
        }

        //Retrieve data from a cell, only if it corresponds to the correct Publisher
        public string getObject(string pubId)
        {
            string data = "ERROR";
            
            loque.AcquireReaderLock(Timeout.Infinite);
            try
            {
                //Check to see if there is data in the MultiCellBuffer
                if(numOfUsedCells >= 0)
                {
                    //Parse string for the publisher id related to the order
                    string[] split = objArray[numOfUsedCells].Split(',');
                    string cellPubId = split[2].Substring(split[2].IndexOf(":") + 1);

                    //If the publisher ids match, retrieve the data and free the cell
                    if(cellPubId == pubId)
                    {
                        loque.UpgradeToWriterLock(Timeout.Infinite);

                        data = objArray[numOfUsedCells];

                        numOfUsedCells--;
                        sem.Release();
                    }
                    else
                    {
                        data = "ERROR";
                    }
                    
                }
            }
            finally
            {
                loque.ReleaseLock();
            }
            return data;
        }
        
    }
}
