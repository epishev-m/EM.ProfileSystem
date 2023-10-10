namespace EM.Profile
{

using System.Collections.Generic;

public sealed class ProfileStorage
{
	public string Version;

	public int Code;

	public long Time;

	public List<IProfileStorageSegment> Segments;
}

}