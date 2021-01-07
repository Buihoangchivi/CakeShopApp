using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class CakeImage : INotifyPropertyChanged
{
	private string imagePath;
	public string ImagePath
	{
		get
		{
			return imagePath;
		}
		set
		{
			imagePath = value;
			OnPropertyChanged("ImagePath");
		}
	}

	public CakeImage()
	{
		this.imagePath = "";
	}
	public CakeImage(string filename)
	{
		this.imagePath = filename;
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
