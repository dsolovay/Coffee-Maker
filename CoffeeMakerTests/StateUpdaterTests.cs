using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine;
using CoffeeMaker.Interfaces;
using NSubstitute;
using Xunit;

namespace CoffeeMakerTests
{
	public class StateUpdaterTests
	{
		private IUpdater _sut;
		ISensors _sensors;
		IEventReceiver _eventReceiver;

		public StateUpdaterTests()
		{
			_sensors = Substitute.For<ISensors>();
			_eventReceiver = Substitute.For<IEventReceiver>();
			_sut = new StateUpdater(_sensors, _eventReceiver);
		}

		[Fact]
		public void StateUpdater_ButtonPushed_Sends()
		{
			_sensors.GetBrewButtonStatus.Returns(BrewButtonStatus.PUSHED);

			_sut.DoUpdate();

			_eventReceiver.Received().DoAction(Events.ButtonPushed);
		}

		[Fact]
		public void StateUpdater_ButtonNotPushed_DoesNotSend()
		{
			_sensors.GetBrewButtonStatus.Returns(BrewButtonStatus.NOT_PUSHED);

			_sut.DoUpdate();

			_eventReceiver.DidNotReceive().DoAction(Events.ButtonPushed);
		}

		[Fact]
		public void StateUpdater_CaraffeOff_Sends()
		{
			_sensors.GetCaraffeStatus.Returns(WarmerPlateStatus.POT_EMPTY)
		}
	}

	internal class StateUpdater : IUpdater
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
			if (_sensors.GetBrewButtonStatus == BrewButtonStatus.PUSHED)
			{
				_eventReceiver.DoAction(Events.ButtonPushed);
			}

		}
	}
}
