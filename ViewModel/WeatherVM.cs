﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WeatherApp.Model;
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

        //Ctor
        public WeatherVM()
        {
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
                            Value = 21
                        }
                    }
                };
            }
            
        }

        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        //Trigger the prop changed event
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
