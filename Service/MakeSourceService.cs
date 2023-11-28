using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDLCompiler.Service
{
    public class MakeSourceService
    {
        const int NORMAL_ARGUMENT = 2;
        const int EXCEPT_ARGUMENT = 1;

        const string TYPE = "Type";
        const string VARIABLE = "Variable";


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
                        // 이 곳에서 반드시 넣어줘야하는 변수를 추가해 넣는다. 
                        int SemiIndex = lines[i].IndexOf(';');
                        string Valid = lines[i].Substring(j, SemiIndex);
                        _ValidLines.Add(sb.ToString());
                        break;
                    }
                }
            }
        }

        private Dictionary<string, List<string>> AnalyzeLine(string Line)
        {
            Dictionary<string, List<string>> result = new();

            int Start = Line.IndexOf('(') + 1;
            int End = Line.IndexOf(')');

            string ArgumentStr = Line.Substring(Start, End - Start);

            if (true == string.IsNullOrEmpty(ArgumentStr))
                return result;

            var Arguments = ArgumentStr.Split(',');

            result.Add(TYPE, new List<string>());
            result.Add(VARIABLE, new List<string>());

            for (int i=0; i<Arguments.Length; i++)
            {
                var Consist = Arguments[i].Trim().Split(' ');

                if(Consist.Length > NORMAL_ARGUMENT)
                {
                    string Type = $"{Consist[0]} ";
                    for(int j=1; j<Consist.Length - 1; j++)
                    {
                        Type += $"{Consist[j]} ";
                    }
                    Type = Type.Substring(0, Type.Length - 1);
                    result[TYPE].Add($"const {Type}&");
                    result[VARIABLE].Add(Consist[Consist.Length - 1]);
                }
                else
                {
                    result[TYPE].Add($"const {Consist[0]}&");
                    result[VARIABLE].Add(Consist[1]);
                }
            }

            return result;
        }

        private string ReconsistLine(string Line)
        {
            StringBuilder ReLine = new("void ");

            int Start = Line.IndexOf('(') + 1;
            var result = AnalyzeLine(Line);

            if (result.Count > 0)
                ReLine.Insert(Start, "const MyList<Session*>& SessionList, ");
            else
                ReLine.Insert(Start, "const MyList<Session*>& SessionList");

            // 각 매개변수 앞에 const Type& 이걸 붙여줘야 한다.


            return ReLine.ToString();
        }

        public void MakeProxyHeaderFile()
        {
            StringBuilder sb = new();

            sb.AppendLine("#pragma once\n");

            for (int i=0; i<_ValidLines.Count; i++)
            {
                sb.AppendLine($"void {_ValidLines[i]}");
            }

            File.WriteAllText($"{PROXY_H_FILE}", sb.ToString());
        }

        public void MakeProxyCPPFile()
        {
            StringBuilder sb = new();

            sb.AppendLine($"#include \"{PROXY_H_FILE}\"");
            sb.AppendLine($"#include \"Packet.h\"");
            sb.AppendLine($"#include \"Session.h\"\n");

            for (int i = 0; i < _ValidLines.Count; i++)
            {
                // 세미콜론 제거
                sb.AppendLine($"void {_ValidLines[i].Substring(0, _ValidLines[i].Length - 1)}");
                sb.AppendLine("{");

                var Dict = AnalyzeLine(_ValidLines[i]);
                if (Dict[VARIABLE].Count > EXCEPT_ARGUMENT)
                {
                    sb.Append("\tPacket packet;\n");
                    sb.Append("\tpacket ");
                    for (int j = EXCEPT_ARGUMENT; j < Dict[VARIABLE].Count; j++)
                    {
                        sb.Append($"<< {Dict[VARIABLE][j]} ");
                    }
                    sb[sb.Length - 1] = ';';
                    sb.AppendLine();

                    sb.AppendLine($"\tSendMessage({Dict[VARIABLE][0]}, &packet);");
                }
                

                sb.AppendLine("}\n");
            }

            File.WriteAllText($"{PROXY_CPP_FILE}", sb.ToString());
        }

        public void MakeStubHeaderFile()
        {

        }

        public void MakeStubCPPFile()
        {

        }
    }
}
