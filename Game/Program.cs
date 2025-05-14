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
        var system = ActorSystem.Create("GameSystem");

        // 처음에 DB 연결
        var dbActorRef = system.ActorOf(Props.Create(() => new DBActor()).WithRouter(new SmallestMailboxPool(30)), "DBPool");

        // 가벼운 채팅이 되는 것부터 만들자.
        // TCP 연결이 되는 것부터 만들어야 한다.



        await Task.Delay(Timeout.InfiniteTimeSpan);
    }
}