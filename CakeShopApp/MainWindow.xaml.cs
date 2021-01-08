using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace CakeShopApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		//---------------------------------------- Khai báo các biến toàn cục --------------------------------------------//

		public event PropertyChangedEventHandler PropertyChanged;
		private Button clickedControlButton;
		private List<Cake> CakeInfoList = new List<Cake>();     //Danh sách thông tin tất cả các chuyến đi
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
		private CollectionView view;
		private BindingList<ColorSetting> ListColor = new BindingList<ColorSetting>       //Tạo dữ liệu màu cho ListColor
		{
			new ColorSetting { Color = "#FFCA5010" }, new ColorSetting { Color = "#FFFF8C00" }, new ColorSetting { Color = "#FFE81123" },
			new ColorSetting { Color = "#FFD13438" }, new ColorSetting { Color = "#FFFF4081" }, new ColorSetting { Color = "#FFC30052" }, 
			new ColorSetting { Color = "#FFBF0077" }, new ColorSetting { Color = "#FF9A0089" }, new ColorSetting { Color = "#FF881798" }, 
			new ColorSetting { Color = "#FF744DA9" }, new ColorSetting { Color = "#FF4CAF50" }, new ColorSetting { Color = "#FF10893E" }, 
			new ColorSetting { Color = "#FF018574" }, new ColorSetting { Color = "#FF03A9F4" }, new ColorSetting { Color = "#FF304FFE" },
			new ColorSetting { Color = "#FF0063B1" }, new ColorSetting { Color = "#FF6B69D6" }, new ColorSetting { Color = "#FF8E8CD8" },
			new ColorSetting { Color = "#FF8764B8" }, new ColorSetting { Color = "#FF038387" }, new ColorSetting { Color = "#FF525E54" }, 
			new ColorSetting { Color = "#FF7E735F" }, new ColorSetting { Color = "#FF9E9E9E" }, new ColorSetting { Color = "#FF515C6B" }, 
			new ColorSetting { Color = "#FF000000" }
		};

		private Condition FilterCondition = new Condition { Type = "" };
		public Cake cake = new Cake();
		private bool isMinimizeMenu, isEditMode, IsDetailCake;
		int selectedCakeIndex = 0;

		private string _colorScheme = "";           //Màu nền hiện tại
		public string ColorScheme
		{
			get
			{
				return _colorScheme;
			}
			set
			{
				_colorScheme = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ColorScheme"));
				}
			}
		}


		//---------------------------------------- Khai báo các class --------------------------------------------//

		//Class lưu trữ màu trong Color setting
		public class ColorSetting
		{
			public string Color { get; set; }
		}

		//Class điều kiện để filter
		class Condition
		{
			public string Type;
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			
			/*CakeInfoList = new List<Cake>
			{
				new Cake
				{
					CakeName = "Bánh tráng",
					Category = "Bánh Bơ-gơ",
					Description = "Bánh tráng siêu to khổng lồ.",
					ID = "0",
					ImagesList = new BindingList<CakeImage>
					{
						new CakeImage
						{
							ImagePath = "Images/0.jpg"
						},
						new CakeImage
						{
							ImagePath = "Images/1.jpg"
						},
						new CakeImage
						{
							ImagePath = "Images/2.jpg"
						}
					},
					Price = 100000,
					PrimaryImagePath = "Images/0.jpg"
				}
			};*/

			CakeCategoryCombobox.ItemsSource = CakeCategoryList;
			CakeCategoryCombobox.SelectedItem = CakeCategoryList[0];

			this.DataContext = this;

			//Binding dữ liệu màu cho Setting Color Table
			SettingColorItemsControl.ItemsSource = ListColor;

			//Cài đặt màu chủ đạo cho ứng dụng
			//ColorScheme = ConfigurationManager.AppSettings["ColorScheme"];
			ColorScheme = ListColor[8].Color;

			//Mặc định khi mở ứng dụng thị hiển thị menu ở dạng mở rộng
			isMinimizeMenu = false;
			//Mặc định không ở màn hình chi tiết
			IsDetailCake = false;

			//Mặc định không ở chế độ chỉnh sửa chuyến đi
			isEditMode = false;

			CakeButtonItemsControl.ItemsSource = CakeInfoList;
			CakeListAppearAnimation();

			//Default buttons
			clickedControlButton = HomeButton;
		}

		public MainWindow()
		{
			InitializeComponent();

			// Đọc dữ liệu các món ăn từ data
			XmlSerializer xsFood = new XmlSerializer(typeof(List<Cake>));
			try
			{
				using (var reader = new StreamReader(@"Data\Cake.xml"))
				{
					CakeInfoList = (List<Cake>)xsFood.Deserialize(reader);
				}
			}
			catch
			{
				CakeInfoList = new List<Cake>();
			}

			view = (CollectionView)CollectionViewSource.GetDefaultView(CakeInfoList);
		}

		//---------------------------------------- Các hàm xử lý cập nhật giao diện --------------------------------------------//

		//Cập nhật lại thay đổi từ dữ liệu lên màn hình
		private void UpdateUIFromData()
		{
			view.Filter = Filter;
			CakeButtonItemsControl.ItemsSource = CakeInfoList;
			CakeListAppearAnimation();
		}

		//---------------------------------------- Các hàm Get --------------------------------------------//

		//Get current app domain
		public static string GetAppDomain()
		{
			string absolutePath;
			absolutePath = AppDomain.CurrentDomain.BaseDirectory;
			return absolutePath;
		}

		//Lấy chỉ số phần tử của chuyến đi trong mảng
		private int GetElementIndexInArray(Button button)
		{
			var curCake = new Cake();
			//Nếu nhấn hình ảnh món ăn ở màn hình Home
			if (button.Content.GetType().Name == "WrapPanel")
			{
				var wrapPanel = (WrapPanel)button.Content;
				curCake = (Cake)wrapPanel.DataContext;
			}
			else //Nếu nhấn món ăn ở trong nút Search
			{
				curCake = (Cake)button.DataContext;
			}

			var result = 0;
			for (int i = 0; i < CakeInfoList.Count; i++)
			{
				if (curCake == CakeInfoList[i])
				{
					result = i;
					break;
				}
				else
				{
					//Do nothing
				}
			}
			return result;
		}

		/*private int GetMinID()
		{
			int result = 1;
			for (int i = 0; i < CakeInfoList.Count; i++)
			{
				if (result < CakeInfoList[i].CakeID)
				{
					break;
				}
				else
				{
					result++;
				}
			}
			return result;
		}*/



		//---------------------------------------- Các hàm lưu trữ dữ liệu --------------------------------------------//

		//Lưu lại danh sách món ăn
		private void SaveListFood()
		{
			XmlSerializer xs = new XmlSerializer(typeof(List<Cake>));
			TextWriter writer = new StreamWriter(@"Data\Cake.xml");
			xs.Serialize(writer, CakeInfoList);
			writer.Close();
		}


		//---------------------------------------- Xử lý cửa sổ --------------------------------------------//

		//Cài đặt nút đóng cửa sổ
		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			SaveListFood();
			//var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			//config.AppSettings.Settings["ColorScheme"].Value = ColorScheme;
			//config.Save(ConfigurationSaveMode.Minimal);
			Application.Current.Shutdown();

		}
		//Cài đặt nút phóng to/ thu nhỏ cửa sổ
		private void MaximizeButton_Click(object sender, RoutedEventArgs e)
		{
			AdjustWindowSize();
		}

		//Cài đặt nút ẩn cửa sổ
		private void MinimizeButton_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		//Thay đổi kích thước cửa sổ
		//Nếu đang ở trạng thái phóng to thì thu nhỏ và ngược lại
		private void AdjustWindowSize()
		{
			var imgName = "";

			if (WindowState == WindowState.Maximized)
			{
				WindowState = WindowState.Normal;
				imgName = "Images/maximize.png";
			}
			else
			{
				WindowState = WindowState.Maximized;
				imgName = "Images/restoreDown.png";
			}

			//Lấy nguồn ảnh
			var img = new BitmapImage(new Uri(
						imgName,
						UriKind.Relative)
				);

			//Thiết lập ảnh chất lượng cao
			RenderOptions.SetBitmapScalingMode(img, BitmapScalingMode.HighQuality);

			//Thay đổi icon
			(MaxButton.Content as Image).Source = img;
		}


		//---------------------------------------- Các hàm sắp xếp --------------------------------------------//

		private bool Filter(object item)
		{
			bool result = true;
			var cakeInfo = (Cake)item;
			if (FilterCondition.Type != "Tất cả" && FilterCondition.Type != CakeCategoryList[cakeInfo.Category].Name)
			{
				result = false;
			}
			return result;
		}



		//---------------------------------------- Xử lý các nút bấm --------------------------------------------//



		private void Cake_Click(object sender, RoutedEventArgs e)
		{
			//Đóng giao diện màn hình danh sách các món bánh ngọt
			CakeListGrid.Visibility = Visibility.Collapsed;
			//Đóng giao diện thanh chọn loại món bánh ngọt
			TypeBarDockPanel.Visibility = Visibility.Collapsed;

			//Lấy chỉ số của hình ảnh món ăn được nhấn
			selectedCakeIndex = GetElementIndexInArray((Button)sender);
			DetailCakeGrid.DataContext = CakeInfoList[selectedCakeIndex];
			cake = new Cake(CakeInfoList[selectedCakeIndex]);

			//Mở giao diện màn hình chi tiết món bánh ngọt
			DetailCakeGrid.Visibility = Visibility.Visible;

			//Bật chế độ đang ở màn hình chi tiết
			IsDetailCake = true;
		}

		private void EditCakeButton_Click(object sender, RoutedEventArgs e)
		{
			isEditMode = true;
			cake = new Cake(CakeInfoList[selectedCakeIndex]);
			//Bật màn hình chỉnh sửa
			ChangeClickedControlButton_Click(AddCakeButton, null);
		}

		private void DeleteCakeButton_Click(object sender, RoutedEventArgs e)
		{
			/*TripInfoList.Remove(TripInfoList[selectedTripIndex]);

			//
			DetailTripGrid.Visibility = Visibility.Collapsed;
			//Tắt màu của nút Add
			var wrapPanel = (WrapPanel)AddTripButton.Content;
			var collection = wrapPanel.Children;
			var block = (TextBlock)collection[0];
			var text = (TextBlock)collection[2];
			block.Background = Brushes.Transparent;
			text.Foreground = Brushes.Black;

			//Quay về màn hình Home
			clickedControlButton = HomeButton;
			TripListGrid.Visibility = Visibility.Visible;
			TypeBarDockPanel.Visibility = Visibility.Visible;
			ControlStackPanel.Visibility = Visibility.Visible;
			//Hiển thị màu cho nút Home
			wrapPanel = (WrapPanel)HomeButton.Content;
			collection = wrapPanel.Children;
			block = (TextBlock)collection[0];
			text = (TextBlock)collection[2];
			block.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
			text.Foreground = block.Background;

			//Tắt chế độ chỉnh sửa
			isEditMode = false;

			//Cập nhật lại giao diện
			UpdateUIFromData();*/
		}

		private void SearchCakeButton_Click(object sender, RoutedEventArgs e)
		{
			Cake_Click(sender, null);
		}

		private void MenuButton_Click(object sender, RoutedEventArgs e)
		{
			if (isMinimizeMenu == false)
			{
				col0.Width = new GridLength(45);
				isMinimizeMenu = true;
			}
			else
			{
				col0.Width = new GridLength(250);
				isMinimizeMenu = false;
			}
		}

		private void CakePriceTextBlock_Loaded(object sender, RoutedEventArgs e)
		{
			
			var cake = ((TextBlock)sender).DataContext as Cake;
			((TextBlock)sender).Text = FormatPriceString(cake.Price) + " ₫";

		}

		private void ChangeClickedControlButton_Click(object sender, RoutedEventArgs e)
		{
			//Tắt màu của nút hiện tại
			var wrapPanel = (WrapPanel)clickedControlButton.Content;
			var collection = wrapPanel.Children;
			var block = (TextBlock)collection[0];
			var text = (TextBlock)collection[2];
			block.Background = Brushes.Transparent;
			text.Foreground = Brushes.Black;

			//Đóng giao diện thanh chọn loại và tìm kiếm
			TypeBarDockPanel.Visibility = Visibility.Collapsed;
			//Đóng giao diện menu
			ControlStackPanel.Visibility = Visibility.Collapsed;
			//Đóng giao diện màn hình chi tiết chuyến đi
			DetailCakeGrid.Visibility = Visibility.Collapsed;
			//Đóng giao diện màn hình trang chủ
			CakeListGrid.Visibility = Visibility.Collapsed;
			//Đóng giao diện màn hình thêm chuyến đi mới
			//AddCakeGrid.Visibility = Visibility.Collapsed;
			//Đóng giao diện màn hình cài đặt
			SettingStackPanel.Visibility = Visibility.Collapsed;
			//Đóng giao diện thông tin developer
			AboutStackPanel.Visibility = Visibility.Collapsed;

			if (IsDetailCake == true)
			{
				IsDetailCake = false;
			}
			else
			{
				//Do nothing
			}

			//Hiển thị màu cho nút vừa được nhấn
			var button = (Button)sender;
			wrapPanel = (WrapPanel)button.Content;
			collection = wrapPanel.Children;
			block = (TextBlock)collection[0];
			text = (TextBlock)collection[2];
			block.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
			text.Foreground = block.Background;

			//Cập nhật nút mới
			clickedControlButton = button;

			//Mở giao diện mới sau khi nhấn nút
			if (button == HomeButton)
			{
				CakeListGrid.Visibility = Visibility.Visible;
				TypeBarDockPanel.Visibility = Visibility.Visible;
				ControlStackPanel.Visibility = Visibility.Visible;
			}
			/*else if (button == AddCakeButton)
			{
				AddCakeGrid.Visibility = Visibility.Visible;
				if (isEditMode == false)
				{
					int newID = GetMinID();
					Cake = new Cake() { CakeID = newID, Stage = "Bắt đầu" };
				}
				AddCakeGrid.DataContext = Cake;
			}*/
			else if (button == SettingButton)
			{
				SettingStackPanel.Visibility = Visibility.Visible;
				ControlStackPanel.Visibility = Visibility.Visible;
				/*var value = ConfigurationManager.AppSettings["ShowSplashScreen"];
				bool showSplashStatus = bool.Parse(value);
				if (showSplashStatus == true)
				{
					ShowSplashScreenCheckBox.IsChecked = true;
				}*/
			}
			else if (button == AboutButton)
			{
				AboutStackPanel.Visibility = Visibility.Visible;
				ControlStackPanel.Visibility = Visibility.Visible;
			}

			//Cập nhật lại giao diện
			UpdateUIFromData();
		}

		private void CakeCategoryCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

			FilterCondition.Type = CakeCategoryList[((ComboBox)sender).SelectedIndex].Name;
			//Cập nhật lại giao diện
			UpdateUIFromData();

		}

		//---------------------------------------- Các hàm xử lý sự kiện --------------------------------------------//

		private string ConvertToUnSign(string input)
		{
			input = input.Trim();
			for (int i = 0x20; i < 0x30; i++)
			{
				input = input.Replace(((char)i).ToString(), " ");
			}
			Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
			string str = input.Normalize(NormalizationForm.FormD);
			string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
			while (str2.IndexOf("?") >= 0)
			{
				str2 = str2.Remove(str2.IndexOf("?"), 1);
			}
			return str2;
		}

		private void DeleteTextInSearchButton_Click(object sender, RoutedEventArgs e)
		{
			searchTextBox.Text = "";
			searchTextBox.Focus();
		}

		private void DeleteTextInSearchButton_MouseEnter(object sender, MouseEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				DeleteTextInSearchButton_Click(null, null);
			}
			else
			{
				//Do nothing
			}
		}

		private void searchTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Down)
			{
				searchComboBox.Focus();
				searchComboBox.SelectedIndex = 0;
				searchComboBox.IsDropDownOpen = true;
			}
			if (e.Key == Key.Escape)
			{
				searchComboBox.IsDropDownOpen = false;

			}
		}

		private void searchTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			if (e.Text != "\u001b")  //khác escapes
			{
				searchComboBox.IsDropDownOpen = true;
			}
			if (!string.IsNullOrEmpty(searchTextBox.Text))
			{
				string fullText = ConvertToUnSign(searchTextBox.Text.Insert(searchTextBox.CaretIndex, (e.Text)));
				searchComboBox.ItemsSource = CakeInfoList.Where(s => ConvertToUnSign(s.CakeName).IndexOf(fullText, StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
				if (searchComboBox.Items.Count == 0)
				{
					SearchNotificationComboBox.IsDropDownOpen = true;
					searchComboBox.IsDropDownOpen = false;
				}
			}
			else if (!string.IsNullOrEmpty(e.Text))
			{
				searchComboBox.ItemsSource = CakeInfoList.Where(s => ConvertToUnSign(s.CakeName).IndexOf(ConvertToUnSign(e.Text), 
					StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
			}
			else
			{
				searchComboBox.ItemsSource = CakeInfoList;
			}
		}

		private void searchTextBox_PreviewKeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Back || e.Key == Key.Delete)
			{

				searchComboBox.IsDropDownOpen = true;

				if (!string.IsNullOrEmpty(searchTextBox.Text))
				{
					searchComboBox.ItemsSource = CakeInfoList.Where(s => ConvertToUnSign(s.CakeName).IndexOf(ConvertToUnSign(searchTextBox.Text), 
						StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
					if (searchComboBox.Items.Count == 0)
					{
						SearchNotificationComboBox.IsDropDownOpen = true;
						searchComboBox.IsDropDownOpen = false;
					}

				}
				else
				{
					searchComboBox.ItemsSource = CakeInfoList;
				}

			}
		}

		private void searchTextBox_Pasting(object sender, DataObjectPastingEventArgs e)
		{

			searchComboBox.IsDropDownOpen = true;

			string pastedText = (string)e.DataObject.GetData(typeof(string));
			string fullText = searchTextBox.Text.Insert(searchTextBox.CaretIndex, (pastedText));

			if (!string.IsNullOrEmpty(fullText))
			{
				searchComboBox.ItemsSource = CakeInfoList.Where(s => ConvertToUnSign(s.CakeName).IndexOf(ConvertToUnSign(fullText), 
					StringComparison.InvariantCultureIgnoreCase) != -1).ToList();
				if (searchComboBox.Items.Count == 0)
				{
					SearchNotificationComboBox.IsDropDownOpen = true;
					searchComboBox.IsDropDownOpen = false;
				}
			}
			else
			{
				searchComboBox.ItemsSource = CakeInfoList;
			}

		}

		private void ColorButton_Click(object sender, RoutedEventArgs e)
		{
			var datatContex = (sender as Button).DataContext;
			var color = (datatContex as ColorSetting).Color;
			ColorScheme = color;
			TitleBar.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
			SettingTextBlock.Background = TitleBar.Background;
			SettingTitleTextBlock.Foreground = SettingTextBlock.Background;
		}

		private void ShowSplashScreenCheckBox_Checked(object sender, RoutedEventArgs e)
		{
			//var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			//config.AppSettings.Settings["ShowSplashScreen"].Value = "true";
			//config.Save(ConfigurationSaveMode.Minimal);
		}

		private void ShowSplashScreenCheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			//var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			//config.AppSettings.Settings["ShowSplashScreen"].Value = "false";
			//config.Save(ConfigurationSaveMode.Minimal);
		}

		//---------------------------------------- Các hàm xử lý khác --------------------------------------------//

		private void CakeListAppearAnimation()
		{
			ThicknessAnimation animation = new ThicknessAnimation();
			animation.AccelerationRatio = 0.9;
			animation.From = new Thickness(15, 60, 0, 0);
			animation.To = new Thickness(15, 6, 0, 0);
			animation.Duration = TimeSpan.FromSeconds(0.5);
			CakeListGrid.BeginAnimation(Grid.MarginProperty, animation);
		}

		private string FormatPriceString(string price)
		{

			var i = price.Length - 1;
			var count = 0;

			while (i > 0)
			{
				count++;
				if (count % 3 == 0)
				{
					price = price.Insert(i, ".");
				}
				else
				{
					//Do nothing
				}
				i--;
			}

			return price;

		}
	}
}
