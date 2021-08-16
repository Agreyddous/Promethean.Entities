using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promethean.Entities.Models.Extensions;
using Promethean.Entities.Tests.Helpers;

namespace Promethean.Entities.Tests.Models.Extensions
{
	[TestClass]
	public class ModelEnumerableExtensionsTests
	{
		[TestInitialize]
		public void Setup() { }

		[TestMethod("Parse a collection of Entity objects to a collection of Model objects, should return a collection of Model objects with all properties set")]
		public void ParseCollectionFromEntityToModel()
		{
			List<TestEntity> entities = new List<TestEntity>();

			for (int index = 0; index < 10; index++)
				entities.Add(new TestEntity(Faker.Name.FullName(), Faker.Internet.Email(), Faker.Identification.DateOfBirth()));

			List<TestModel> models = entities.ToModels<TestEntity, TestModel>().ToList();

			Assert.AreEqual(entities.Count, models.Count);

			for (int index = 0; index < entities.Count; index++)
				Assert.AreEqual(entities[index].Id, models[index].Id);
		}

		[TestMethod("Parse a collection of Model objects to a collection of Entity objects, should return a collection of Entity objects with all properties set")]
		public void ParseCollectionFromModelToEntity()
		{
			List<TestModel> models = new List<TestModel>();

			for (int index = 0; index < 10; index++)
				models.Add((TestModel)new TestEntity(Faker.Name.FullName(), Faker.Internet.Email(), Faker.Identification.DateOfBirth()));

			List<TestEntity> entities = models.ToEntities<TestEntity, TestModel>().ToList();

			Assert.AreEqual(models.Count, entities.Count);

			for (int index = 0; index < models.Count; index++)
				Assert.AreEqual(models[index].Id, entities[index].Id);
		}
	}
}