using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj2
{

    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _textOne;

        public string TextOne
        {
            get { return _textOne; }
            set
            {
                _textOne = value;
                NotifyPropertyOnChange(nameof(TextOne));
            }
        }

        private string _textTwo;

        public string TextTwo
        {
            get { return _textTwo; }
            set
            {
                _textTwo = value;
                NotifyPropertyOnChange(nameof(TextTwo));
            }
        }

        private string _textThree;

        public string TextThree
        {
            get { return _textThree; }
            set
            {
                _textThree = value;
                NotifyPropertyOnChange(nameof(TextThree));
            }
        }

        private string _textFour;

        public string TextFour
        {
            get { return _textFour; }
            set
            {
                _textFour = value;
                NotifyPropertyOnChange(nameof(TextFour));
            }
        }

        private string _textFive;

        public string TextFive
        {
            get { return _textFive; }
            set
            {
                _textFive = value;
                NotifyPropertyOnChange(nameof(TextFive));
            }
        }

        public void NotifyPropertyOnChange(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

