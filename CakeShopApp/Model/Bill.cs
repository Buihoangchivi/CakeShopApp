using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

public class Bill : INotifyPropertyChanged
{
	private string id;
	private string customerName;
	private string phoneNumber;
	private string address;
	private int paymentMethod;
	private string totalMoney;
	private string date;
	private BindingList<CakeInfo> cakesList;

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
	public string CustomerName
	{
		get
		{
			return customerName;
		}
		set
		{
			customerName = value;
			OnPropertyChanged("CustomerName");
		}
	}
	public string PhoneNumber
	{
		get
		{
			return phoneNumber;
		}
		set
		{
			phoneNumber = value;
			OnPropertyChanged("PhoneNumber");
		}
	}
	public string Address
	{
		get
		{
			return address;
		}
		set
		{
			address = value;
			OnPropertyChanged("Address");
		}
	}
	public int PaymentMethod
	{
		get
		{
			return paymentMethod;
		}
		set
		{
			paymentMethod = value;
			OnPropertyChanged("PaymentMethod");
		}
	}
	public string TotalMoney
	{
		get
		{
			return totalMoney;
		}
		set
		{
			totalMoney = value;
			OnPropertyChanged("TotalMoney");
		}
	}
	public string Date
	{
		get
		{
			return date;
		}
		set
		{
			date = value;
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("Date"));
			}
		}
	}
	public BindingList<CakeInfo> CakesList
	{
		get
		{
			return cakesList;
		}
		set
		{
			cakesList = value;
			OnPropertyChanged("CakesList");
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

