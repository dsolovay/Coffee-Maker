using CoffeeMachine;

namespace CoffeeMaker.Interfaces
{
	public interface IEventReceiver
	{
		void HandleEvent(Events theEvent);
	}
}