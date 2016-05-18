using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine;
using CoffeeMaker.Interfaces;

namespace CoffeeMakerHarness
{
	class FakeCoffeeAPI:IApiAdapter
	{
		internal BoilerStatus BoilerStatus { private get;  set; }
		internal WarmerPlateStatus WarmerPlateStatus { private get; set; }
		internal BrewButtonStatus BrewButtonStatus { private get; set; }
		private BoilerState BoilerState { get; set; }
		private WarmerState WarmerState { get; set; }
		private IndicatorState IndicatorState { get; set; }
		private ReliefValveState ReliefValveState { get; set; }

		public FakeCoffeeAPI()
		{
			BoilerStatus = BoilerStatus.EMPTY;
			WarmerPlateStatus = WarmerPlateStatus.POT_EMPTY;
			BrewButtonStatus = BrewButtonStatus.NOT_PUSHED;
			BoilerState = BoilerState.OFF;
			WarmerState = WarmerState.OFF;
			IndicatorState = IndicatorState.OFF;
			ReliefValveState = ReliefValveState.CLOSED;
		}
		public WarmerPlateStatus GetWarmerPlateStatus()
		{
			return this.WarmerPlateStatus;
		}

		public BoilerStatus GetBoilerStatus()
		{
			return this.BoilerStatus;
		}

		public BrewButtonStatus GetBrewButtonStatus()
		{
			if (BrewButtonStatus == BrewButtonStatus.PUSHED)
			{
				BrewButtonStatus = BrewButtonStatus.NOT_PUSHED;
				return BrewButtonStatus.PUSHED;
			}
			else
			{
				return BrewButtonStatus;
			}
		}

		public void SetBoilerState(BoilerState s)
		{
			this.BoilerState = s;
		}

		public void SetWarmerState(WarmerState s)
		{
			this.WarmerState = s;
		}

		public void SetIndicatorState(IndicatorState s)
		{
			this.IndicatorState = s;
		}

		public void SetReliefValveState(ReliefValveState s)
		{
			this.ReliefValveState = s;
		}

		public string ShowStateValues()
		{
			return string.Format("Boiler State: {0}\nWarmer State: {1}\nIndicator State: {2}\nRelief Valve: {3}\n\n",
				this.BoilerState,
				this.WarmerState,
				this.IndicatorState,
				this.ReliefValveState);
		}
	}
}
