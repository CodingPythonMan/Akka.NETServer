using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.IO;

namespace Core.Network
{
    public class ServerActor : ReceiveActor
    {
        public ServerActor()
        {
            var tcp = Context.System.Tcp();
        }
    }
}
