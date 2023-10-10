namespace EM.Profile
{

public interface IProfileBinding
{
	IProfileBinding To<T>()
		where T : class, IStorageSegmentSaver;
}

}