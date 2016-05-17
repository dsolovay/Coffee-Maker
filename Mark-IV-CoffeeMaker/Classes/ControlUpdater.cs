using System;
using CoffeeMachine;
using CoffeeMaker.Enumerations;
using CoffeeMaker.Interfaces;

namespace CoffeeMaker.Classes
{
	public class ControlUpdater : IUpdater
	{
		private readonly IControls _controls;
		private readonly IStateProvider _stateProvider;

		public ControlUpdater(IStateProvider stateProvider, IControls controls)
		{
			if (stateProvider == null) throw new ArgumentNullException(nameof(stateProvider));
			if (controls == null) throw new ArgumentNullException(nameof(controls));
			_stateProvider = stateProvider;
			_controls = controls;
		}

		public void DoUpdate()
		{
			States currentState = _stateProvider.CurrentState;

			switch (currentState)
			{
				case States.Off:
					_controls.SetIndicatorState(IndicatorState.OFF);
					_controls.SetBoilerState(BoilerState.OFF);
					_controls.SetReliefValveState(ReliefValveState.CLOSED);
					_controls.SetWarmerState(WarmerState.OFF);
					break;
				case States.Brew:
					_controls.SetIndicatorState(IndicatorState.OFF);
					_controls.SetBoilerState(BoilerState.ON);
					_controls.SetReliefValveState(ReliefValveState.CLOSED);
					_controls.SetWarmerState(WarmerState.ON);
					break;
				case States.Pause:
					_controls.SetIndicatorState(IndicatorState.OFF);
					_controls.SetBoilerState(BoilerState.ON);
					_controls.SetReliefValveState(ReliefValveState.OPEN);
					_controls.SetWarmerState(WarmerState.ON);
					break;
				case States.Ready:
					_controls.SetIndicatorState(IndicatorState.ON);
					_controls.SetBoilerState(BoilerState.OFF);
					_controls.SetReliefValveState(ReliefValveState.CLOSED);
					_controls.SetWarmerState(WarmerState.ON);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}


