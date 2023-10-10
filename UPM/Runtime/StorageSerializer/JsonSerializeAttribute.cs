namespace EM.Profile
{

using System;
using System.Text;

public sealed class JsonSerializeAttribute : Attribute
{
	public readonly string Guid;

	public JsonSerializeAttribute(string guid)
	{
		Guid = new StringBuilder("#").Append(guid).ToString();
	}
}

}