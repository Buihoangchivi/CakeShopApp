using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class Cake : INotifyPropertyChanged
{
	private string cakeName;
	private string id;
	private int category;
	private string price;
	private string primaryImagePath;        //Đường dẫn ảnh chính
	private BindingList<CakeImage> imagesList;
	private string description;

	public string CakeName
	{
		get
		{
			return cakeName;
		}
		set
		{
			cakeName = value;
			OnPropertyChanged("CakeName");
		}
	}
	public string ID
	{
		get
		{
			return id;
		}
		set
		{
			id = value;
			OnPropertyChanged("ID");
		}
	}
	public int Category
	{
		get
		{
			return category;
		}
		set
		{
			category = value;
			OnPropertyChanged("Category");
		}
	}
	public string Price
	{
		get
		{
			return price;
		}
		set
		{
			price = value;
			OnPropertyChanged("Price");
		}
	}
	public string PrimaryImagePath
	{
		get
		{
			return primaryImagePath;
		}
		set
		{
			primaryImagePath = value;
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("PrimaryImagePath"));
			}
		}
	}
	public BindingList<CakeImage> ImagesList
	{
		get
		{
			return imagesList;
		}
		set
		{
			imagesList = value;
			OnPropertyChanged("ImagesList");
		}
	}
	public string Description
	{
		get
		{
			return description;
		}
		set
		{
			description = value;
			OnPropertyChanged("Description");
		}
	}

	#region INotifyPropertyChanged Members  

	public event PropertyChangedEventHandler PropertyChanged;
	private void OnPropertyChanged(string propertyName)
	{
		if (PropertyChanged != null)
		{
			PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}

	#endregion

}

