using IDLCompiler.Service;

const string PACKET_FILE = "PacketInfo.dat";
MakeSourceService service = new();

// 1. 파일 읽기
service.ReadFile(PACKET_FILE);

// 2. 소스파일 생성하기
service.MakeProxyFile();
service.MakeStubFile();