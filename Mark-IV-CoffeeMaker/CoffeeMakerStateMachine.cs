using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace CoffeeMachine
{
	public class CoffeeMakerStateMachine:IActionReceiver
	{
		public CoffeeMakerStateMachine()
		{
			CurrentState = States.Off;
		}
		public States CurrentState { get; set; }

		public enum Actions
		{
			ButtonPushed,
			CaraffeRemoved,
			CaraffePresent,
			BoilerEmpty,
			CaraffeEmpty
		}

		public enum States
		{
			Off,
			Brew,
			Pause,
			Ready
		}

		private Dictionary<StateTransition, States> transitions = new Dictionary<StateTransition, States>
		{
			{new StateTransition(States.Off, Actions.ButtonPushed), States.Brew},
			{new StateTransition(States.Brew, Actions.CaraffeRemoved), States.Pause},
			{new StateTransition(States.Pause, Actions.CaraffePresent), States.Brew},
			{new StateTransition(States.Brew, Actions.BoilerEmpty), States.Ready},
			{new StateTransition(States.Ready, Actions.CaraffeEmpty), States.Off},
		};


		 

		public void DoAction(Actions action)
		{
			var t = new StateTransition(this.CurrentState, action);
			if (transitions.ContainsKey(t))
			{
				CurrentState = transitions[t];
			}
		}

		public class StateTransition { 
			private readonly States _s;
			private readonly Actions _a;

			public StateTransition(States s, Actions a)
			{
				_s = s;
				_a = a;
			}

			protected bool Equals(StateTransition other)
			{
				return _s == other._s && _a == other._a;
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj)) return false;
				if (ReferenceEquals(this, obj)) return true;
				if (obj.GetType() != this.GetType()) return false;
				return Equals((StateTransition) obj);
			}


			public override int GetHashCode()
			{
				unchecked
				{
					return ((int) _s*397) ^ (int) _a;
				}
			}
		}
	}

	public interface IActionReceiver
	{
		void DoAction(CoffeeMakerStateMachine.Actions action);
	}
}