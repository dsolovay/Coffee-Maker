using System;
using CoffeeMachine;
using CoffeeMaker.Interfaces;

namespace CoffeeMaker.Classes
{
	public class StateUpdater : IUpdater
	{
		private readonly ISensors _sensors;
		private readonly IEventReceiver _eventReceiver;

		public StateUpdater(ISensors sensors, IEventReceiver eventReceiver)
		{
			_sensors = sensors;
			_eventReceiver = eventReceiver;
		}

		public void DoUpdate()
		{
			if (_sensors.GetBrewButtonStatus() == BrewButtonStatus.PUSHED)
			{
				_eventReceiver.HandleEvent(Events.ButtonPushed);
			}

			switch (_sensors.GetWarmerPlateStatus())
			{
				case WarmerPlateStatus.POT_EMPTY:
					_eventReceiver.HandleEvent(Events.PotEmpty);
					break;
				case WarmerPlateStatus.POT_NOT_EMPTY:
					_eventReceiver.HandleEvent(Events.PotPresent);
					break;
				case WarmerPlateStatus.WARMER_EMPTY:
					_eventReceiver.HandleEvent(Events.PotRemoved);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			switch (_sensors.GetBoilerStatus())
			{
				case BoilerStatus.EMPTY:
					_eventReceiver.HandleEvent(Events.BoilerEmpty);
					break;
				case BoilerStatus.NOT_EMPTY:
					_eventReceiver.HandleEvent(Events.BoilerNotEmpty);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}