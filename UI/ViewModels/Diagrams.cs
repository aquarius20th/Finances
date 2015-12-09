using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using Common;
using UI.Interfaces;
using UI.Services;
using static Common.Record;

namespace UI.ViewModels
{
	public class Diagrams : Screen, IViewModel
	{
		public class Data
		{
			public Categories Category { get; set; }
			public decimal Amount { get; set; }
			public string Description { get; set; }
		}

		private readonly IAnalyzer analyzer;

		private readonly IExpenses expenses;

		private ILookup<Types, Record> types;

		public Diagrams(IExpenses expenses, IAnalyzer analyzer)
		{
			this.expenses = expenses;
			this.analyzer = analyzer;
		}

		protected override void OnInitialize()
		{
			base.OnInitialize();

			Update();
		}

		public Dictionary<Types, Dictionary<int, decimal>> BalanceByMonth { get; private set; }
		public Dictionary<Categories, decimal> ExpenseByCategory { get; private set; }
		public Dictionary<Categories, decimal> IncomeByCategory { get; private set; }
		public Dictionary<string, Data[]> ExpenseByDay { get; private set; }

		public void Update()
		{
			types = analyzer.GroupByType(expenses.Records);
			var expense = types[Types.Expense].Concat(types[Types.Shared]).ToArray();

			BalanceByMonth = CalculateBalanceByMonth();
			ExpenseByCategory = GroupByCategory(expense);
			IncomeByCategory = GroupByCategory(types[Types.Income]);
			ExpenseByDay = GroupByDay(expense);

			NotifyOfPropertyChange(nameof(BalanceByMonth));
			NotifyOfPropertyChange(nameof(ExpenseByCategory));
			NotifyOfPropertyChange(nameof(IncomeByCategory));
			NotifyOfPropertyChange(nameof(ExpenseByDay));
		}

		private Dictionary<string, Data[]> GroupByDay(IEnumerable<Record> records)
		{
			return analyzer.GroupByDay(records).ToDictionary(
				day => day.Key,
				day => day.GroupBy(record => record.Category)
				          .Select(SquashRecords)
				          .ToArray());
		}

		private Dictionary<Categories, decimal> GroupByCategory(IEnumerable<Record> records)
		{
			return analyzer.GroupByCategory(records).ToDictionary(
				group => group.Key,
				group => group.Sum(record => record.Amount));
		}

		private Dictionary<Types, Dictionary<int, decimal>> CalculateBalanceByMonth()
		{
			return new Dictionary<Types, Dictionary<int, decimal>>
			{
				[Types.Expense] = analyzer.CalculateTotalByMonth(types[Types.Expense].Concat(types[Types.Shared])),
				[Types.Income] = analyzer.CalculateTotalByMonth(types[Types.Income])
			};
		}

		private Data SquashRecords(IGrouping<Categories, Record> group)
		{
			return new Data
			{
				Category	= group.Key,
				Amount		= group.Sum(r => r.Amount),
				Description = group.Select(r => r.Description)
				                   .Aggregate((a, b) => $"{a}\n{b}")
			};
		}
	}
}