using Akka.Actor;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DB
{
    public class DBActor : ReceiveActor
    {
        private readonly IDatabase mRedis;

        public DBActor()
        {
            var redis = ConnectionMultiplexer.Connect("localhost");
            mRedis = redis.GetDatabase();

            ReceiveAsync<LoadAccount>(async msg =>
            {
                var data = await mRedis.StringGetAsync(msg.AccountId);
                Sender.Tell(data.HasValue ? data.ToString() : null);
            });

            ReceiveAsync<SaveAccount>(async msg =>
            {
                await mRedis.StringSetAsync(msg.AccountId, msg.JsonData);
            });
        }
    }
}