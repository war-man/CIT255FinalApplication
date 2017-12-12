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
    /// <summary>
    /// The first form you come to
    /// </summary>
    public partial class FormWelcome : Form
    {
        /// <summary>
        /// The constructor for this form that pulls the weather data from the Api
        /// </summary>
        public FormWelcome()
        {
            InitializeDataFileXML.PullDataApi();

            InitializeComponent();
        }

        /// <summary>
        /// Closes the current form and opens the api pull form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBegin_Click(object sender, EventArgs e)
        {
            FormFreshAPIPull formFreshApiPull = new FormFreshAPIPull(AppEnum.ManagerAction.GetWeather);           

            this.Hide();
            formFreshApiPull.Show();
        }        

        /// <summary>
        /// closes the form and ends the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
