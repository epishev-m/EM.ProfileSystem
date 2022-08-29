namespace EM.Profile
{

using System;

public sealed class JsonSerializeAttribute : Attribute
{
	public readonly string Guid;

	public JsonSerializeAttribute(string guid)
	{
		Guid = guid;
	}
}

}