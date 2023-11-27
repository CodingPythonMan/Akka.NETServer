#include "IDLCompiler.h"
#include "Parser.h"
#include <iostream>

int main()
{
	Parser* parser = Parser::GetInstance();
	IDLCompiler* compiler = new IDLCompiler;

	// 1. ������ ������ �д´�.
	parser->LoadFile("Config.dat");

	parser->GetValueStr("OutputPath", &parser->OutputPath);
	parser->GetValueStr("FileName", &parser->FileName);
	parser->GetValueStr("SerializeBufferName", &parser->SerializeBufferName);

	if(true == compiler->ReadFile(parser->FileName))
		std::cout << "[Read File] Error! \n";

	// 2. �� �� �����͸� �� �پ� �������ش�.
	if (true == compiler->AnalyzeFile())
		std::cout << "[Analyze File] Error! \n";

	// 3. �� �ٸ��� �����ϴ� �ҽ������� �����.
	if (true == compiler->MakeProxy(parser->OutputPath, parser->SerializeBufferName))
		std::cout << "[Make Proxy] Error! \n";
	
	if (true == compiler->MakeStub(parser->OutputPath))
		std::cout << "[Make Stub] Error! \n";
}