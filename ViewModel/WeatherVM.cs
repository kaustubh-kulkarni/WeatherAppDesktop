using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using WeatherApp.Model;
using WeatherApp.ViewModel.Commands;
using WeatherApp.ViewModel.Helpers;

namespace WeatherApp.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        //Call the method when prop changes i.e when setter is there
        private string query;

        public string Query
        {
            get => query;
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }

        //Observable collection
        public ObservableCollection<City> Cities { get; set; }

        //Prop for weather conditions
        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get => currentConditions;
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }

        //Prop for cities
        private City selectedCity;

        public City SelectedCity
        {
            get => selectedCity;
            set
            {
                selectedCity = value;
                if(selectedCity != null)
                {
                    OnPropertyChanged("SelectedCity");
                    GetCurrentConditions();
                }
                
            }
        }

        //Prop for executing the ICommand interface
        public SearchCommand SearchCommand { get; set; }

        //Ctor
        public WeatherVM()
        {
            //When application is not running
            if(DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                selectedCity = new City
                {
                    LocalizedName = "Frankfurt"
                };
                currentConditions = new CurrentConditions
                {
                    WeatherText = "Snow",
                    Temperature = new Temperature
                    {
                        Metric = new Units
                        {
                            Value = "21"
                        }
                    }
                };
            }

            //When application is running, initialize the search command
            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
            
        }

        //Method to actually retrieve the current conditions when city changes
        //It will be called after the prop change on setter
        public async void GetCurrentConditions()
        {
            //Making the query empty as user already has the city
            Query = string.Empty;
            //Get the current conditions for new city in this prop we declared above
            CurrentConditions = await AccuWeatherHelper.GetCurrentConditions(SelectedCity.Key);
            //Clearing the city as the user now will try to fetch other city
            Cities.Clear();
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
            //Clear any value if it consist
            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Trigger the prop changed event
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
