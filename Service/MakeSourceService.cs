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

        List<string> _ValidLines = new();

        // Valid Line 걸러내는 줄.
        public void AnalyzeFile(string ConfigFile)
        {
            //Temp
            ConfigFile = $"D:\\Tools\\gitsource\\IDLCompiler\\{ConfigFile}";

            StreamReader reader = File.OpenText(ConfigFile);
            string text = reader.ReadToEnd();

            string[] lines = text.Split(Environment.NewLine);

            for(int i=0; i<lines.Length; i++)
            {
                for(int j=0; j < lines[i].Length; j++)
                {
                    if (char.IsLetter(lines[i][j]))
                    {
                        _ValidLines.Add(lines[i]);

                        break;
                    }
                }
            }
        }

        public Dictionary<string, List<string>> AnalyzeLine(string Line)
        {
            Dictionary<string, List<string>> result = new();

            int Start = Line.IndexOf('(') + 1;
            int End = Line.IndexOf(')');

            string str = Line.Substring(Start, End - Start);



            result.Add("Type", new List<string>());
            result.Add("Argument", new List<string>());

            return result;
        }

        public void MakeProxyHeaderFile()
        {
            StringBuilder sb = new();

            sb.AppendLine("#pragma once\n");

            sb.AppendLine("class Proxy {");
            sb.AppendLine("public:");
            for (int i=0; i<_ValidLines.Count; i++)
            {
                sb.AppendLine(_ValidLines[i]);
            }
            sb.AppendLine("};");

            File.WriteAllText($"{PROXY_H_FILE}", sb.ToString());
        }

        public void MakeProxyCPPFile()
        {
            StringBuilder sb = new();

            sb.AppendLine($"#include \"{PROXY_H_FILE}\"");
            sb.AppendLine($"#include \"Packet.h\"\n");
            sb.AppendLine($"#include \"Session.h\"\n");

            for (int i = 0; i < _ValidLines.Count; i++)
            {
                // 세미콜론 제거
                sb.AppendLine(_ValidLines[i].Remove(';'));
                sb.AppendLine("{");

                var Dict = AnalyzeLine(_ValidLines[i]);

                sb.AppendLine("Packet ");
                sb.AppendLine("}\n");
            }

            File.WriteAllText($"{PROXY_H_FILE}", sb.ToString());
        }

        public void MakeStubHeaderFile()
        {

        }

        public void MakeStubCPPFile()
        {

        }
    }
}
