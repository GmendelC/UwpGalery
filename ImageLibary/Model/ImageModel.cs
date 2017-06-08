using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ImageLibary.Model
{
    public class ImageModel : INotifyPropertyChanged
    {
        private int _id;
        public int Id { get { return _id; }
            set {
                if (_id != value) { 
                _id = value;
                Update("ID");
                }
            } }
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    Update("Name");
                }
            }
        }

        private string _paht;
        public string Paht {
            get { return _paht; }
            set
            {
                if (_paht != value & !value.Contains("BitmapImage"))
                {
                    _paht = value;
                    Update("Paht");
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    Update("Description");
                }
            }
        }
        private string _locatio;
        public string Location
        {
            get { return _locatio; }
            set
            {
                if (_locatio != value)
                {
                    _locatio = value;
                    Update("Location");
                }
            }
        }

        private void Update(string propName)
        {
            PropertyChanged?.Invoke( this , new  PropertyChangedEventArgs( propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
