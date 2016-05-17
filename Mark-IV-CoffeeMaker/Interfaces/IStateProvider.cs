using CoffeeMaker.Enumerations;

namespace CoffeeMaker.Interfaces
{
	public interface IStateProvider
	{
		States CurrentState { get; }
	}
}