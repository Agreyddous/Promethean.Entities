using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Promethean.Entities.Tests.Helpers;

namespace Promethean.Entities.Tests.Models
{
	[TestClass]
	public class ValidatableModelTests
	{
		[TestInitialize]
		public void Setup() { }

		[TestMethod("Created model should be validated and valid")]
		public void CreateValidValidatableModel()
		{
			TestValidatableModel testValidatableModel = new TestValidatableModel
			{
				Name = Faker.Name.FullName(),
				Email = Faker.Internet.Email(),
				Birthdate = Faker.Identification.DateOfBirth()
			};

			ValidationContext validationContext = new ValidationContext(testValidatableModel);

			testValidatableModel.Validate(validationContext);

			Assert.IsTrue(testValidatableModel.Valid);
		}

		[TestMethod("Created model should be validated and invalid")]
		public void CreateInvalidValidatableModel()
		{
			TestValidatableModel testValidatableModel = new TestValidatableModel
			{
				Name = string.Empty,
				Email = string.Empty,
				Birthdate = null
			};

			ValidationContext validationContext = new ValidationContext(testValidatableModel);

			testValidatableModel.Validate(validationContext);

			Assert.IsFalse(testValidatableModel.Valid);
			Assert.AreEqual(3, testValidatableModel.Notifications.Count);
		}
	}
}