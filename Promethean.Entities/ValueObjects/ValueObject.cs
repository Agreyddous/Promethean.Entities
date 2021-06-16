using Promethean.Entities.ValueObjects.Contracts;
using Promethean.Notifications;

namespace Promethean.Entities.ValueObjects
{
	public abstract class ValueObject : Notifiable, IValueObject
	{
		public abstract void Validate();
	}
}