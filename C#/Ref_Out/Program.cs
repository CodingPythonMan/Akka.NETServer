using System;

namespace Ref_Out
{
    class Program
    {
        static void Main(string[] args)
        {
            int whatIsRef = 3;

            Console.Write("In Main Ref : " + whatIsRef);
            Console.Write("In Main Out : " + whatIsRef);

            setRef(ref whatIsRef);
        }

        static void setRef(ref int whatIsRef)
        {

        }

        static void setOut()
        {

        }
    }
}
