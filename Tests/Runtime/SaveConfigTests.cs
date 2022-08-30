using System.IO;
using EM.Profile;
using NUnit.Framework;
using UnityEngine;

public sealed class SaveConfigTests
{
	[Test]
	public void SaveConfig_EditorModeTrue_GetFullPath()
	{
		// Act
		var saveConfig = ScriptableObject.CreateInstance<SaveConfig>();
		saveConfig.IsEditorMode = true;
		saveConfig.DevSaveExt = "dev";
		saveConfig.EditorSavePath = "save/";
		var actual = saveConfig.GetFullPath("test");

		// Assert
		Assert.AreEqual("save/test.dev", actual);
	}
	
	[Test]
	public void SaveConfig_EditorModeFalse_GetFullPath()
	{
		// Arrange
		const string name = "test";
		
		// Act
		var saveConfig = ScriptableObject.CreateInstance<SaveConfig>();
		saveConfig.IsEditorMode = false;
		saveConfig.DevSaveExt = "dev";
		saveConfig.EditorSavePath = "save/";
		var actual = saveConfig.GetFullPath(name);
		var expected = Path.Combine(Application.persistentDataPath, $"{name}.{saveConfig.DevSaveExt}");

		// Assert
		Assert.AreEqual(expected, actual);
	}
}