using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Nameless.NumberConverter.ViewModels;

namespace Nameless.NumberConverter.Views
{
	/// <summary>
	/// Логика взаимодействия для NumberConverterView.xaml
	/// </summary>
	public partial class NumberConverterView : UserControl
	{
		public NumberConverterView()
		{
			InitializeComponent();
            DataContext = new NumberConverterViewModel();
		}
	}
}
