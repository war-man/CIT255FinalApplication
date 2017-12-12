using System;
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
    /// <summary>
    /// The form where the weather data is retrieved and placed onto a calendar
    /// </summary>
    public partial class FormFreshAPIPull : Form
    {
        static IResponseRepository _responseRepository;

        /// <summary>
        /// The constructor for this form. Allows you to open this form from multiple places and jump to different functionality depending on the enum passed in
        /// </summary>
        /// <param name="actionChoice"></param>
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

        /// <summary>
        /// This refreshes the data from the API, initializes the winforms components, hides the GetWeather buttons and shows the other buttons, and then fills the calendar with the planting days
        /// </summary>
        private void FreshApiPullAutoFill()
        {
            RefreshApiPull();

            InitializeComponent();

            SetButtonsAfterInitialGetWeather();

            AutoFillPlantingDays();
        }        

        /// <summary>
        /// This refreshes the data and initializes the form with a calendar using the days from the forecast, but leaving the contents empty to allow the user to click on a Get Weather button themselves
        /// </summary>
        private void FreshApiPullGetWeather()
        {
            InitializeDataFileXML.PullDataApi();

            _responseRepository = null;

            _responseRepository = new ResponseRepositoryXML();

            InitializeComponent();

            SetBlankCalendar();
        }

        /// <summary>
        /// This hides the GetWeather button and shows the other buttons because we only want to show "Get Weather" button once. After they click it, they should only see a "Start Over" button at the buttom so they know they will lose their current data by pressing that button
        /// </summary>
        private void SetButtonsAfterInitialGetWeather()
        {
            btnGetWeather.Hide();

            btnAutoFill.Show();
            btnCustomize.Show();
            btnStartOver.Show();
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
        /// Event handler for the GetWeather button: Assigns the rainy day logic, displays the weather icons on the calendar, and adjusts the UI buttons on the screen accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetWeather_Click(object sender, EventArgs e)
        {
            AssignRainyDays();

            SetWeatherIcons();

            SetButtonsAfterInitialGetWeather();
        }

        /// <summary>
        /// Calls the AssignRainyDay() method from the business class repository
        /// </summary>
        private void AssignRainyDays()
        {
            ResponseBusiness responseBusiness = new ResponseBusiness(_responseRepository);

            using (responseBusiness)
            {
                responseBusiness.AssignIsRainyDay();
            }
        }

        /// <summary>
        /// Fills the calendar with icons for the weather
        /// </summary>
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
        /// Event handler for the StartOver button. Refreshes the data from the API, clears the current calendar, and resets the weather icons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStartOver_Click(object sender, EventArgs e)
        {
            this.Refresh();

            RefreshApiPull();

            ClearTable();

            SetWeatherIcons();
        }

        /// <summary>
        /// removes all images from the table layout panel for the calendar, but keeps the days of the week visible
        /// </summary>
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

        /// <summary>
        /// nullifies the response repository, and refreshes the data from the API, saving the new data to a newly instantiated repository, then assigns rainy day logic
        /// </summary>
        private void RefreshApiPull()
        {
            InitializeDataFileXML.PullDataApi();

            _responseRepository = null;

            _responseRepository = new ResponseRepositoryXML();            

            AssignRainyDays();
        }

        /// <summary>
        /// event handler for the Auto Fill button: applies the business logic for planting days, and displays it on the screen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoFill_Click(object sender, EventArgs e)
        {
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
        /// event handler for the customize button. closes the current form and opens a customize form, passing the response repository into it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCustomize_Click(object sender, EventArgs e)
        {
            FormCustomizePlantingDays formCustomizePlantingDays = new FormCustomizePlantingDays(_responseRepository);

            this.Hide();
            formCustomizePlantingDays.Show();
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
