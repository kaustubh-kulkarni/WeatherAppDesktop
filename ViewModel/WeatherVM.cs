using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WeatherApp.Model;

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

        //Prop for weather conditions
        private CurrentConditions currentConditions;

        public CurrentConditions CurrentConditions
        {
            get => currentConditions;
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentCondition");
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
                OnPropertyChanged("SelectedCity");
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
