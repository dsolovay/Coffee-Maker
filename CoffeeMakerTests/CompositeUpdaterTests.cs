using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMaker;
using CoffeeMaker.Classes;
using CoffeeMaker.Interfaces;
using NSubstitute;
using Xunit;

namespace CoffeeMakerTests
{
	public class CompositeUpdaterTests
	{
		IUpdater _sut;
		IUpdater _composedUpdater1;
		IUpdater _composedUpdater2;

		public CompositeUpdaterTests()
		{
			_composedUpdater1 = Substitute.For<IUpdater>();
			_composedUpdater2 = Substitute.For<IUpdater>();
			_sut = new CompositeUpdater(new IUpdater[]{ _composedUpdater1, _composedUpdater2});
		}

		[Fact]
		public void CompositeUpdater_Called_Calls()
		{
			_sut.DoUpdate();

			_composedUpdater1.Received(1).DoUpdate();
			_composedUpdater2.Received(1).DoUpdate();
		}

		[Fact]
		public void CompositeUpdater_Called_CallsInRightOrder()
		{
			 
			_sut.DoUpdate();

			Received.InOrder(() =>
			{
				_composedUpdater1.DoUpdate();
				_composedUpdater2.DoUpdate();
			});
		}

	}
}
