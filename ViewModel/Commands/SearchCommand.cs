using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        //Instance of VM
        public WeatherVM VM { get; set; }

        public event EventHandler CanExecuteChanged;

        public SearchCommand(WeatherVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }
    }
}
