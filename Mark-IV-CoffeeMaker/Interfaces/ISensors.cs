using CoffeeMachine;

namespace CoffeeMaker.Interfaces
{
	public interface ISensors
	{
		WarmerPlateStatus GetWarmerPlateStatus();
		BoilerStatus GetBoilerStatus();
		BrewButtonStatus GetBrewButtonStatus();
	}
}