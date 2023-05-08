namespace EM.Profile
{

using Foundation;

public interface IProfileBindingLifeTime
{
	LifeTime LifeTime
	{
		get;
	}

	IProfileBinding InGlobal();

	IProfileBinding InLocal();

	IProfileBinding SetLifeTime(LifeTime lifeTime);
}

}