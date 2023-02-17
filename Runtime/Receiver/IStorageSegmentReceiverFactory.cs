namespace EM.Profile
{

using System;

public interface IStorageSegmentReceiverFactory
{
	IStorageSegmentSaver Get(Type type);
}

}