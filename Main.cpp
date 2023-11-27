#include "IDLCompiler.h"
#include "Parser.h"
#include <iostream>

int main()
{
	Parser* parser = Parser::GetInstance();
	IDLCompiler* compiler = new IDLCompiler;

	// 1. 데이터 파일을 읽는다.
	parser->LoadFile("Config.dat");

	parser->GetValueStr("OutputPath", &parser->OutputPath);
	parser->GetValueStr("FileName", &parser->FileName);
	parser->GetValueStr("SerializeBufferName", &parser->SerializeBufferName);

	if(true == compiler->ReadFile(parser->FileName))
		std::cout << "[Read File] Error! \n";

	// 2. 안 쪽 데이터를 한 줄씩 정리해준다.
	if (true == compiler->AnalyzeFile())
		std::cout << "[Analyze File] Error! \n";

	// 3. 각 줄마다 대응하는 소스파일을 만든다.
	if (true == compiler->MakeProxy(parser->OutputPath, parser->SerializeBufferName))
		std::cout << "[Make Proxy] Error! \n";
	
	if (true == compiler->MakeStub(parser->OutputPath))
		std::cout << "[Make Stub] Error! \n";
}