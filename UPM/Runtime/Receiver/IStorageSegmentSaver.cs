namespace EM.Profile
{

public interface IStorageSegmentSaver
{
	IProfileStorageSegment Save();

	bool Load(IProfileStorageSegment segment);
}

}