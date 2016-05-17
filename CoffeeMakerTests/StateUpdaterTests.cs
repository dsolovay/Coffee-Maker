using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine;
using CoffeeMaker;
using CoffeeMaker.Classes;
using CoffeeMaker.Interfaces;
using NSubstitute;
using Xunit;

namespace CoffeeMakerTests
{
	public class StateUpdaterTests
	{
		private readonly IUpdater _sut;
		readonly ISensors _sensors;
		readonly IEventReceiver _eventReceiver;

		public StateUpdaterTests()
		{
			_sensors = Substitute.For<ISensors>();
			_eventReceiver = Substitute.For<IEventReceiver>();
			_sut = new StateUpdater(_sensors, _eventReceiver);
		}

		[Fact]
		public void StateUpdater_ButtonPushed_Sends()
		{
			_sensors.GetBrewButtonStatus().Returns(BrewButtonStatus.PUSHED);

			_sut.DoUpdate();

			_eventReceiver.Received().SendEvent(Events.ButtonPushed);
		}

		[Fact]
		public void StateUpdater_ButtonNotPushed_DoesNotSend()
		{
			_sensors.GetBrewButtonStatus().Returns(BrewButtonStatus.NOT_PUSHED);

			_sut.DoUpdate();

			_eventReceiver.DidNotReceive().SendEvent(Events.ButtonPushed);
		}

		[Fact]
		public void StateUpdater_PotEmpty_Sends()
		{
			_sensors.GetWarmerPlateStatus().Returns(WarmerPlateStatus.POT_EMPTY);

			_sut.DoUpdate();

			_eventReceiver.Received().SendEvent(Events.PotEmpty);

		}

		[Fact]
		public void StateUpdater_PotPresent_Sends()
		{
			_sensors.GetWarmerPlateStatus().Returns(WarmerPlateStatus.POT_NOT_EMPTY);

			_sut.DoUpdate();

			_eventReceiver.Received().SendEvent(Events.PotPresent);
		}

		[Fact]
		public void StateUpdater_PotRemoved_Sends()
		{
			_sensors.GetWarmerPlateStatus().Returns(WarmerPlateStatus.WARMER_EMPTY);

			_sut.DoUpdate();

			_eventReceiver.Received().SendEvent(Events.PotRemoved);
		}

		[Fact]
		public void StateUpdater_BoilerEmpty_Sends()
		{
			_sensors.GetBoilerStatus().Returns(BoilerStatus.EMPTY);

			_sut.DoUpdate();

			_eventReceiver.Received().SendEvent(Events.BoilerEmpty);
		}
	}
}
