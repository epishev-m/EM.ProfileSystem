namespace EM.Profile
{

using System.Text;
using Newtonsoft.Json;

public class JsonStorageSerializer : IStorageSerializer
{
	private readonly JsonSerializeConfig _config;

	private readonly Encoding _encoding = new UTF8Encoding(false);

	#region IStorageSerializer

	public byte[] Save(ProfileStorage profileStorage)
	{
		var json = JsonConvert.SerializeObject(profileStorage, _config.Settings);
		var result = _encoding.GetBytes(json);

		return result;
	}

	public ProfileStorage Load(byte[] bytes)
	{
		if (bytes == null || bytes.Length == 0)
		{
			return null;
		}

		var json = _encoding.GetString(bytes);
		var storage = JsonConvert.DeserializeObject<ProfileStorage>(json, _config.Settings);

		return storage;
	}

	#endregion

	#region JsonStorageSerializer

	public JsonStorageSerializer(JsonSerializeConfig config)
	{
		_config = config;
	}

	#endregion
}

}