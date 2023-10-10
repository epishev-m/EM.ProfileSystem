namespace EM.Profile
{

using System;
using System.IO;
using UnityEngine;

public class FileStorageLocation : IStorageLocation
{
	private readonly ISaveConfig _saveConfig;

	#region IStorageLocation

	public void Write(string fileName,
		byte[] contents)
	{
		var filePath = _saveConfig.GetFullPath(fileName);
		var directoryPath = Path.GetDirectoryName(filePath);

		try
		{
			if (!Directory.Exists(directoryPath))
			{
				Directory.CreateDirectory(directoryPath);
			}

			File.WriteAllBytes(filePath, contents);
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
		}
	}

	public byte[] Read(string fileName)
	{
		var path = _saveConfig.GetFullPath(fileName);

		if (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
		{
			return null;
		}

		try
		{
			var bytes = File.ReadAllBytes(path);

			return bytes;
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}

		return null;
	}

	#endregion

	#region FileStorageLocation

	public FileStorageLocation(ISaveConfig saveConfig)
	{
		_saveConfig = saveConfig;
	}

	#endregion
}

}