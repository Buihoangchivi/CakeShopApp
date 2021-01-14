using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace CakeShopApp
{
    class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
			{

                return "";

			}
            var price = (string)value;
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

            return price + " ₫";
        }

        public static BitmapImage ConvertToImage(string path)
        {

            BitmapImage bitmapImage = null;
            if (File.Exists(path))
            {
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new FileStream(path, FileMode.Open, FileAccess.Read);
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.StreamSource.Dispose();
            }

            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
