using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMakerHarness
{
	class Program
	{
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("(F)ill Reservoir, E(M)pty Reservoir, Press (B)utton, (R)emove Pot, (P)lace Pot, (E)mpty Pot, E(X)it:");

				string line = Console.ReadLine();

				string first = line.ToUpper().Substring(0, 1);

				if (first == "X")
				{
					Console.WriteLine("Goodbye");
					Console.ReadLine();
					return;
				}
			}
		}
	}
}
