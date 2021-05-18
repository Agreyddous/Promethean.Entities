using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promethean.Entities.Tests.Helpers;

namespace Promethean.Entities.Tests.Models
{
	[TestClass]
	public class ModelTests
	{
		[TestInitialize]
		public void Setup() { }

		[TestMethod("Parse from Entity to Model, should return a Model with all properties set")]
		public void ParseFromEntityToModel()
		{
			TestEntity entity = new TestEntity(Faker.Name.FullName(), Faker.Internet.Email(), Faker.Identification.DateOfBirth());

			TestModel model = (TestModel)entity;

			Assert.AreEqual(entity.Id, model.Id);
			Assert.AreEqual(entity.Name, model.Name);
			Assert.AreEqual(entity.Email, model.Email);
			Assert.AreEqual(entity.Birthdate, model.Birthdate);
			Assert.AreEqual(entity.CreatedAt, model.CreatedAt);

			Assert.AreEqual(new DateTime(DateTime.Today.Subtract(entity.Birthdate).Ticks).Year, model.Age);
		}

		[TestMethod("Parse from Model to Entity without a set Id, should return an Entity with the same Name, Email and Birthdate, but Id and CreatedAt with their type's respective default value")]
		public void ParseFromModelToEntityWithoutId()
		{
			TestModel model = new TestModel
			{
				Name = Faker.Name.FullName(),
				Email = Faker.Internet.Email(),
				Birthdate = Faker.Identification.DateOfBirth()
			};

			TestEntity entity = model;

			Assert.AreEqual(default, entity.Id);
			Assert.AreEqual(model.Name, entity.Name);
			Assert.AreEqual(model.Email, entity.Email);
			Assert.AreEqual(model.Birthdate, entity.Birthdate);
			Assert.AreEqual(default, entity.CreatedAt);
		}

		[TestMethod("Parse from Model to Entity with a set Id, should return an Entity with the same Id, Name, Email and Birthdate, but CreatedAt with it's type's default value")]
		public void ParseFromModelToEntityWithId()
		{
			TestModel model = new TestModel
			{
				Name = Faker.Name.FullName(),
				Email = Faker.Internet.Email(),
				Birthdate = Faker.Identification.DateOfBirth()
			};

			model.SetId(Guid.NewGuid());

			TestEntity entity = model;

			Assert.AreEqual(model.Id, entity.Id);
			Assert.AreEqual(model.Name, entity.Name);
			Assert.AreEqual(model.Email, entity.Email);
			Assert.AreEqual(model.Birthdate, entity.Birthdate);
			Assert.AreEqual(default, entity.CreatedAt);
		}

		[TestMethod("Parse from Model to Entity with a set model only property, the model only property's value should not be passed")]
		public void ParseFromModelToEntityWithModelOnlyPropertySet()
		{
			TestModel model = new TestModel();

			typeof(TestModel).GetProperty(nameof(model.CreatedAt)).SetValue(model, DateTime.UtcNow);

			TestEntity entity = model;

			Assert.AreEqual(default, entity.CreatedAt);
			Assert.AreNotEqual(model.CreatedAt, entity.CreatedAt);
		}
	}
}