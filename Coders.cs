using System;


namespace CSE445Project2
{
    //have both encode and decode functions- one class
    public class Coders
    {
        
        /*
         Encoder is a class or a method in a class: The Encoder will convert an OrderObject into a string. You
         can choose any way to encode the values into a string, as long as you can decode the string back to the
         original order object
         */
        
        public static string encode(OrderClass obj)
        {
           
            string encode;
            encode = obj.getSenderId() + "," + obj.getCardNo().ToString() + ",PUBID:" + obj.getRecieverID()+ ","+ obj.getAmount().ToString() + "," + obj.getUnitPrice().ToString() + "," + obj.getTimeStamp() + "," + obj.getOrderNumber();
           
           
            return encode;
        }
        
        
        /*
         Decoder is a class or a method in a class: The Decoder will convert the encoded string back into the
         OrderObject
         */
        
        public static OrderClass decode(string encode)
        {
            //use delimiter of comma
            char delimit = ',';
            string[] arrayofencode = encode.Split(delimit);

            //back to order object
            OrderClass newobj = new OrderClass(arrayofencode[0], Convert.ToInt32(arrayofencode[1]), arrayofencode[2].Substring(arrayofencode[2].IndexOf(":")+1), Convert.ToInt32(arrayofencode[3]), Convert.ToDouble(arrayofencode[4]), Convert.ToInt32(arrayofencode[6]), arrayofencode[5] );
            
            return newobj;
        }
    }
}
