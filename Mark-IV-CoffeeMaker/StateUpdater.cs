using System;
using CoffeeMachine;
using CoffeeMaker.Interfaces;

namespace CoffeeMaker
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
				_eventReceiver.SendEvent(Events.ButtonPushed);
			}

			switch (_sensors.GetWarmerPlateStatus())
			{
				case WarmerPlateStatus.POT_EMPTY:
					_eventReceiver.SendEvent(Events.PotEmpty);
					break;
				case WarmerPlateStatus.POT_NOT_EMPTY:
					_eventReceiver.SendEvent(Events.PotPresent);
					break;
				case WarmerPlateStatus.WARMER_EMPTY:
					_eventReceiver.SendEvent(Events.PotRemoved);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			if (_sensors.GetBoilerStatus() == BoilerStatus.EMPTY)
			{
				_eventReceiver.SendEvent(Events.BoilerEmpty);
			}
		}
	}
}