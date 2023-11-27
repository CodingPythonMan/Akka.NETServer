using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDLCompiler.Service
{
    public class MakeSourceService
    {
        const string PROXY_CPP_FILE = "Proxy.cpp";
        const string PROXY_H_FILE = "Proxy.h";

        const string SERIALIZE = "Packet";

        public void ReadFile(string ConfigFile)
        {
            StreamReader reader = File.OpenText(ConfigFile);
            string line;
            while((line = reader.ReadLine()!) != null)
            {



            }
        }

        public void MakeProxyFile()
        {
            
        }

        public void MakeStubFile()
        {

        }
    }
}
