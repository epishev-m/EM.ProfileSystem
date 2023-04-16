namespace EM.Profile.Editor
{

using System.IO;
using Assistant.Editor;
using UnityEditor;
using UnityEngine;

public sealed class AssistantComponentSave : ScriptableObjectAssistantComponent<SaveConfig>
{
	#region ScriptableObjectAssistantWindowComponent

	public override string Name => "Save";

	protected override string GetCreatePath()
	{
		var path = EditorUtility.SaveFilePanelInProject("Create Save Settings", "SaveSettings.asset", "asset", "");

		return path;
	}

	protected override string GetSelectPath()
	{
		var path = EditorUtility.OpenFilePanel("Select Save Settings", "Assets", "asset");

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

		if (string.IsNullOrWhiteSpace(Settings.SaveExt))
		{
			EditorGUILayout.HelpBox("Save file extension not set", MessageType.Warning);
		}

		Settings.SaveExt = EditorGUILayout.TextField(Settings.SaveExt);
	}

	private void OnGUIDevSaveFileExt()
	{
		EditorGUILayout.LabelField("Dev save file ext:");

		if (string.IsNullOrWhiteSpace(Settings.SaveExt))
		{
			EditorGUILayout.HelpBox("Development save file extension not set", MessageType.Warning);
		}

		Settings.DevSaveExt = EditorGUILayout.TextField(Settings.DevSaveExt);
	}

	private void OnGUIEditorMode()
	{
		EditorGUILayout.Space();
		Settings.IsEditorMode = EditorGUILayout.Toggle("Use Editor Mode:", Settings.IsEditorMode);
		EditorGUILayout.LabelField("Editor save path:");

		if (string.IsNullOrWhiteSpace(Settings.EditorSavePath))
		{
			EditorGUILayout.HelpBox("Editor save path not set", MessageType.Warning);
		}

		GUI.enabled = Settings.IsEditorMode;
		Settings.EditorSavePath = EditorGUILayout.TextField(Settings.EditorSavePath);
		GUI.enabled = true;
	}

	private void OnGuiDeleteSave()
	{
		EditorGUILayout.Space();
		EditorGUILayout.BeginHorizontal();

		if (GUILayout.Button("Delete save (Edit Mode)"))
		{
			AssetDatabase.DeleteAsset(Settings.EditorSavePath);
		}

		if (GUILayout.Button("Delete save"))
		{
			var cashValue = Settings.IsEditorMode;
			Settings.IsEditorMode = false;
			var filePath = Settings.GetFullPath("*");
			Settings.IsEditorMode = cashValue;
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
