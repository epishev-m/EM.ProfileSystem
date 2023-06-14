namespace EM.Profile
{

using System;
using Foundation;

public interface IStorageSegmentReceiverFactory
{
	Result<IStorageSegmentSaver> Get(Type type);
}

}