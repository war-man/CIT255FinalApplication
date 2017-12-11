﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BusinessLayer;
using DAL;
using Model;
using WeatherToPlant.Properties;
using Data;
using System.Reflection;

namespace WeatherToPlant
{
    public partial class FormFreshAPIPull : Form
    {
        static IResponseRepository _responseRepository;

        public FormFreshAPIPull(AppEnum.ManagerAction actionChoice)
        {
            switch (actionChoice)
            {
                case AppEnum.ManagerAction.None:
                case AppEnum.ManagerAction.WelcomePage:
                    break;
                case AppEnum.ManagerAction.GetWeather:
                    FreshApiPullGetWeather();
                    break;
                case AppEnum.ManagerAction.CustomizePlantingDay:
                    break;
                case AppEnum.ManagerAction.AutoFillPlantingDays:
                    FreshApiPullAutoFill();
                    break;
                case AppEnum.ManagerAction.TogglePlantingDay:
                case AppEnum.ManagerAction.CalendarOnly:
                case AppEnum.ManagerAction.Print:
                case AppEnum.ManagerAction.Exit:
                default:
                    break;
            }            
        }

        private void FreshApiPullAutoFill()
        {
            RefreshApiPull();

            InitializeComponent();

            SetButtonsAfterInitialGetWeather();

            AutoFillPlantingDays();
        }        

        private void FreshApiPullGetWeather()
        {
            InitializeDataFileXML.PullDataApi();

            _responseRepository = null;

            _responseRepository = new ResponseRepositoryXML();

            InitializeComponent();

            SetBlankCalendar();
        }

        private void SetButtonsAfterInitialGetWeather()
        {
            //
            // we only want to show "Get Weather" button once. After they click it, they should only see a "Start Over" button at the buttom so they know they will lose their current data by pressing that button
            //
            btnGetWeather.Hide();
            //
            // the other buttons can now become visible
            //
            btnAutoFill.Show();
            btnCustomize.Show();
            btnStartOver.Show();
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

        private void btnGetWeather_Click(object sender, EventArgs e)
        {
            AssignRainyDays();

            SetWeatherIcons();

            SetButtonsAfterInitialGetWeather();
        }

        private void AssignRainyDays()
        {
            ResponseBusiness responseBusiness = new ResponseBusiness(_responseRepository);

            using (responseBusiness)
            {
                responseBusiness.AssignIsRainyDay();
            }
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
            //ClearTable();

            //FreshApiPullGetWeather();

            this.Refresh();

            RefreshApiPull();

            ClearTable();

            SetWeatherIcons();
        }

        private void ClearTable()
        {
            ResponseBusiness responseBusiness = new ResponseBusiness(_responseRepository);
            Response response;

            using (responseBusiness)
            {
                response = responseBusiness.SelectAll();
            }

            FillWeatherDays(response, AppEnum.ManagerAction.CalendarOnly);
        }

        private void RefreshApiPull()
        {
            InitializeDataFileXML.PullDataApi();

            _responseRepository = null;

            _responseRepository = new ResponseRepositoryXML();            

            AssignRainyDays();
        }

        private void btnAutoFill_Click(object sender, EventArgs e)
        {
            //TODO: change the Hide Show of the customize button to a grayed out version of the button instead
            btnCustomize.Hide();

            AutoFillPlantingDays();

            btnCustomize.Show();
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

        private void btnCustomize_Click(object sender, EventArgs e)
        {
            FormCustomizePlantingDays formCustomizePlantingDays = new FormCustomizePlantingDays(_responseRepository);

            this.Hide();
            formCustomizePlantingDays.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
