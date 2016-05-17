using System;
using System.Collections.Generic;
using CoffeeMaker.Interfaces;

namespace CoffeeMaker.Classes
{
	public class CompositeUpdater : IUpdater
	{
		private readonly IEnumerable<IUpdater> _updaters;

		public CompositeUpdater(IEnumerable<IUpdater> updaters)
		{
			if (updaters == null) throw new ArgumentNullException(nameof(updaters));
			this._updaters = updaters;
		}

		public void DoUpdate()
		{
			foreach (IUpdater updater in _updaters)
			{
				updater.DoUpdate();
			}

		}
	}
}