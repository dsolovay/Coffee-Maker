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
	public class StateMachineTests
	{
		public StateMachineTests()
		{
			this.StateMachine = new StateMachine();
		}

		public StateMachine StateMachine { get; set; }

		[Fact]
		public void StateMachine_Created_StateIsEmpty()
		{
			Assert.Equal(States.Empty, this.StateMachine.CurrentState);
		}

		[Theory,
			InlineData(States.Empty, Events.ButtonPushed, States.Empty),
			InlineData(States.Empty, Events.BoilerNotEmpty, States.Off),
			InlineData(States.Off, Events.ButtonPushed, States.Brew),
			InlineData(States.Brew, Events.ButtonPushed, States.Brew),
			InlineData(States.Brew, Events.PotRemoved, States.Pause),
			InlineData(States.Pause, Events.PotPresent, States.Brew),
			InlineData(States.Brew, Events.BoilerEmpty, States.Ready),
			InlineData(States.Ready, Events.PotEmpty, States.Empty) 
			]
		public void StateMachine_ButtonPush_StateIsBrew(States startState, Events theEvent, States expectedState)
		{
			this.StateMachine.CurrentState = startState;

			this.StateMachine.HandleEvent(theEvent);

			Assert.Equal(expectedState, this.StateMachine.CurrentState);
		}

	 
	}
}
