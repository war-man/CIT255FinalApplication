using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using Model;
using BusinessLayer;
using WeatherToPlant.Properties;

namespace WeatherToPlant
{
    public partial class FormCustomizePlantingDays : Form
    {
        static IResponseRepository _responseRepository;

        public FormCustomizePlantingDays(IResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;

            InitializeComponent();

            SetBlankCalendar();

            AutoFillPlantingDays();
        }

        private void AutoFillPlantingDays()
        {
            ResponseBusiness responseBusiness = new ResponseBusiness(_responseRepository);
            Response response;

            using (responseBusiness)
            {
                responseBusiness.AutoFillPlantingDays();
                response = responseBusiness.SelectAll();
            }

            FillWeatherDays(response, AppEnum.ManagerAction.AutoFillPlantingDays);
        }

        private void SetBlankCalendar()
        {
            // get the weather, but only display the days for now
            // b/c more fun for a user to see the process from blank calendar to 
            // a calendar filled with weather icons
            ResponseBusiness responseBusiness = new ResponseBusiness(_responseRepository);
            Response response;

            using (responseBusiness)
            {
                response = responseBusiness.SelectAll();
            }

            FillWeatherDays(response, AppEnum.ManagerAction.CalendarOnly);
        }

        private void SetWeatherIcons()
        {
            ResponseBusiness responseBusiness = new ResponseBusiness(_responseRepository);
            Response response;

            using (responseBusiness)
            {
                response = responseBusiness.SelectAll();
            }

            FillWeatherDays(response, AppEnum.ManagerAction.GetWeather);
        }

        private void FillWeatherDays(Response response, AppEnum.ManagerAction actionChoice)
        {
            int indexR = 0;

            for (int r = 0; r < tblFreshAPI.RowCount; r++)
            {
                for (int c = 0; c < tblFreshAPI.ColumnCount; c++)
                {
                    Control control = tblFreshAPI.GetControlFromPosition(c, r);
                    //
                    // apply names to the labels
                    //
                    if (control is Label)
                    {
                        //
                        // cast the control as a Label
                        //
                        Label iconLabel = control as Label;
                        if (iconLabel != null)
                        {
                            //
                            // get the weekday from the response object
                            //
                            string date = response.Forecast.Simpleforecast.Forecastdays.Forecastday[indexR].Date.Weekday;
                            iconLabel.Text = date;
                            indexR += 1;
                        }
                        //
                        // reset the response index, because the row after a label row will be the same set of days, whereas after a row of picture boxes, you want it to keep incrementing to the next set of five days in the forecast
                        //
                        //
                        // the end of the row is actually column = 4, because they start their numbering at 0,0 instead of 1,1 to be more programmer-ish
                        //
                        if (c == 4)
                        {
                            if (r == 0)
                            {
                                indexR = 0;
                            }
                            else if (r == 2)
                            {
                                indexR = 5;
                            }
                        }
                    }
                    else //apply images to picture boxes
                    {
                        PictureBox picture = control as PictureBox;
                        Forecastday fd = response.Forecast.Simpleforecast.Forecastdays.Forecastday[indexR];

                        switch (actionChoice)
                        {
                            case AppEnum.ManagerAction.None:
                                break;
                            case AppEnum.ManagerAction.WelcomePage:
                                break;
                            case AppEnum.ManagerAction.GetWeather:
                                if (fd.IsRainyDay)
                                {
                                    picture.Image = Resources.raindrop;
                                }
                                break;
                            case AppEnum.ManagerAction.CustomizePlantingDay:
                                break;
                            case AppEnum.ManagerAction.AutoFillPlantingDays:
                                if (fd.IsPlantingDay)
                                {
                                    picture.Image = Resources.seedling;
                                }
                                else if (fd.IsRainyDay)
                                {
                                    picture.Image = Resources.raindrop;
                                }
                                break;
                            case AppEnum.ManagerAction.TogglePlantingDay:
                                if (fd.IsPlantingDay)
                                {
                                    picture.Image = Resources.seedling;
                                }
                                else if (fd.IsRainyDay)
                                {
                                    picture.Image = Resources.raindrop;
                                }
                                else
                                {
                                    picture.Image = Resources.blankscreen;
                                }
                                break;
                            case AppEnum.ManagerAction.Print:
                                break;
                            case AppEnum.ManagerAction.Exit:
                                break;
                            case AppEnum.ManagerAction.CalendarOnly:
                                picture.Image = Resources.blankscreen;
                                break;
                            default:
                                break;
                        }

                        indexR += 1;
                    }
                }
            }
        }


        private void btnStartOver_Click(object sender, EventArgs e)
        {
            FormFreshAPIPull formFreshApiPull = new FormFreshAPIPull(AppEnum.ManagerAction.GetWeather);

            this.Hide();
            formFreshApiPull.Show();
        }
        

        private void btnAutoFill_Click(object sender, EventArgs e)
        {
            FormFreshAPIPull formFreshApiPull = new FormFreshAPIPull(AppEnum.ManagerAction.AutoFillPlantingDays);

            this.Hide();
            formFreshApiPull.Show();
        }

        private void CommonClick_TogglePlantingDay(object sender, EventArgs e)
        {
            ResponseBusiness responseBusiness = new ResponseBusiness(_responseRepository);
            Response response;

            using (responseBusiness)
            {
                response = responseBusiness.SelectAll();
            }

            PictureBox picture = sender as PictureBox;

            TogglePlantingDay(sender, response);

            responseBusiness = null;

            responseBusiness = new ResponseBusiness(_responseRepository);

            using (responseBusiness)
            {
                response = responseBusiness.SelectAll();
            }

            FillWeatherDays(response, AppEnum.ManagerAction.TogglePlantingDay);
        }

        private void TogglePlantingDay(object sender, Response response)
        {
            int indexR = 0;

            for (int r = 0; r < tblFreshAPI.RowCount; r++)
            {
                for (int c = 0; c < tblFreshAPI.ColumnCount; c++)
                {
                    Control control = tblFreshAPI.GetControlFromPosition(c, r);

                    Forecastday fd = response.Forecast.Simpleforecast.Forecastdays.Forecastday[indexR];

                    if (control is PictureBox)
                    {
                        PictureBox currentPB = control as PictureBox;

                        if (currentPB == sender)
                        {
                            ResponseBusiness responseBusiness = new ResponseBusiness(_responseRepository);

                            using (responseBusiness)
                            {
                                responseBusiness.TogglePlantingDay(fd.Period);
                            }
                        }
                    }

                    indexR += 1;

                    if (c == 4)
                    {
                        if (r == 0)
                        {
                            indexR = 0;
                        }
                        else if (r == 2)
                        {
                            indexR = 5;
                        }
                    }
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
