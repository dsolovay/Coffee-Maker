using System.Collections.Generic;
using CoffeeMachine;
using CoffeeMaker.Enumerations;
using CoffeeMaker.Interfaces;

namespace CoffeeMaker.Classes
{
	public class StateMachine:IEventReceiver,IStateProvider
	{
		public StateMachine()
		{
			CurrentState = States.Empty;
		}
		public States CurrentState { get; set; }

		private Dictionary<StateTransition, States> transitions = new Dictionary<StateTransition, States>
		{
			{new StateTransition(States.Empty, Events.BoilerNotEmpty), States.Off},
			{new StateTransition(States.Off, Events.ButtonPushed), States.Brew},
			{new StateTransition(States.Brew, Events.PotRemoved), States.Pause},
			{new StateTransition(States.Pause, Events.PotPresent), States.Brew},
			{new StateTransition(States.Brew, Events.BoilerEmpty), States.Ready},
			{new StateTransition(States.Ready, Events.PotEmpty), States.Empty},
		};

		public void HandleEvent(Events theEvent)
		{
			var t = new StateTransition(this.CurrentState, theEvent);
			if (transitions.ContainsKey(t))
			{
				CurrentState = transitions[t];
			}
		}

		public class StateTransition { 
			private readonly States _state;
			private readonly Events _event;

			public StateTransition(States state, Events @event)
			{
				_state = state;
				_event = @event;
			}

			protected bool Equals(StateTransition other)
			{
				return _state == other._state && _event == other._event;
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
					return ((int) _state*397) ^ (int) _event;
				}
			}
		}
	}
}