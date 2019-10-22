using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KinoAplikacija.User_Controls.MainPanels.Language
{
    static class LangLoc
    {
        internal static void setlanguage(this Form form, CultureInfo lang) {
            System.Threading.Thread.CurrentThread.CurrentCulture = lang;
            System.Threading.Thread.CurrentThread.CurrentUICulture = lang;

        }
    }
}
