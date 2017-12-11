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

        public FormWelcome()
        {
            InitializeDataFileXML.PullDataApi();
            //TODO: make sure you really DON'T need the repository here... don't think so
            //_responseRepository = new ResponseRepositoryXML();

            InitializeComponent();
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            FormFreshAPIPull formFreshApiPull = new FormFreshAPIPull(AppEnum.ManagerAction.GetWeather);           

            this.Hide();
            formFreshApiPull.Show();
        }
    }
}
