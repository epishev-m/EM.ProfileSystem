namespace EM.Profile
{

using System.Collections.Generic;

public sealed class Storage
{
	public string Version;

	public int Code;

	public long Time;

	public List<IStorageSegment> Segments;
}

}