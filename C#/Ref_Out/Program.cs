using System;

namespace Ref_Out
{
    class Program
    {
        static void Main(string[] args)
        {
            int whatIsRef = 3;
            int whatIsOut = 103;

            Console.WriteLine("In Main Before Ref : " + whatIsRef);
            Console.WriteLine("In Main Out : " + whatIsOut);

            SetRef(ref whatIsRef);
            SetOut(out whatIsOut);

            Console.WriteLine("In Main After Ref : " + whatIsRef);
            Console.WriteLine("In Main After Out : " + whatIsOut);
        }

        static void SetRef(ref int whatIsRefVal)
        {
            whatIsRefVal += 1;

            Console.WriteLine("In Method Ref : " + whatIsRefVal);
        }

        static void SetOut(out int whatIsOutVal)
        {
            whatIsOutVal = 5;
            whatIsOutVal += 1;

            Console.WriteLine("In Method Out : " + whatIsOutVal);
        }
    }
}
