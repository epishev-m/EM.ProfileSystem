namespace EM.Profile
{

using System;
using System.Collections.Generic;
using System.Linq;
using BuildSystem;
using Foundation;

public class Profile :
	Binder,
	IProfile
{
	private readonly IStorageSerializer _storageSerializer;

	private readonly IStorageLocation _storageLocation;

	private readonly IStorageSegmentReceiverFactory _factory;

	private readonly IVersionConfig _versionConfig;

	#region IProfile

	public void Save(object key)
	{
		if (GetBinding(key) is not ProfileBinding binding)
		{
			return;
		}

		var receivers = GetReceivers(binding.Values);
		var segments = GetSegments(receivers);

		var storage = new Storage
		{
			Version = _versionConfig.Version,
			Code = _versionConfig.Code,
			Time = 0,
			Segments = segments.ToList()
		};

		var bytes = _storageSerializer.Save(storage);
		_storageLocation.Write(key.ToString(), bytes);
	}

	public void Load(object key)
	{
		var bytes = _storageLocation.Read(key.ToString());
		var storage = _storageSerializer.Load(bytes);

		if (storage == null)
		{
			return;
		}

		if (GetBinding(key) is not ProfileBinding binding)
		{
			return;
		}

		var receivers = GetReceivers(binding.Values).ToArray();

		foreach (var segment in storage.Segments)
		{
			ReceiverApply(segment, receivers);
		}
	}

	public IProfileBindingLifeTime Bind(object key)
	{
		return base.Bind(key) as IProfileBindingLifeTime;
	}

	public bool Unbind(object key)
	{
		return base.Unbind(key);
	}

	public void Unbind(LifeTime lifeTime)
	{
		Unbind(binding =>
		{
			var diBinding = (IProfileBindingLifeTime) binding;
			var result = diBinding.LifeTime == lifeTime;

			return result;
		});
	}

	#endregion
	
	#region Binder

	protected override IBinding GetRawBinding(object key,
		object name)
	{
		return new ProfileBinding(key, name, BindingResolver);
	}

	#endregion

	#region Profile

	public Profile(IStorageSerializer storageSerializer,
		IStorageLocation storageLocation,
		IStorageSegmentReceiverFactory factory,
		IVersionConfig versionConfig)
	{
		_storageSerializer = storageSerializer;
		_storageLocation = storageLocation;
		_factory = factory;
		_versionConfig = versionConfig;
	}

	private IEnumerable<IStorageSegmentReceiver> GetReceivers(IEnumerable<object> values)
	{
		var receivers = new List<IStorageSegmentReceiver>();

		foreach (var value in values)
		{
			var receiver = _factory.Get((Type) value);

			if (receiver != null)
			{
				receivers.Add(receiver);
			}
		}

		return receivers;
	}

	private static IEnumerable<IStorageSegment> GetSegments(IEnumerable<IStorageSegmentReceiver> receivers)
	{
		var segments = new List<IStorageSegment>();

		foreach (var receiver in receivers)
		{
			var segment = receiver.GetStorageSegments();
			segments.AddRange(segment);
		}

		return segments;
	}

	private static void ReceiverApply(IStorageSegment segment,
		IEnumerable<IStorageSegmentReceiver> receivers)
	{
		foreach (var receiver in receivers)
		{
			if (receiver.Apply(segment))
			{
				break;
			}
		}
	}

	#endregion
}

}