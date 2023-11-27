class IDLCompiler {
public:
	~IDLCompiler();

	bool ReadFile(const char* fileName);
	bool AnalyzeFile();

	bool MakeProxy(const char* OutputName, const char* SerializeBufferName);
	bool MakeStub(const char* OutputName);

private:
	bool SkipToNextWord(char** chrBufferPtr);

	char* _FileData;

	// 데이터 저장
	char** _AnalyzeData;
	int _Line;
};