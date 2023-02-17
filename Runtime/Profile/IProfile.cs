namespace EM.Profile
{

using Foundation;

public interface IProfile
{
	void Save(object key);

	void Load(object key);

	IProfileBindingLifeTime Bind(object key);

	bool Unbind(object key);

	void Unbind(LifeTime lifeTime);
}

}