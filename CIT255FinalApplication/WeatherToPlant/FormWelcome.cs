using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using DAL;
using Data;

namespace WeatherToPlant
{
    public partial class FormWelcome : Form
    {
        static IResponseRepository _responseRepository;

        public FormWelcome(AppEnum.ManagerAction actionChoice)
        {
            switch (actionChoice)
            {
                case AppEnum.ManagerAction.None:
                    break;
                case AppEnum.ManagerAction.WelcomePage:
                    InitializeDataFileXML.PullDataApi();

                    InitializeComponent();
                    break;
                case AppEnum.ManagerAction.GetWeather:
                    break;
                case AppEnum.ManagerAction.CustomizePlantingDay:
                    break;
                case AppEnum.ManagerAction.AutoFillPlantingDays:
                    break;
                case AppEnum.ManagerAction.TogglePlantingDay:
                    break;
                case AppEnum.ManagerAction.CalendarOnly:
                    break;
                case AppEnum.ManagerAction.Print:
                    break;
                case AppEnum.ManagerAction.Exit:
                    Close();
                    break;
                default:
                    break;
            }
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            FormFreshAPIPull formFreshApiPull = new FormFreshAPIPull(AppEnum.ManagerAction.GetWeather);           

            this.Hide();
            formFreshApiPull.Show();
        }        

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
