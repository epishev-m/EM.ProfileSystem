namespace EM.Profile
{

public interface IStorageSerializer
{
	byte[] Save(Storage storage);

	Storage Load(byte[] bytes);
}

}