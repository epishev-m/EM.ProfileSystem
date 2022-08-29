namespace EM.Profile
{

using System;
using IoC;

public sealed class StorageSegmentReceiverFactory :
	IStorageSegmentReceiverFactory
{
	private readonly IDiContainer _diContainer;

	#region IStorageSegmentReceiverFactory

	public IStorageSegmentReceiver Get(Type type)
	{
		return _diContainer.GetInstance(type) as IStorageSegmentReceiver;
	}

	#endregion

	#region ReceiverFactory

	public StorageSegmentReceiverFactory(IDiContainer diContainer)
	{
		_diContainer = diContainer;
	}

	#endregion
}

}