namespace EM.Profile
{

using System.Collections.Generic;

public interface IStorageSegmentReceiver
{
	IEnumerable<IStorageSegment> GetStorageSegments();

	bool Apply(IStorageSegment segment);
}

}