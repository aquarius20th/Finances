﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Practices.Unity;

namespace Statistics.Views
{
	/// <summary>
	/// Interaction logic for Funds.xaml
	/// </summary>
	public partial class Funds : UserControl
	{
		public Funds()
		{
			InitializeComponent();
		}

		[InjectionConstructor]
		//------------------------------------------------------------------
		public Funds(ViewModels.Funds viewModel) : this()
	    {
			DataContext = viewModel;
		}
	}


}