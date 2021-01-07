using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace CakeShopApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public BindingList<CakeCategory> CakeCategoryList = new BindingList<CakeCategory> {
			new CakeCategory { Name = "Tất cả" },
			new CakeCategory { Name = "Bánh Bơ-gơ" },
			new CakeCategory { Name = "Bánh kem" },
			new CakeCategory { Name = "Bánh mì" },
			new CakeCategory { Name = "Bánh mì vòng" },
			new CakeCategory { Name = "Bánh nướng nhỏ" },
			new CakeCategory { Name = "Bánh ổ dài" },
			new CakeCategory { Name = "Các loại khác" }
		};

		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = this;
			CakeCategoryCombobox.ItemsSource = CakeCategoryList;
			CakeCategoryCombobox.SelectedItem = CakeCategoryList[0];
		}

		private void Cake_Click(object sender, RoutedEventArgs e)
		{

		}

		private void SearchCakeButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void searchTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{

		}

		private void searchTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{

		}

		private void searchTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
		{

		}

		private void searchTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
		{

		}

		private void MinimizeButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void MenuButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void MaximizeButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void DeleteTextInSearchButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void DeleteTextInSearchButton_MouseEnter(object sender, MouseEventArgs e)
		{

		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ChangeClickedTypeButton_Click(object sender, RoutedEventArgs e)
		{

		}

		private void ChangeClickedControlButton_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
