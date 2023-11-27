#include "IDLCompiler.h"
#include <iostream>

void IDLCompiler::ReadFile(const char* fileName)
{
	FILE* file;

	fopen_s(&file, fileName, "r");

	if (file == nullptr)
		return;

	fseek(file, 0, SEEK_END);
	int length = ftell(file);
	fseek(file, 0, SEEK_SET);

	fileData = (char*)malloc(length + 1);
	if (fileData == nullptr)
		return;

	fread_s(fileData, length + 1, 1, length, file);
	fileData[length] = '\0';
	
	fclose(file);
}