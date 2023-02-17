namespace EM.Profile
{

using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(SaveConfig), menuName = "Game/Save Config")]
public class SaveConfig : ScriptableObject,
	ISaveConfig
{
	[SerializeField]
	private string _saveExt = "save";

	[SerializeField]
	private string _devSaveExt = "devsave";

#if UNITY_EDITOR
	[SerializeField]
	private bool _isEditorMode = true;

	[SerializeField]
	private string _editorSavePath = "Assets/Save";
#endif

	#region ISaveConfig

	public string SaveExt
	{
		get => _saveExt;
#if UNITY_EDITOR
		set => _saveExt = value;
#endif
	}

	public string DevSaveExt
	{
		get => _devSaveExt;
#if UNITY_EDITOR
		set => _devSaveExt = value;
#endif
	}

#if UNITY_EDITOR
	public bool IsEditorMode
	{
		get => _isEditorMode;
		set => _isEditorMode = value;
	}

	public string EditorSavePath
	{
		get => _editorSavePath;
		set => _editorSavePath = value;
	}
#endif

	public string GetFullPath(string fileName)
	{
		var saveExt = Debug.isDebugBuild ? DevSaveExt : SaveExt;
#if UNITY_EDITOR
		var path = Path.Combine(IsEditorMode ? EditorSavePath : Application.persistentDataPath, $"{fileName}.{saveExt}");
#else
		var path = Path.Combine(Application.persistentDataPath, $"{fileName}.{saveExt}");
#endif

		return path;
	}

	#endregion
}

}