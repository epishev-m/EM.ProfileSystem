namespace EM.Profile.Editor
{

using System.IO;
using Foundation.Editor;
using UnityEditor;
using UnityEngine;

public sealed class AssistantWindowComponentSave : ScriptableObjectAssistantWindowComponent<SaveConfig>
{
	#region ScriptableObjectAssistantWindowComponent

	public override string Name => "Save";

	protected override string GetCreatePath()
	{
		var path = EditorUtility.SaveFilePanelInProject("Create Save Config", "SaveConfig.asset", "asset", "");

		return path;
	}

	protected override string GetSelectPath()
	{
		var path = EditorUtility.OpenFilePanel("Select Save Config", "Assets", "asset");

		return path;
	}

	protected override void OnGUIConfig()
	{
		OnGUISaveFileExt();
		OnGUIDevSaveFileExt();
		OnGUIEditorMode();
		OnGuiDeleteSave();
	}

	#endregion

	#region AssistantWindowComponentSave

	private void OnGUISaveFileExt()
	{
		EditorGUILayout.LabelField("Save file ext:");

		if (string.IsNullOrWhiteSpace(Config.SaveExt))
		{
			EditorGUILayout.HelpBox("Save file extension not set", MessageType.Warning);
		}

		Config.SaveExt = EditorGUILayout.TextField(Config.SaveExt);
	}

	private void OnGUIDevSaveFileExt()
	{
		EditorGUILayout.LabelField("Dev save file ext:");

		if (string.IsNullOrWhiteSpace(Config.SaveExt))
		{
			EditorGUILayout.HelpBox("Development save file extension not set", MessageType.Warning);
		}

		Config.DevSaveExt = EditorGUILayout.TextField(Config.DevSaveExt);
	}

	private void OnGUIEditorMode()
	{
		EditorGUILayout.Space();
		Config.IsEditorMode = EditorGUILayout.Toggle("Use Editor Mode:", Config.IsEditorMode);
		EditorGUILayout.LabelField("Editor save path:");

		if (string.IsNullOrWhiteSpace(Config.EditorSavePath))
		{
			EditorGUILayout.HelpBox("Editor save path not set", MessageType.Warning);
		}

		GUI.enabled = Config.IsEditorMode;
		Config.EditorSavePath = EditorGUILayout.TextField(Config.EditorSavePath);
		GUI.enabled = true;
	}

	private void OnGuiDeleteSave()
	{
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("Delete save (Edit Mode)"))
		{
			AssetDatabase.DeleteAsset(Config.EditorSavePath);
		}

		if (GUILayout.Button("Delete save"))
		{
			var cashValue = Config.IsEditorMode;
			Config.IsEditorMode = false;
			var filePath = Config.GetFullPath("*");
			Config.IsEditorMode = cashValue;
			var directoryPath = Path.GetDirectoryName(filePath);

			if (!string.IsNullOrWhiteSpace(directoryPath))
			{
				Directory.Delete(directoryPath, true);
			}
		}

		EditorGUILayout.EndHorizontal();
	}

	#endregion
}

}
