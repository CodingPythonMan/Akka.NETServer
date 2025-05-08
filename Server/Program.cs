using Akka.Actor;
using Akka.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        // 기본 ActorSystem 설정
        var baseConfig = ConfigurationFactory.ParseString("akka.actor.provider = \"local\"");
        using var system = ActorSystem.Create("TestSystem", baseConfig);

        var tasks = new List<Task>();

        // 10개의 서로 다른 설정을 동시에 주입
        for (int i = 0; i < 10; i++)
        {
            int index = i; // 클로저 방지
            tasks.Add(Task.Run(() =>
            {
                var config = ConfigurationFactory.ParseString($@"
akka.test.module{index}.enabled = true
");

                Console.WriteLine($"[Thread {index}] Injecting config...");
                system.Settings.InjectTopLevelFallback(config);
            }));
        }

        Task.WaitAll(tasks.ToArray());

        // 최종 구성 확인
        Console.WriteLine("\n=== Final Configuration Snapshot ===");
        for (int i = 0; i < 10; i++)
        {
            var value = system.Settings.Config.GetString($"akka.test.module{i}.enabled", "false");
            Console.WriteLine($"akka.test.module{i}.enabled = {value}");
        }
    }
}