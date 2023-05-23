namespace EM.Profile
{

using Foundation;

public sealed class ProfileBinding : Binding,
	IProfileBindingLifeTime,
	IProfileBinding
{
	#region IProfileBindingLifeTime

	public LifeTime LifeTime
	{
		get;
		private set;
	} = LifeTime.None;

	public IProfileBinding InGlobal()
	{
		Requires.ValidOperation(LifeTime == LifeTime.None, this);

		LifeTime = LifeTime.Global;

		return this;
	}

	public IProfileBinding InLocal()
	{
		Requires.ValidOperation(LifeTime == LifeTime.None, this);

		LifeTime = LifeTime.Local;

		return this;
	}

	public IProfileBinding SetLifeTime(LifeTime lifeTime)
	{
		Requires.ValidOperation(LifeTime == LifeTime.None, this);

		LifeTime = lifeTime;

		return this;
	}

	#endregion

	#region IProfileBinding

	public new IProfileBinding To<T>()
		where T : class, IStorageSegmentSaver
	{
		Requires.ValidOperation(LifeTime != LifeTime.None, this);
		Requires.ValidOperation(Values == null, this, nameof(Values));

		return base.To<T>() as IProfileBinding;
	}

	#endregion

	#region ProfileBinding

	public ProfileBinding(object key,
		object name,
		Resolver resolver)
		: base(key, name, resolver)
	{
	}

	#endregion
}

}