using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine;
using NSubstitute;
using Xunit;

namespace CoffeeMakerTests
{
	public class CoffeeMachineTests
	{
		private ISensors _sensors;
		private IActionReceiver _receiver;
		private CoffeeMachineEngine _coffeeMaker;

		public CoffeeMachineTests()
		{
			_sensors = Substitute.For<ISensors>();
			_receiver = Substitute.For<IActionReceiver>();
			_coffeeMaker = new CoffeeMachineEngine(_sensors, _receiver);
		}

		[Fact]
		public void CoffeeMachine_ButtonPushed_SendsAction()
		{
			_sensors.GetBrewButtonStatus.Returns(BrewButtonStatus.PUSHED);

			_coffeeMaker.ProcessSensors();

			_receiver.Received().DoAction(Events.ButtonPushed);

		}

		[Fact]
		public void CoffeeMachine_CaraffeRemoved_SendsAction()
		{
			_sensors.GetCaraffeStatus.Returns(WarmerPlateStatus.WARMER_EMPTY);

			_coffeeMaker.ProcessSensors();

			_receiver.Received().DoAction(Events.PotRemoved);

		}
	}

	public class CoffeeMachineEngine
	{
		private readonly ISensors _sensors;
		private readonly IActionReceiver _receiver;

		public CoffeeMachineEngine(ISensors sensors, IActionReceiver receiver)
		{
			_sensors = sensors;
			_receiver = receiver;
 
		}

		public void ProcessSensors()
		{
			BrewButtonStatus brewButtonStatus = _sensors.GetBrewButtonStatus;
			WarmerPlateStatus warmerPlateStatus = _sensors.GetCaraffeStatus;

			if (brewButtonStatus == BrewButtonStatus.PUSHED)
				_receiver.DoAction(Events.ButtonPushed);

			switch (warmerPlateStatus)
			{
				case WarmerPlateStatus.WARMER_EMPTY:
					_receiver.DoAction(Events.PotRemoved);
					break;
				case WarmerPlateStatus.POT_EMPTY:
					break;
				case WarmerPlateStatus.POT_NOT_EMPTY:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}

	public interface ISensors
	{
		BrewButtonStatus GetBrewButtonStatus { get; set; }
		WarmerPlateStatus GetCaraffeStatus { get; set; }
	}
}
