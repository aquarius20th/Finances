using Common;
using UI.Views.Converters;

namespace UI.Services.Amount
{
	public class SharedAmount : Amount
	{
		private readonly int customers;
		private decimal value;

		public SharedAmount(IAdder adder, ISettings settings) : base(adder)
		{
			customers = settings.Customers;
		}

		public override decimal Value
		{
			get { return value; }
			set { this.value = value/customers; }
		}

		public override decimal Total => Value*customers;
	}
}