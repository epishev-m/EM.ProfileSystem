namespace EM.Profile
{

public interface IStorageLocation
{
	public void Write(string fileName,
		byte[] contents);

	public byte[] Read(string fileName);
}

}