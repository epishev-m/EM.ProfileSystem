using System.IO;
using EM.Assistant.Editor;
using UnityEditor;
using UnityEngine.UIElements;

namespace EM.Profile.Editor
{

public sealed class SaveAssistantComponent : ScriptableObjectAssistantComponent<SaveConfig>
{
	private TextField _saveExtText;

	private TextField _devSaveExtText;

	private Toggle _useEditMode;

	private TextField _editorSavePath;

	private VisualElement _deleteButtonsPanel;

	#region SaveAssistantComponent

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

	protected override void OnInitialized()
	{
		base.OnInitialized();
		CreateSaveExtText();
		CreateDevSaveExtText();
		CreateUseEditMode();
		CreateEditorSavePath();
		CreateButtons();
	}

	protected override void OnComposed()
	{
		base.OnComposed();
		Root.AddChild(_saveExtText)
			.AddChild(_devSaveExtText)
			.AddChild(_useEditMode)
			.AddChild(_editorSavePath)
			.AddChild(_deleteButtonsPanel);
	}

	protected override void SetConfig(SaveConfig config)
	{
		base.SetConfig(config);
		_saveExtText.value = Config.SaveExt;
		_devSaveExtText.value = Config.DevSaveExt;
		_useEditMode.value = Config.IsEditorMode;
		_editorSavePath.value = Config.EditorSavePath;
	}

	#endregion

	#region ScriptableObjectAssistantComponent

	private void CreateSaveExtText()
	{
		_saveExtText = new TextField("Save file ext:")
			.AddValueChangedCallback<TextField, string>(value => Config.SaveExt = value);

		if (Config != null)
		{
			_saveExtText.value = Config.SaveExt;
		}
	}

	private void CreateDevSaveExtText()
	{
		_devSaveExtText = new TextField("Dev save file ext:")
			.AddValueChangedCallback<TextField, string>(value => Config.DevSaveExt = value);

		if (Config != null)
		{
			_devSaveExtText.value = Config.DevSaveExt;
		}
	}

	private void CreateUseEditMode()
	{
		_useEditMode = new Toggle("Use Editor Mode:")
			.AddValueChangedCallback<Toggle, bool>(OnToggleValueChanged);

		if (Config != null)
		{
			_useEditMode.value = Config.IsEditorMode;
		}
	}

	private void CreateEditorSavePath()
	{
		_editorSavePath = new TextField("Editor save path:")
			.AddValueChangedCallback<TextField, string>(value => Config.EditorSavePath = value);

		if (Config == null)
		{
			return;
		}

		_editorSavePath.value = Config.EditorSavePath;
		_editorSavePath.SetEnabled(Config.IsEditorMode);
	}

	private void CreateButtons()
	{
		var deleteSaveEditModeButton = new Button(OnDeleteSaveEditModeButtonClicked)
			.SetText("Delete save (Edit Mode)")
			.SetStyleFlexBasisPercent(50);

		var deleteSaveButton = new Button(OnDeleteSaveButtonClicked)
			.SetText("Delete save")
			.SetStyleFlexBasisPercent(50);

		_deleteButtonsPanel = new VisualElement()
			.SetStyleFlexDirection(FlexDirection.Row)
			.SetStyleJustifyContent(Justify.SpaceAround)
			.SetStyleMargin(0, 10, 0, 0)
			.AddChild(deleteSaveEditModeButton)
			.AddChild(deleteSaveButton);
	}

	private void OnToggleValueChanged(bool value)
	{
		Config.IsEditorMode = value;
		_editorSavePath.SetEnabled(value);
	}

	private void OnDeleteSaveEditModeButtonClicked()
	{
		AssetDatabase.DeleteAsset(Config.EditorSavePath);
	}

	private void OnDeleteSaveButtonClicked()
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

	#endregion
}

}