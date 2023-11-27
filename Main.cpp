#include "IDLCompiler.h"

int main()
{
	IDLCompiler* compiler = new IDLCompiler;

	// 1. 데이터 파일을 읽는다.
	compiler->ReadFile("PacketInfo.data");

	// 2. 소스파일을 만든다.
	compiler->MakeProxy();
	compiler->MakeStub();
}