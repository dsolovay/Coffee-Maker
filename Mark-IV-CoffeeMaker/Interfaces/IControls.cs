using CoffeeMachine;

namespace CoffeeMaker.Interfaces
{
	public interface IControls
	{
		void SetBoilerState(BoilerState s);

		void SetWarmerState(WarmerState s);

		void SetIndicatorState(IndicatorState s);

		void SetReliefValveState(ReliefValveState s);
	}
}