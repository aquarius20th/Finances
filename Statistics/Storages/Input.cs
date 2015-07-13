﻿using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.Practices.Prism.Mvvm;

namespace Statistics.Storages
{
	public class Input :BindableBase, IStorage<decimal>
	{
		private decimal value;
		private readonly FileStorage file;

		public decimal Value
		{
			get { return value; }
			set
			{
				this.value = value;
				file.Save(Name, Value);
				OnPropertyChanged("Value");
			}
		}

		public string Name { get; set; }

		public Input(string name)
		{
			Name = name;
			file = new FileStorage();
			value = file.Load(Name);
		}


		public override string ToString()
		{
			return "ASS";
		}


	}
}