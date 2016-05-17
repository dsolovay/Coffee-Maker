using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine;
using CoffeeMaker.Classes;
using CoffeeMaker.Enumerations;
using Xunit;

namespace CoffeeMakerTests
{
	public class CoffeeMakerStateMachineTests
	{
		public CoffeeMakerStateMachineTests()
		{
			this.StateMachine = new StateMachine();
		}

		public StateMachine StateMachine { get; set; }

		[Fact]
		public void StateMachine_Created_StateIsOff()
		{
			Assert.Equal(States.Off, this.StateMachine.CurrentState);
		}

		[Theory,
			InlineData(States.Off, Events.ButtonPushed, States.Brew),
			InlineData(States.Brew, Events.ButtonPushed, States.Brew),
			InlineData(States.Brew, Events.PotRemoved, States.Pause),
			InlineData(States.Brew, Events.BoilerEmpty, States.Ready),
			InlineData(States.Ready, Events.PotEmpty, States.Off) 
			]
		public void StateMachine_ButtonPush_StateIsBrew(States startState, Events action, States expectedState)
		{
			this.StateMachine.CurrentState = startState;

			this.StateMachine.SendEvent(action);

			Assert.Equal(expectedState, this.StateMachine.CurrentState);
		}
	}
}
