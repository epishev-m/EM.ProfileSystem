namespace EM.Profile
{

using System;

public interface IStorageSegmentReceiverFactory
{
	IStorageSegmentReceiver Get(Type type);
}

}