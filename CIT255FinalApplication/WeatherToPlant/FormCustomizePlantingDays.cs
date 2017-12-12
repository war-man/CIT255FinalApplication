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
    /// <summary>
    /// A form used to toggle each day between planting day or non-planting day
    /// </summary>
    public partial class FormCustomizePlantingDays : Form
    {
        static IResponseRepository _responseRepository;

        /// <summary>
        /// the constructor for this form that fills a calendar with existing data from the API and the business logic from planting days
        /// </summary>
        /// <param name="responseRepository"></param>
        public FormCustomizePlantingDays(IResponseRepository responseRepository)
        {
            _responseRepository = responseRepository;

            InitializeComponent();

            SetBlankCalendar();

            AutoFillPlantingDays();
        }

        /// <summary>
        /// instantiates a business repository and uses that to apply the business logic for planting days, and saves that business repository to a reponse object, and passes that into the method that fills the table layout panel calendar with weather and planting days both
        /// </summary>
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

        /// <summary>
        /// Define a response object from the repository and use it to fill the calendar with the names of the days of the week, but nothing else
        /// </summary>
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

        /// <summary>
        /// A method that iterates through the table layout panel for the calendar, and depending on the enum that gets sent to it, will either fill in only the names of the days of the week, the days plus the weather icons, or all of the above with the planting days too
        /// </summary>
        /// <param name="response"></param>
        /// <param name="actionChoice"></param>
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

        /// <summary>
        /// Event handler for the StartOver button. Closes the current form and opens the api pull form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartOver_Click(object sender, EventArgs e)
        {
            FormFreshAPIPull formFreshApiPull = new FormFreshAPIPull(AppEnum.ManagerAction.GetWeather);

            this.Hide();
            formFreshApiPull.Show();
        }
        
        /// <summary>
        /// event handler for the AutoFill button. closes the current form and opens the api pull form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoFill_Click(object sender, EventArgs e)
        {
            FormFreshAPIPull formFreshApiPull = new FormFreshAPIPull(AppEnum.ManagerAction.AutoFillPlantingDays);

            this.Hide();
            formFreshApiPull.Show();
        }

        /// <summary>
        /// the event handler for all picture boxes in the table layout panel calendar that allows the user to toggle back and forth between having a day be a planting day or not
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Iterate through the calendar and find the picture box that matches the sender (the clicked object), and then change that forecast day's IsPlantingDay bool, and updates the picture to reflect that change
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="response"></param>
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

        /// <summary>
        /// event handler for the exit button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
