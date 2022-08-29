namespace EM.Profile
{

using Foundation;

public sealed class ProfileBinding :
	Binding,
	IProfileBindingLifeTime,
	IProfileBinding
{
	#region IProfileBindingLifeTime

	public LifeTime LifeTime
	{
		get;
		private set;
	} = LifeTime.External;

	public IProfileBinding InGlobal()
	{
		Requires.ValidOperation(LifeTime == LifeTime.External, this, nameof(InGlobal));

		LifeTime = LifeTime.Global;

		return this;
	}

	public IProfileBinding InLocal()
	{
		Requires.ValidOperation(LifeTime == LifeTime.External, this, nameof(InLocal));

		LifeTime = LifeTime.Local;

		return this;
	}

	#endregion

	#region IProfileBinding

	public new IProfileBinding To<T>()
		where T : class, IStorageSegmentReceiver
	{
		Requires.ValidOperation(LifeTime != LifeTime.External, this, nameof(To));
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