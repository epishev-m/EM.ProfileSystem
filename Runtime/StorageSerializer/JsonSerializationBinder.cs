#nullable enable
namespace EM.Profile
{

using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using IoC;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class JsonSerializationBinder : DefaultSerializationBinder
{
	private readonly IReflector _reflector;

	private readonly Dictionary<string, Type> _typeList;

	#region DefaultSerializationBinder

	public override Type BindToType(string? assemblyName,
		string typeName)
	{
		if (!typeName.StartsWith("#"))
		{
			return base.BindToType(assemblyName, typeName);
		}

		if (!_typeList.TryGetValue(typeName, out var result))
		{
			throw new Exception($"Type is not found with id='{typeName}'");
		}

		return result;
	}

	public override void BindToName(Type serializedType,
		out string? assemblyName,
		out string? typeName)
	{
		assemblyName = default;
		typeName = default;

		var resultReflectionInfo = _reflector.GetReflectionInfo(serializedType);

		if (resultReflectionInfo.Failure)
		{
			return;
		}

		var resultAttributes = resultReflectionInfo.Data.GetAttributes();

		if (resultAttributes.Failure)
		{
			return;
		}

		var attr = resultAttributes.Data.FirstOrDefault(a => a is JsonSerializeAttribute);

		if (attr is not JsonSerializeAttribute jsonAttribute)
		{
			base.BindToName(serializedType, out assemblyName, out typeName);

			return;
		}

		if (!_typeList.TryGetValue(jsonAttribute.Guid, out var realType))
		{
			throw new Exception(
				$"Type map entry is not found with id='{jsonAttribute.Guid}' name={serializedType.FullName}");
		}

		Debug.Assert(realType == serializedType,
			$"Types mismatch: in table found {realType} but actual is {serializedType}");

		assemblyName = null;
		typeName = jsonAttribute.Guid;
	}

	#endregion

	#region JsonSerializationBinder

	public JsonSerializationBinder(IReflector reflector)
	{
		Requires.NotNullParam(reflector, nameof(reflector));

		_reflector = reflector;
		_typeList = LoadTypes();
	}

	private Dictionary<string, Type> LoadTypes()
	{
		var result = new Dictionary<string, Type>(128);
		var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => !assembly.IsDynamic);
		var types = assemblies.SelectMany(assembly => assembly.GetTypes());

		foreach (var type in types)
		{
			var resultReflectionInfo = _reflector.GetReflectionInfo(type);

			if (resultReflectionInfo.Failure)
			{
				//TODO error
				continue;
			}

			var resultAttributes = resultReflectionInfo.Data.GetAttributes();

			if (resultAttributes.Failure)
			{
				//TODO error
				continue;
			}

			var attr = resultAttributes.Data.FirstOrDefault(a => a is JsonSerializeAttribute);

			if (attr is not JsonSerializeAttribute jsonAttribute)
			{
				continue;
			}

			var key = jsonAttribute.Guid;

			if (!result.TryAdd(key, type))
			{
				throw new Exception($"Duplicated GUID found on class '{type.FullName}'");
			}
		}

		return result;
	}

	#endregion
}

}