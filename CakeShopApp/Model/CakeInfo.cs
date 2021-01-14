using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class CakeInfo : INotifyPropertyChanged
{
	private string cakeName;
	private string id;
	private string price;
	private long number;
	private string total;
	private string category;
	private string primaryImagePath;        //Đường dẫn ảnh chính

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
	public long Number
	{
		get
		{
			return number;
		}
		set
		{
			number = value;
			OnPropertyChanged("Number");
		}
	}
	public string Total
	{
		get
		{
			return total;
		}
		set
		{
			total = value;
			OnPropertyChanged("Total");
		}
	}
	public string Category
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

