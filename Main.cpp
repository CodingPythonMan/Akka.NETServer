#include "IDLCompiler.h"

int main()
{
	IDLCompiler* compiler = new IDLCompiler;

	// 1. ������ ������ �д´�.
	compiler->ReadFile("PacketInfo.data");

	// 2. �ҽ������� �����.
	compiler->MakeProxy();
	compiler->MakeStub();
}