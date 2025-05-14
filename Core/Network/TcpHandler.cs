using Akka.Actor;
using Akka.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Network
{
    public abstract class TcpHandler : ReceiveActor
    {
        protected readonly IActorRef mConnection;

        protected TcpHandler(IActorRef connection)
        {
            mConnection = connection;

            Receive<Tcp.Received>(received =>
            {
                var msg = Encoding.UTF8.GetString(received.Data.ToArray()).Trim();
                OnReceiveText(msg);
            });
        }

        protected abstract void OnReceiveText(string text);
    }
}
