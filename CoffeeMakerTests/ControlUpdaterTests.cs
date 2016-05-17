using CoffeeMachine;
using CoffeeMaker;
using CoffeeMaker.Classes;
using CoffeeMaker.Enumerations;
using CoffeeMaker.Interfaces;
using NSubstitute;
using Xunit;

namespace CoffeeMakerTests
{
	public class ControlUpdaterTests
	{
		private IUpdater _sut;
		private IStateProvider _stateProvider;
		private IControls _controls;

		public ControlUpdaterTests()
		{
			_stateProvider = Substitute.For<IStateProvider>();
			_controls = Substitute.For<IControls>();
			_sut = new ControlUpdater(_stateProvider, _controls);
		}

		[Fact]
		public void CoffeeUpdater_Brew_SetsIndictorOff()
		{
			_stateProvider.CurrentState.Returns(States.Brew);

			_sut.DoUpdate();

			_controls.Received().SetIndicatorState(IndicatorState.OFF);
		}

		[Fact]
		public void CoffeeUpdater_Brew_SetsBoilerOn()
		{
			_stateProvider.CurrentState.Returns(States.Brew);

			_sut.DoUpdate();

			_controls.Received().SetBoilerState(BoilerState.ON);
		}

		[Fact]
		public void CoffeeUpdater_Brew_SetValveClosed()
		{
			_stateProvider.CurrentState.Returns(States.Brew);

			_sut.DoUpdate();

			_controls.Received().SetReliefValveState(ReliefValveState.CLOSED);
		}

		[Fact]
		public void CoffeeUpdater_Brew_SetWarmerOn()
		{
			_stateProvider.CurrentState.Returns(States.Brew);

			_sut.DoUpdate();

			_controls.Received().SetWarmerState(WarmerState.ON);
		}

		[Fact]
		public void CoffeeUpdater_Off_SetsIndictorOff()
		{
			_stateProvider.CurrentState.Returns(States.Off);

			_sut.DoUpdate();

			_controls.Received().SetIndicatorState(IndicatorState.OFF);
		}

		[Fact]
		public void CoffeeUpdater_Off_SetsBoilerOff()
		{
			_stateProvider.CurrentState.Returns(States.Off);

			_sut.DoUpdate();

			_controls.Received().SetBoilerState(BoilerState.OFF);
		}

		[Fact]
		public void CoffeeUpdater_Off_SetValveClosed()
		{
			_stateProvider.CurrentState.Returns(States.Off);

			_sut.DoUpdate();

			_controls.Received().SetReliefValveState(ReliefValveState.CLOSED);
		}

		[Fact]
		public void CoffeeUpdater_Off_SetWarmerOff()
		{
			_stateProvider.CurrentState.Returns(States.Off);

			_sut.DoUpdate();

			_controls.Received().SetWarmerState(WarmerState.OFF);
		}

		[Fact]
		public void CoffeeUpdater_Ready_SetsIndictorOff()
		{
			_stateProvider.CurrentState.Returns(States.Ready);

			_sut.DoUpdate();

			_controls.Received().SetIndicatorState(IndicatorState.ON);
		}

		[Fact]
		public void CoffeeUpdater_Ready_SetsBoilerOff()
		{
			_stateProvider.CurrentState.Returns(States.Ready);

			_sut.DoUpdate();

			_controls.Received().SetBoilerState(BoilerState.OFF);
		}

		[Fact]
		public void CoffeeUpdater_Ready_SetValveClosed()
		{
			_stateProvider.CurrentState.Returns(States.Ready);

			_sut.DoUpdate();

			_controls.Received().SetReliefValveState(ReliefValveState.CLOSED);
		}

		[Fact]
		public void CoffeeUpdater_Ready_SetWarmerOff()
		{
			_stateProvider.CurrentState.Returns(States.Ready);

			_sut.DoUpdate();

			_controls.Received().SetWarmerState(WarmerState.ON);
		}

		[Fact]
		public void CoffeeUpdater_Paused_SetsIndictorOff()
		{
			_stateProvider.CurrentState.Returns(States.Pause);

			_sut.DoUpdate();

			_controls.Received().SetIndicatorState(IndicatorState.OFF);
		}

		[Fact]
		public void CoffeeUpdater_Paused_SetsBoilerOn()
		{
			_stateProvider.CurrentState.Returns(States.Pause);

			_sut.DoUpdate();

			_controls.Received().SetBoilerState(BoilerState.ON);
		}

		[Fact]
		public void CoffeeUpdater_Paused_SetValveOpen()
		{
			_stateProvider.CurrentState.Returns(States.Pause);

			_sut.DoUpdate();

			_controls.Received().SetReliefValveState(ReliefValveState.OPEN);
		}

		[Fact]
		public void CoffeeUpdater_Paused_SetWarmerON()
		{
			_stateProvider.CurrentState.Returns(States.Pause);

			_sut.DoUpdate();

			_controls.Received().SetWarmerState(WarmerState.ON);
		}
	}
}