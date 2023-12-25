using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppWPF.ViewModel
{
    public class WeatherVM : INotifyPropertyChanged
    {
        //które właściwości chciałbym obserwowac?? -> propfull snippet
        //chciałbym obserwować textboxa do którego będzie wpisywało się miasto
        private string query;

        public string Query
        {
            get { return query; }
            set 
            {
                //ogrywam getterem i setterem pole query, ale przy setterze daje znać, że właściwość obserwowana się zmieniła
               query = value;
               OnPropertyChanged("Query");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        //konwencja jest taka, że tworzy się taką metodę
        //będzie ona przyjmowała nazwę właściwości (tej klasy) jako argument
        private void OnPropertyChanged(string propertyName)
        {
            //gdy obserwowana właściwość się zmieniła invokuje się to
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
