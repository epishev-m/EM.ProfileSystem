using System;
using EM.Foundation;
using EM.Profile;
using NUnit.Framework;

internal sealed class ProfileBindingTests
{
	#region Constructor

	[Test]
	public void ProfileBinding_Constructor_Exception()
	{
		// Act
		var actual = false;

		try
		{
			var unused = new ProfileBinding(null, null, null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		//Assert
		Assert.IsTrue(actual);
	}

	#endregion

	#region LifeTime

	[Test]
	public void ProfileBinding_InGlobal()
	{
		// Arrange
		var key = typeof(Test);
		var binding = new ProfileBinding(key, null, null);

		// Act
		binding.InGlobal();
		var actual = binding.LifeTime;

		// Assert
		Assert.AreEqual(LifeTime.Global, actual);
	}

	[Test]
	public void ProfileBinding_InGlobal_IsInvalidOperationException()
	{
		// Arrange
		var actual = false;
		var key = typeof(Test);
		var binding = new ProfileBinding(key, null, null);
		binding.InGlobal();

		// Act
		try
		{
			binding.InGlobal();
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void ProfileBinding_InLocal()
	{
		// Arrange
		var key = typeof(Test);
		var binding = new ProfileBinding(key, null, null);

		// Act
		binding.InLocal();
		var actual = binding.LifeTime;

		// Assert
		Assert.AreEqual(LifeTime.Local, actual);
	}

	[Test]
	public void ProfileBinding_InLocal_IsInvalidOperationException()
	{
		// Arrange
		var actual = false;
		var key = typeof(Test);
		var binding = new ProfileBinding(key, null, null);
		binding.InLocal();

		// Act
		try
		{
			binding.InLocal();
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	#endregion

	#region To

	[Test]
	public void ProfileBinding_ToGeneric_NotLifeTimeException()
	{
		// Arrange
		var actual = false;
		var key = typeof(Test);
		var binding = new ProfileBinding(key, null, null);

		// Act
		try
		{
			binding.To<Test>();
		}
		catch (InvalidOperationException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void ProfileBinding_ToGeneric_ReturnIDIBinder()
	{
		// Arrange
		var key = typeof(Test);
		var binding = new ProfileBinding(key, null, null);

		// Act
		var actual = binding.InGlobal().To<Test>();

		// Assert
		Assert.AreEqual(binding, actual);
	}

	[Test]
	public void ProfileBinding_ToGeneric_Resolver()
	{
		// Arrange
		var actual = false;
		var key = typeof(Test);
		var binding = new ProfileBinding(key, null, Resolver);

		// Act
		binding.InGlobal().To<Test>();

		void Resolver(IBinding unused) => actual = true;

		// Assert
		Assert.IsTrue(actual);
	}

	#endregion

	#region Nested

	private class Test : IStorageSegmentSaver
	{
		#region IStorageSegmentReceiver

		public IProfileStorageSegment Save()
		{
			throw new NotImplementedException();
		}

		public bool Load(IProfileStorageSegment segment)
		{
			throw new NotImplementedException();
		}

		#endregion
	}

	#endregion
}