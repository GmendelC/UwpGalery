using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.UI.Xaml;

namespace UWPUtiles
{
    public class Tiles
    {
        DispatcherTimer timer;
        public Tiles()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += ChageTile;
        }

        private void ChageTile(object sender, object e)
        {
            var tile = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text01);
            var text = tile.GetElementsByTagName("text");
            text[0].InnerText = DateTime.Now.ToString();
            // tile.Attributes[0].AppendChild(tile.CreateTextNode("Bla"));

            var notifier = new TileNotification(tile);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(notifier);
        }

        public void Start()
        {
            timer.Start();
        }
    }
}
