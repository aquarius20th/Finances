﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UI.Interfaces;
using UI.ViewModels;

// TODO: Subtract decimal

namespace UI.Services
{
    public class Subtractor : ISubtractor
    {
        public IForm Primary { get; private set; }

        public void Add(IForm form)
		{
			form.PropertyChanged += OnPropertyChanged;

			Primary = Primary ?? form;
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs arguments)
		{
		    if (arguments.PropertyName != nameof(Form.Amount)) return;
		    if (sender == Primary) return;

		    var form = (IForm) sender;
            Primary.Subtract(form);
			form.PropertyChanged -= OnPropertyChanged;
        }
    }

    public interface ISubtractor
    {
        void Add(IForm form);
    }
}