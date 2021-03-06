using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using Caliburn.Micro;
using UI.Services;
using UI.Services.Amount;
using static Common.Record;

namespace UI.Interfaces
{
	public interface IForm : INotifyPropertyChangedEx
	{
		decimal Amount { get; }
		Types SelectedType { get; set; }
		Brush Background { get; set; }
		void Submit();
		bool CanSubmit();
		void Subtract(decimal value);
	}
}