namespace EM.Profile
{

public interface IStorageSerializer
{
	byte[] Save(ProfileStorage profileStorage);

	ProfileStorage Load(byte[] bytes);
}

}