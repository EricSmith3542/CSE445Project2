/*
 MultiCellBuffer class is used for the communication between the bookstores (clients) and the
 publishers (servers): This class has n data cells (group project, n = 3).
 
 The number of cells available must be less than (<) the max number of bookstores in your experiment.
 To write data into and to read data from one of the available cells, setOneCell and getOneCell methods can
 be defined. You must use a semaphore of value n to manage the availability of the cells.
 You must use an additional lock mechanism to provide read or write permissions for a cell.
 You cannot use a queue for thebuffer, which is a different data structure.
 The semaphore allows a bookstore to see the availability of the cells, while the lock mechanism allows the agent to gain the right to write into one of the buffer cells.
 The Publisher can read buffer cells at the same time. Synchronization/monitor is required for read/write and write/write overlap.
 */

#include "MultiCellBuffer.hpp"
using System;
using System.Threading;

namespace CSE445Project2
{
    
   public class MultiCellBuffer
    {
        private int buffercount;
        private int N = 5;
        //gets N from BookStore supposed to be 5
        private int n = 3;
        
        private OrderClass obj = new OrderClass();
        
        private Semaphore read;
        private Semaphore write;
        
        public MultiCellBuffer(int construct)
        {
            
       /* The lock statement obtains the mutual-exclusion lock for a given object, executes a statement block, and then releases the lock. While a lock is held, the thread that holds the lock can again obtain and release the lock. Any other thread is blocked from obtaining the lock and waits until the lock is released.
        */
        //there is a lock method wtf I could have used this for CSE 330...
           //semaphores are same as above woahhh
            // example implementation : Semaphore semaphoreObject = new Semaphore(initialCount: 0, maximumCount: 5);
           
            
            //lock new object or the n value... i think it might be n but unsure??????
            lock (obj)
            {
           
                
                if (n < N)
                {
                    //still working on what will be inital and max
                    write = new Semaphore(inital, max);
                    read = new Semaphore(inital,max);
                   //create string cells
                    //user string array?
                    
                }
                else
                   { }
            }
        }
        
        //set and get one cells to be implemented
        //still working on it
        
    }
}
