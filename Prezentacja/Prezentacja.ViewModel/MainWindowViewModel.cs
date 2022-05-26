using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Prezentacja.Model;

namespace Prezentacja.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private IList _balls;
        private int _number;
        private int _height;
        private int _width;
        private string _text;
        private ModelAbstractApi model { get; set; }

        public MainWindowViewModel()
        {
            model = ModelAbstractApi.CreateAPI();
            StartClick = new RelayCommand(() => CreateBalls());
            StopClick = new RelayCommand(() => StopBalls());
            _height = model.Height + 4;
            _width = model.Width + 4;
            _balls = model.CreateBalls(_number);
        }
        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                RaisePropertyChanged("Width");
            }
        }
        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                RaisePropertyChanged("Height");
            }
        }
       public IList Balls
        {
            get => _balls;
        }
        public string NumberOfBalls
        {
            get { return _text; }
            set
            {
                _text = value;
                try
                {
                    int val = System.Int32.Parse(_text);
                    if (val > 0 && val <= 20)
                    {
                        _number = val;
                    }
                    else
                    {
                        _number = 0;
                    }
                    RaisePropertyChanged(nameof(Number));

                }
                catch (System.FormatException)
                {
                    Trace.WriteLine("Text() z viewModel rzucil wyjatek Format");
                    _number = 0;
                    RaisePropertyChanged(nameof(Number));
                }
                catch (System.OverflowException)
                {
                    Trace.WriteLine("Text() z viewModel rzucil wyjatek Overflow");
                    _number = 0;
                    RaisePropertyChanged(nameof(Number));
                }
            }
        }
        public ICommand StartClick { get; set; }
        public ICommand StopClick { get; set; }

        private void CreateBalls()
        {
            model.CreateBalls( _number);
        }      
        private void StopBalls()
        {
            model.StopBalls();
        }
        public int Number
        {
            get
            {
                return _number;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {

            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}