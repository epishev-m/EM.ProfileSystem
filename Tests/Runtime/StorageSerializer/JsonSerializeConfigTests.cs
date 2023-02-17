using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using EM.Foundation;
using EM.Profile;
using NUnit.Framework;

public sealed class JsonSerializeConfigTests
{
	[Test]
	public void JsonSerializeConfig_Constructor_Exception()
	{
		// Arrange
		var actual = false;

		// Act
		try
		{
			var unused = new JsonSerializeConfig(null);
		}
		catch (ArgumentNullException)
		{
			actual = true;
		}

		// Assert
		Assert.IsTrue(actual);
	}

	[Test]
	public void JsonSerializeConfig_CreateSettings()
	{
		// Arrange
		var reflector = new TestReflector();

		// Act
		var config = new JsonSerializeConfig(reflector);
		var actual = config.Settings;

		// Assert
		Assert.NotNull(actual);
	}

	#region Nested

	private sealed class TestReflector : IReflector
	{
		public IReflectionInfo GetReflectionInfo<T>()
		{
			throw new NotImplementedException();
		}

		public IReflectionInfo GetReflectionInfo(Type type)
		{
			return new ReflectionInfo();
		}
	}

	[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
	private sealed class ReflectionInfo : IReflectionInfo
	{
		public ConstructorInfo ConstructorInfo
		{
			get;
		}
		
		public IEnumerable<Type> ConstructorParametersTypes
		{
			get;
		}

		IEnumerable<Attribute> IReflectionInfo.Attributes => new List<TestReflectionInfoAttribute>();
	}

	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
	private sealed class TestReflectionInfoAttribute : Attribute
	{
	}

	#endregion
}