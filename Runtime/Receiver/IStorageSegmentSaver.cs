namespace EM.Profile
{

public interface IStorageSegmentSaver
{
	IStorageSegment Save();

	bool Load(IStorageSegment segment);
}

}