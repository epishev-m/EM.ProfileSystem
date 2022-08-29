namespace EM.Profile
{

public interface ISaveConfig
{
	string SaveExt
	{
		get;
	}

	string DevSaveExt
	{
		get;
	}

#if UNITY_EDITOR
	string EditorSavePath
	{
		get;
	}

	bool IsEditorMode
	{
		get;
	}
#endif

	string GetFullPath(string fileName);
}

}