using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EM.BuildSystem;
using EM.Profile;

internal sealed class ProfileTests
{
	#region Constructor

	[Test]
	public void Profile_Constructor_Exception1()
	{
		// Arrange
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var actual = false;

		// Act
		try
		{
			var unused = new Profile(null, storageLocation, factory, versionConfig);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Profile_Constructor_Exception2()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var actual = false;

		// Act
		try
		{
			var unused = new Profile(storageSerializer, null, factory, versionConfig);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Profile_Constructor_Exception3()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var versionConfig = new TestVersionConfig();
		var actual = false;

		// Act
		try
		{
			var unused = new Profile(storageSerializer, storageLocation, null, versionConfig);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Profile_Constructor_Exception4()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var actual = false;

		// Act
		try
		{
			var unused = new Profile(storageSerializer, storageLocation, factory, null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	#endregion

	#region Bind

	[Test]
	public void Profile_Bind_Exception()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var actual = false;

		// Act
		try
		{
			var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
			var unused = profile.Bind(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Profile_Bind_ReturnBinding()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var key = typeof(string);

		// Act
		var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
		var actual = profile.Bind(key);

		//Assert
		Assert.IsNotNull(actual);
	}

	[Test]
	public void Profile_BindGeneric_ReturnBinding()
	{
		// Act
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
		var actual = profile.Bind<string>();

		//Assert
		Assert.IsNotNull(actual);
	}

	#endregion

	#region Unbind

	[Test]
	public void Profile_Unbind_Exception()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var actual = false;

		// Act
		try
		{
			var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
			var unused = profile.Unbind(null, null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void Profile_Unbind_ReturnFalse()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var key = typeof(string);

		// Act
		var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
		var actual = profile.Unbind(key);

		//Assert
		Assert.IsFalse(actual);
	}

	[Test]
	public void Profile_Unbind_ReturnTrue()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var key = typeof(string);

		// Act
		var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
		var expected = profile.Bind(key).InLocal().To<TesStorageSegmentReceiver>();
		var actualResult = profile.Unbind(key);
		var actual = profile.Bind(key);

		//Assert
		Assert.AreNotEqual(expected, actual);
		Assert.IsTrue(actualResult);
	}

	[Test]
	public void Profile_UnbindGeneric_ReturnFalse()
	{
		// Act
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
		var actual = profile.Unbind<string>();

		//Assert
		Assert.IsFalse(actual);
	}

	[Test]
	public void Profile_UnbindGeneric_ReturnTrue()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var key = typeof(string);

		// Act
		var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
		var expected = profile.Bind(key).InLocal().To<TesStorageSegmentReceiver>();
		var actualResult = profile.Unbind<string>();
		var actual = profile.Bind(key);

		//Assert
		Assert.AreNotEqual(expected, actual);
		Assert.IsTrue(actualResult);
	}

	[Test]
	public void Profile_UnbindPredicate()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var key = typeof(string);

		// Act
		var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
		var expected = profile.Bind(key).InLocal().To<TesStorageSegmentReceiver>();
		profile.Unbind(b => b.Key.Equals(key));
		var actual = profile.Bind(key);

		//Assert
		Assert.AreNotEqual(expected, actual);
	}

	[Test]
	public void Profile_UnbindAll()
	{
		// Arrange
		var storageSerializer = new TestStorageSerializer();
		var storageLocation = new TestStorageLocation();
		var factory = new TestStorageSegmentReceiverFactory();
		var versionConfig = new TestVersionConfig();
		var key = typeof(string);

		// Act
		var profile = new Profile(storageSerializer, storageLocation, factory, versionConfig);
		var expected = profile.Bind(key).InLocal().To<TesStorageSegmentReceiver>();
		profile.UnbindAll();
		var actual = profile.Bind(key);

		//Assert
		Assert.AreNotEqual(expected, actual);
	}

	#endregion

	#region Nested

	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
	private sealed class TesStorageSegmentReceiver : IStorageSegmentReceiver
	{
		public IEnumerable<IStorageSegment> GetStorageSegments()
		{
			throw new NotImplementedException();
		}

		public bool Apply(IStorageSegment segment)
		{
			throw new NotImplementedException();
		}
	}

	private sealed class TestStorageSerializer : IStorageSerializer
	{
		public byte[] Save(Storage storage)
		{
			throw new NotImplementedException();
		}

		public Storage Load(byte[] bytes)
		{
			throw new NotImplementedException();
		}
	}

	private sealed class TestStorageLocation : IStorageLocation
	{
		public void Write(string fileName,
			byte[] contents)
		{
			throw new NotImplementedException();
		}

		public byte[] Read(string fileName)
		{
			throw new NotImplementedException();
		}
	}

	private sealed class TestStorageSegmentReceiverFactory : IStorageSegmentReceiverFactory
	{
		public IStorageSegmentReceiver Get(Type type)
		{
			throw new NotImplementedException();
		}
	}

	[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
	private sealed class TestVersionConfig : IVersionConfig
	{
		public string Version
		{
			get;
		}

		public int Code
		{
			get;
		}
	}

	#endregion
}