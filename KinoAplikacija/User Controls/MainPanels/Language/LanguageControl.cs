using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KinoAplikacija.User_Controls.MainPanels.Normal;

namespace KinoAplikacija.User_Controls.MainPanels.Language
{
    public partial class LanguageControl : UserControl
    {
        public List<LanguageClass> langs;
        Form form;
        public LanguageControl(Form f)
        {
            InitializeComponent();
            form = f;
            langs = new List<LanguageClass>();
            langs.Add(new LanguageClass() { id = 0, name="Slovenščina" });
            langs.Add(new LanguageClass() { id = 1, name = "English" });
            langs.Add(new LanguageClass() { id = 2, name = "Deutsch" });
            comboBox1.DataSource = langs;
            comboBox1.DisplayMember = "name";
            comboBox1.ValueMember = "id";
            if (Properties.Settings.Default.language == "Slovenščina")
            {
                setLanguage("Slovenščina");
                comboBox1.SelectedIndex = comboBox1.FindStringExact("Slovenščina");
            }
            else if (Properties.Settings.Default.language == "English")
            {
                setLanguage("English");
                comboBox1.SelectedIndex = comboBox1.FindStringExact("English");
            }
            else
            {
                setLanguage("Deutsch");
                comboBox1.SelectedIndex = comboBox1.FindStringExact("Deutsch");
            }

        }
        private void setLanguage(string lang) {
            if (lang.Equals("Slovenščina"))
            {
                LangLoc.setlanguage(form, new System.Globalization.CultureInfo("sl-SI"));
                Properties.Settings.Default.language = "Slovenščina";
                Properties.Settings.Default.Save();

            }
            else if (lang.Equals("English"))
            {
                LangLoc.setlanguage(form, new System.Globalization.CultureInfo("en-US"));
                Properties.Settings.Default.language = "English";
                Properties.Settings.Default.Save();
            }
            else if (lang.Equals("Deutsch"))
            {
                LangLoc.setlanguage(form, new System.Globalization.CultureInfo("de-DE"));
                Properties.Settings.Default.language = "Deutsch";
                Properties.Settings.Default.Save();
            }
            if (form.GetType() == typeof(HomeForm))
            {
                ((HomeForm)form).setTextLanguage();
                foreach (Control c in ((HomeForm)form).startform.flowLayoutPanel1.Controls) {
                    if (c.GetType() == typeof(LanguageControl))
                    {
                        LanguageControl uc = (LanguageControl)c;
                        uc.comboBox1.SelectedIndex = comboBox1.FindStringExact(Properties.Settings.Default.language);
                    }
                }
                ((HomeForm)form).startform.setTextLanguage();
                foreach (Control c in ((HomeForm)form).SidePanel.Controls)
                {
                    if (c.GetType() == typeof(UserSidePanel))
                    {
                        UserSidePanel uc = (UserSidePanel)c;
                        uc.setTextLanguage();
                    }
                }
                foreach (Control c in ((HomeForm)form).MainPanel.Controls)
                {
                    if (c.GetType() == typeof(ProfileControl))
                    {
                        ProfileControl pc = (ProfileControl)c;
                        pc.setTextLanguage();
                    }
                    if (c.GetType() == typeof(UserEventsControl))
                    {
                        UserEventsControl uec = (UserEventsControl)c;
                        uec.setTextLanguage();
                    }
                    if (c.GetType() == typeof(UserBillsControl))
                    {
                        UserBillsControl ubc = (UserBillsControl)c;
                        ubc.setTextLanguage();
                    }
                }
            } else if (form.GetType() == typeof(StartForm))
            {
                ((StartForm)form).setTextLanguage();
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            setLanguage(comboBox1.Text);
        }
    }
}
