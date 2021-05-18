using System;

namespace Promethean.Entities.Tests.Helpers
{
	public class TestEntity : IEntity<Guid>
	{
		protected TestEntity() { }
		public TestEntity(string name, string email, DateTime birthdate)
		{
			Name = name;
			Email = email;
			Birthdate = birthdate;

			Id = Guid.NewGuid();
			CreatedAt = DateTime.UtcNow;
		}

		public Guid Id { get; private set; }

		public string Name { get; private set; }
		public string Email { get; private set; }
		public DateTime Birthdate { get; private set; }
		public DateTime CreatedAt { get; private set; }
	}
}