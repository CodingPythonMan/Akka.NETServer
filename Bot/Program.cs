using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Akka.Routing;

class Program
{
    static async Task Main()
    {
        // 설계를 천천히 해보자.
        var system = ActorSystem.Create("BotSystem");

        // 모드를 선택해서 입장하는데, 1이 아니면, 봇으로서 채팅을 치게 만들자.


        await Task.Delay(Timeout.InfiniteTimeSpan);
    }
}