using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace UWPUtiles
{
    public class ProgramMessageDialog
    {
        public static async Task<bool> YesNoDialog(string menssage, string title = null)
        {
            MessageDialog mD;
            if (string.IsNullOrEmpty(title))
            {
                 mD = new MessageDialog(menssage);
            }
            else
            {
                 mD = new MessageDialog(title, menssage);
            }

            bool res = false;

            mD.Commands.Add(new UICommand("Yes", (x) => {
                res = true;
            }));

            mD.Commands.Add(new UICommand("No"));

            await mD.ShowAsync();

            return res;
        }
    }
}
