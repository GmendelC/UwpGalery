using ImageLibary.Model;
using Project.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace Project.Convertes
{
   public class StringToPageConverter : IValueConverter
    {
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                    if (typeof(GaleryImageView) == value)
                        return EPage.galery;
                if (typeof(EditImageView) == value)
                        return EPage.edit;
            }
            return EPage.galery;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                switch ( (EPage)value)
                {
                    case EPage.galery:
                        return typeof(GaleryImageView);
                    case EPage.edit:
                        return typeof(EditImageView);
                }
            }
            return typeof(GaleryImageView);
        }
    }
}
