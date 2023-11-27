#include "IDLCompiler.h"
#include <iostream>
#include <cstring>

IDLCompiler::~IDLCompiler()
{
	delete[] _FileData;
	
	for (int i = 0; i < _Line; i++)
		delete[] _AnalyzeData[i];
	delete[] _AnalyzeData;
}

bool IDLCompiler::ReadFile(const char* fileName)
{
	FILE* file;

	fopen_s(&file, fileName, "r");

	if (file == nullptr)
		return true;

	fseek(file, 0, SEEK_END);
	int length = ftell(file);
	fseek(file, 0, SEEK_SET);

	_FileData = new char[length + 1];
	if (_FileData == nullptr)
		return true;

	fread_s(_FileData, length+1, 1, length+1, file);
	_FileData[length] = '\0';
	
	fclose(file);

	return false;
}

bool IDLCompiler::AnalyzeFile()
{
	// 우선 세미콜론 개수를 찾은 다음, 줄 수를 정해준다.
	_Line = 0;
	char* data = _FileData;
	while (1)
	{
		if (*data == ';')
		{
			_Line++;
		}

		if (*data == '\0')
			break;

		data++;
	}

	_AnalyzeData = new char*[_Line];
	data = _FileData;
	// Skip 된 데이터부터 차례차례 정리 필요
	for (int i = 0; i < _Line; i++)
	{
		// 올바른 문자 앞까지 Skip 후, 세미콜론을 찾아서 그 전까지 멤카피
		if (SkipToNextWord(&data))
			return true;

		int length = 0;
		while (1)
		{
			if (*data == ';')
			{
				break;
			}
			data++;
			length++;
		}

		_AnalyzeData[i] = new char[length];
		memcpy_s(_AnalyzeData[i], length, data-length, length);
	}

	return false;
}

bool IDLCompiler::MakeProxy(const char* OutputName, const char* SerializeBufferName)
{
	int NameLen = strlen(OutputName) + 10;
	char* ProxyCPPName = new char[NameLen];

	int len = 0;
	memcpy(ProxyCPPName, OutputName, strlen(OutputName));
	len += (int)strlen(OutputName);
	memcpy(ProxyCPPName + len, "/Proxy.cpp", strlen("/Proxy.cpp"));
	ProxyCPPName[NameLen] = '\0';

	FILE* file;
	fopen_s(&file, ProxyCPPName, "w");

	// 줄당 Argument 갯수도 알아야한다.


	delete[] ProxyCPPName;

	return false;
}

bool IDLCompiler::MakeStub(const char* OutputName)
{
	return false;
}

bool IDLCompiler::SkipToNextWord(char** chrBufferPtr)
{
	while (1)
	{
		char* chrBuffer = *chrBufferPtr;

		// 주석처리 스킵 
		if (*chrBuffer == '/')
		{
			if (*(chrBuffer + 1) == '\0')
			{
				break;
			}

			if (*(chrBuffer + 1) == '/')
			{
				// New Line 처리까지 스킵
				while (1)
				{
					if (*chrBuffer == 0x0d && *(chrBuffer + 1) == 0x0a)
						break;

					if (*chrBuffer == '\0')
						break;

					(*chrBufferPtr)++;
				}
			}
		}

		// 스페이스, 탭, 엔터코드
		// 0x20 스페이스
		// 0x08 백스페이스
		// 0x09 탭
		// 0x0a 라인 피드
		// 0x0d 캐리지 리턴
		if (*chrBuffer == 0x20 || *chrBuffer == 0x08 || *chrBuffer == 0x09 ||
			*chrBuffer == 0x0a || *chrBuffer == 0x0d || *chrBuffer == '{' || *chrBuffer == '"')
		{
			(*chrBufferPtr)++;
		}
		else
		{
			break;
		}
	}

	if (**chrBufferPtr == '\0')
		return true;

	return false;
}