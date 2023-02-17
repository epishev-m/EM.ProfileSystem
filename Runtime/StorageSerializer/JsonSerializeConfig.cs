namespace EM.Profile
{

using Foundation;
using Newtonsoft.Json;
using UnityEngine;

public sealed class JsonSerializeConfig
{
	public readonly JsonSerializerSettings Settings;

	private readonly IReflector _reflector;

	#region JsonProfileConfig

	public JsonSerializeConfig(IReflector reflector)
	{
		Requires.NotNull(reflector, nameof(reflector));

		_reflector = reflector;
		Settings = CreateSettings();
	}

	private JsonSerializerSettings CreateSettings()
	{
		var serializerSettings = new JsonSerializerSettings()
		{
			Formatting = Debug.isDebugBuild ? Formatting.Indented : Formatting.None,
			ReferenceLoopHandling = ReferenceLoopHandling.Error,
			NullValueHandling = NullValueHandling.Ignore,
			TypeNameHandling = TypeNameHandling.Auto,
			DefaultValueHandling = DefaultValueHandling.Ignore,
			SerializationBinder = new JsonSerializationBinder(_reflector)
		};

		return serializerSettings;
	}

	#endregion
}

}