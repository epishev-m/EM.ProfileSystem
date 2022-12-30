namespace EM.Profile
{

public interface IStorageSegmentReceiver
{
	IStorageSegment GetStorageSegments();

	bool Apply(IStorageSegment segment);
}

}