﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using Caliburn.Micro;
using NSubstitute;
using NUnit.Framework;
using Common;
using Common.Storages;
using UI.Services;
using UI.ViewModels;
using static NSubstitute.Substitute;
using static Common.Record.Types;
using static Common.Record.Categories;

namespace UI.Tests.ViewModels
{
	public class FundsTests
	{
		private readonly Record[] records =
		{
			new Record(100, Expense,Food, "Novus",	new DateTime(2015, 10, 1)),
			new Record(100, Shared,	Food, "Water",	new DateTime(2015, 10, 1)),
			new Record(500, Income, Deposit, "",	new DateTime(2015, 10, 1)),
		};

		private static Funds Create()
		{
			var sources = Enumerable.Range(1, 3)
			                        .Select(i =>
			                        {
				                        var source = For<IFundsSource>();
//				                        source.Value = 100;
				                        return source;
			                        })
			                        .ToArray();

			var expenses = For<IExpenses>( );
			expenses.Records = new ObservableCollection<Record>(Enumerable.Empty<Record>());
			return new Funds(For<IFund[]>(), expenses, null);
		}

		[Test]
		public void CalculateEstimatedBalance_Always_ReturnsRestBasedOnRecords()
		{
			var funds = Create();

			var actual = funds.CalculateEstimatedBalance(records);

			Assert.That(actual, Is.EqualTo(300));
		}

		[Test]
		public void CalculateDivergence_Always_ReturnsDifferenceInEstimatedAndRealBalance()
		{
			var funds = Create();

			var actual = funds.CalculateDivergence(300, records);

			Assert.That(actual, Is.EqualTo(0));
		}
	}
}