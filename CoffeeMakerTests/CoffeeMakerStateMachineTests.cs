using System;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMaker;
using Xunit;

namespace CoffeeMakerTests
{
	public class CoffeeMakerStateMachineTests
	{
		public CoffeeMakerStateMachineTests()
		{
			this.StateMachine = new CoffeeMakerStateMachine();
		}

		public CoffeeMakerStateMachine StateMachine { get; set; }

		[Fact]
		public void StateMachine_Created_StateIsOff()
		{
			Assert.Equal(CoffeeMakerStateMachine.States.Off, this.StateMachine.CurrentState);
		}

		[Theory,
			InlineData(CoffeeMakerStateMachine.States.Off, CoffeeMakerStateMachine.Actions.ButtonPushed, CoffeeMakerStateMachine.States.Brew),
			InlineData(CoffeeMakerStateMachine.States.Brew, CoffeeMakerStateMachine.Actions.ButtonPushed, CoffeeMakerStateMachine.States.Brew),
			InlineData(CoffeeMakerStateMachine.States.Brew, CoffeeMakerStateMachine.Actions.CaraffeRemoved, CoffeeMakerStateMachine.States.Pause),
			InlineData(CoffeeMakerStateMachine.States.Brew, CoffeeMakerStateMachine.Actions.BoilerEmpty, CoffeeMakerStateMachine.States.Ready),
			InlineData(CoffeeMakerStateMachine.States.Ready, CoffeeMakerStateMachine.Actions.CaraffeEmpty, CoffeeMakerStateMachine.States.Off) 
			]
		public void StateMachine_ButtonPush_StateIsBrew(CoffeeMakerStateMachine.States startState, CoffeeMakerStateMachine.Actions action, CoffeeMakerStateMachine.States expectedState)
		{
			this.StateMachine.CurrentState = startState;

			this.StateMachine.DoAction(action);

			Assert.Equal(expectedState, this.StateMachine.CurrentState);
		}
	}
}
