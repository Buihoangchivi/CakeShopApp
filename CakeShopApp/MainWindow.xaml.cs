using Microsoft.Win32;
using System;
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

namespace CakeShopApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
        {
			
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

        private void CancelTripButton_Click(object sender, RoutedEventArgs e)
        {
			////Đóng màn hình thêm chuyến đi
			//AddTripGrid.Visibility = Visibility.Collapsed;
			////Tắt màu của nút Add
			//var wrapPanel = (WrapPanel)AddTripButton.Content;
			//var collection = wrapPanel.Children;
			//var block = (TextBlock)collection[0];
			//var text = (TextBlock)collection[2];
			//block.Background = Brushes.Transparent;
			//text.Foreground = Brushes.Black;

			//if (isEditMode == true)
			//{
			//	//Quay ve man hinh chi tiet
			//	DetailTripGrid.DataContext = TripInfoList[selectedTripIndex];
			//	DetailTripGrid.Visibility = Visibility.Visible;
			//	ControlStackPanel.Visibility = Visibility.Visible;

			//	//Tắt chế độ chỉnh sửa
			//	isEditMode = false;
			//	IsDetailTrip = true;
			//}
			//else
			//{
			//	//Quay về màn hình Home
			//	clickedControlButton = HomeButton;
			//	TripListGrid.Visibility = Visibility.Visible;
			//	TypeBarDockPanel.Visibility = Visibility.Visible;
			//	ControlStackPanel.Visibility = Visibility.Visible;
			//}

			////Hiển thị màu cho nút Home
			//wrapPanel = (WrapPanel)HomeButton.Content;
			//collection = wrapPanel.Children;
			//block = (TextBlock)collection[0];
			//text = (TextBlock)collection[2];
			//block.Background = (SolidColorBrush)new BrushConverter().ConvertFromString(ColorScheme);
			//text.Foreground = block.Background;

			//clickedControlButton = HomeButton;

			////Cập nhật lại giao diện
			//UpdateUIFromData();
		}

      

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
			//var fileDialog = new OpenFileDialog();
			//fileDialog.Multiselect = true;
			//fileDialog.Filter = "Image Files(*.JPG*;*.JPEG*;*.PNG*)|*.JPG;*.JPEG*;*.PNG*";
			//fileDialog.Title = "Select Image";

			//if (fileDialog.ShowDialog() == true)
			//{
			//	var fileNames = fileDialog.FileNames;
			//	foreach (var filename in fileNames)
			//	{
			//		trip.ImagesList.Add(new TripImage(filename));
			//	}
			//}
		}

      

        private void DeleteImageButton_Click(object sender, RoutedEventArgs e)
        {
			//trip.ImagesList.Remove(ImagesListView.SelectedItem as TripImage);
		}

      

       

        private void SaveTripButton_Click(object sender, RoutedEventArgs e)
        {
			//if (isEditMode == false)
			//{
			//	string appFolder = GetAppDomain();
			//	for (int i = 0; i < trip.ImagesList.Count; i++)
			//	{
			//		var imageExtension = System.IO.Path.GetExtension(trip.ImagesList[i].ImagePath);
			//		var newImageName = $"Images/{trip.TripID}_{i}{imageExtension}";
			//		var newPath = appFolder + newImageName;
			//		File.Copy(trip.ImagesList[i].ImagePath, newPath, true);
			//		trip.ImagesList[i].ImagePath = newImageName;
			//	}
			//	trip.PrimaryImagePath = trip.ImagesList[0].ImagePath;
			//	TripInfoList.Add(trip);
			//}
			//else
			//{
			//	string appFolder = GetAppDomain();
			//	for (int i = 0; i < trip.ImagesList.Count; i++)
			//	{
			//		TripImage currentImage = trip.ImagesList[i];
			//		var imageExtension = System.IO.Path.GetExtension(currentImage.ImagePath);
			//		var newImageName = $"Images/{trip.TripID}_{i}{imageExtension}";
			//		var newPath = appFolder + newImageName;
			//		if (System.IO.Path.IsPathRooted(currentImage.ImagePath))
			//		{
			//			File.Copy(currentImage.ImagePath, newPath, true);
			//			trip.ImagesList[i].ImagePath = newImageName;
			//		}
			//		else
			//		{
			//			if (currentImage.ImagePath != TripInfoList[selectedTripIndex].ImagesList[i].ImagePath)
			//			{
			//				File.Delete(appFolder + TripInfoList[selectedTripIndex].ImagesList[i].ImagePath);
			//				File.Move(appFolder + currentImage.ImagePath, newPath);
			//				currentImage.ImagePath = newImageName;
			//			}
			//		}
			//	}
			//	if (trip.ImagesList.Count > 0)
			//	{
			//		trip.PrimaryImagePath = trip.ImagesList[0].ImagePath;
			//	}
			//	else
			//	{
			//		trip.PrimaryImagePath = "";
			//	}
			//	TripInfoList[selectedTripIndex] = trip;
			//}

			////Đóng giao diện thêm/chỉnh sửa và mở giao diện trang chủ
			//CancelTripButton_Click(null, null);
		}

      

  //      private void Description_LostFocus(object sender, RoutedEventArgs e)
  //      {
		//	if (string.IsNullOrWhiteSpace(Description.Text))
		//	{
		//		Description.Text = "Nhập văn bản ở đây...";
		//	}
		//}

  //      private void Description_GotFocus(object sender, RoutedEventArgs e)
  //      {
		//	if (Description.Text == "Nhập văn bản ở đây...")
		//	{
		//		Description.Text = "";
		//	}
		//}
    }
}
