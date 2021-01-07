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
	private double price;
	private int number;

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
	public double Price
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

