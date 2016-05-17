using CoffeeMachine;

namespace CoffeeMaker.Interfaces
{
	public interface IEventReceiver
	{
		void SendEvent(Events action);
	}
}