using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

class Program
{
    static void Main()
    {
        // 그럼 어케 만들징? ㅇㅅㅇ
        var hello = new HelloService();
        Console.WriteLine(hello.Greet("DeliKi"));
    }
}