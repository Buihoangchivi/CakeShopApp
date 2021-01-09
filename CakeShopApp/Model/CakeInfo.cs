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
	private int number;
	private int category;

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
	public int Number
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

