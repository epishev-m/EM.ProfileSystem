namespace EM.Profile
{

public interface IStorageSegment
{
	void Apply(IStorageSegmentReceiver receiver);
}

}