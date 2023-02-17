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
		public Result<IReflectionInfo> GetReflectionInfo<T>()
		{
			throw new NotImplementedException();
		}

		public Result<IReflectionInfo> GetReflectionInfo(Type type)
		{
			var reflectionInfo = new ReflectionInfo();

			return new SuccessResult<IReflectionInfo>(reflectionInfo);
		}
	}

	[SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
	[SuppressMessage("ReSharper", "CollectionNeverUpdated.Local")]
	private sealed class ReflectionInfo : IReflectionInfo
	{
		public Result<ConstructorInfo> GetConstructorInfo()
		{
			throw new NotImplementedException();
		}

		public Result<IEnumerable<Type>> GetConstructorParamTypes()
		{
			throw new NotImplementedException();
		}

		public Result<IEnumerable<Attribute>> GetAttributes()
		{
			var attributes = new List<TestReflectionInfoAttribute>();

			return new SuccessResult<IEnumerable<Attribute>>(attributes);
		}
	}

	[SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
	private sealed class TestReflectionInfoAttribute : Attribute
	{
	}

	#endregion
}