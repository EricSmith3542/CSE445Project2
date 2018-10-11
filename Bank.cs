using System;
using System.Collections.Generic;

namespace CSE445Project2
{
    public class Bank
    {
        public static List<string> cardList = new List<string>();
        public static List<double> fundsList = new List<double>();

        /*Validate orders by checking that the credit card has been registered and that it has sufficient funds*/
        public static string validate(Int32 creditCardNumber, double amount)
        {
            ServiceReference1.ServiceClient serviceClient = new ServiceReference1.ServiceClient();
            int index = -1;
            for (int i = 0; i < cardList.Count && index == -1; i++)
            {
                if (creditCardNumber == Int32.Parse(serviceClient.Decrypt(cardList[i])))
                {
                    index = i;
                }
            }

            if (index != -1 && fundsList[index] > amount)
            {
                fundsList[index] -= amount;
                return "valid";
            }
            else
            {
                return "not valid";
            }
        }

        /*
         * Generate card
         * Add card number to list of cards and funds, starting with no funds
         * Return the new card
         */
        public static Int32 registerCreditCard()
        {
            ServiceReference1.ServiceClient serviceClient = new ServiceReference1.ServiceClient();
            Random rnd = new Random();
            bool valid;
            Int32 newCard;
            string encryptedCard = "";
            do
            {
                valid = true;
                newCard = rnd.Next(5000, 10000);
                for (int i = 0; i < cardList.Count && valid; i++)
                {
                    if(newCard == Int32.Parse(serviceClient.Decrypt(cardList[i])))
                    {
                        valid = false;
                    }
                }
            } while (!valid);
            encryptedCard = serviceClient.Encrypt(newCard.ToString());
            cardList.Add(encryptedCard);
            fundsList.Add(0);
            return newCard;
        }

        /*
         * Adds funds and returns true if the creditCard is found, otherwise returns false
         */
        public static bool deposit(Int32 creditCardNumber, double amount)
        {
            ServiceReference1.ServiceClient serviceClient = new ServiceReference1.ServiceClient();
            int index = -1;
            for (int i = 0; i < cardList.Count && index == -1; i++)
            {
                if(creditCardNumber == Int32.Parse(serviceClient.Decrypt(cardList[i])))
                {
                    index = i;
                }
            }

            if(index != -1)
            {
                fundsList[index] += amount;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
