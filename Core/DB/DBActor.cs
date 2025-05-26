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
        }
    }
}