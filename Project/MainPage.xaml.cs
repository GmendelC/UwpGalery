using ImageLibary;
using Project.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Project
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainViewModel Source { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            Source = new MainViewModel();
            Source.OnNavigateToEdit += Source_OnNavigateToEdit;
            Source.OnNavigateToGalery += Source_OnNavigateToGalery;
            Source_OnNavigateToGalery(this, null);
        }

        
        private async void Source_OnNavigateToGalery(object sender, EventArgs e)
        {
          await   Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High 
              ,()=> { contentFrame.Navigate(typeof(GaleryImageView), Source);
                  lbMenu.SelectedIndex = 0;
              });
        }

        private async void Source_OnNavigateToEdit(object sender, EventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.High
            , () => { contentFrame.Navigate(typeof(EditImageView), Source);
                lbMenu.SelectedIndex = 1;
            });
        }

        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            int ind = ((ComboBox)sender).SelectedIndex;

            switch (ind)
            {
                case 0:
                    RequestedTheme = ElementTheme.Light;
                    break;
                case 1:
                    RequestedTheme = ElementTheme.Dark;
                    break;
                default:
                    RequestedTheme = ElementTheme.Default;
                    break;
            }
        }
    }
}
