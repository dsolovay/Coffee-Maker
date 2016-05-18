using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine;
using CoffeeMaker.Classes;
using CoffeeMaker.Interfaces;

namespace CoffeeMakerHarness
{
	class Program
	{
		
		 
		static void Main(string[] args)
		{
			FakeCoffeeAPI fakeApi = new FakeCoffeeAPI();
			IUpdater coffeeMachine = CreateCoffeeMachine(fakeApi);

			while (true)
			{
				Console.Write("F = Fill Boiler,\nM = Empty Boiler,\nB = Press Button,\nR = Remove Pot,\nP = Place Pot,\nE = Empty Pot,\nX = Exit\nChoose option: ");

				string line = Console.ReadLine();

				string first = line.ToUpper().Substring(0, 1);

				SetFakeApiState(fakeApi, first);
				coffeeMachine.DoUpdate();

				Console.WriteLine();
				Console.WriteLine(fakeApi.ShowStateValues());

				if (first == "X")
				{
					Console.WriteLine("Goodbye");
					Console.ReadLine();
					return;
				}
			}
		}

		private static IUpdater CreateCoffeeMachine(IApiAdapter api)
		{
			StateMachine stateMachine = new StateMachine();
			return new CompositeUpdater(new IUpdater[] {new StateUpdater(api, stateMachine), new ControlUpdater(stateMachine, api)});
		}

		private static void SetFakeApiState(FakeCoffeeAPI fakeApi, string first)
		{
			switch (first)
			{
				case "F":
					fakeApi.BoilerStatus = BoilerStatus.NOT_EMPTY;
					break;
				case "M":
					fakeApi.BoilerStatus = BoilerStatus.EMPTY;
					break;
				case "B":
					fakeApi.BrewButtonStatus = BrewButtonStatus.PUSHED;
					break;
				case "R":
					fakeApi.WarmerPlateStatus = WarmerPlateStatus.WARMER_EMPTY;
					break;
				case "P":
					fakeApi.WarmerPlateStatus = WarmerPlateStatus.POT_NOT_EMPTY;
					break;
				case "E":
					fakeApi.WarmerPlateStatus = WarmerPlateStatus.POT_EMPTY;
					break;
			}
		}
	}
}
