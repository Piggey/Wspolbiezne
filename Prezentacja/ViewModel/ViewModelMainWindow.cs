using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using Prezentacja.Model;

namespace Prezentacja.ViewModel
{
    public class ViewModelMainWindow : INotifyPropertyChanged
    {
        private readonly ModelApi _modelApi;
        public ObservableCollection<IBall> Balls { get; }
        public ICommand StartButtonClick { get; }
        public event PropertyChangedEventHandler? PropertyChanged;

        private bool _state;
        public bool State
        {
            get => _state;
            set
            {
                _state = value;
                RaisePropertyChanged(nameof(State));
            }
        }
        
        private string _inputText;
        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                RaisePropertyChanged(nameof(InputText));
            }
        }
        
        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set 
            { 
                _errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }


        public ViewModelMainWindow() : this(ModelApi.CreateModelApi()) { }

        private ViewModelMainWindow(ModelApi baseModel)
        {  
            State = true;
            _modelApi = baseModel;
            StartButtonClick = new RelayCommand(StartButtonClickHandler);
            Balls = new ObservableCollection<IBall>();
            IDisposable observer = _modelApi.Subscribe(x => Balls.Add(x));
        }
        private void StartButtonClickHandler()
        {
            _modelApi.AddBallsAndStart(ReadFromTextBox());
        }

        private int ReadFromTextBox()
        {
            if (Int32.TryParse(InputText, out var number) && InputText != "0")
            {
                number = Int32.Parse(InputText);
                ErrorMessage = "";
                State = false;
                if (number > 10)
                {
                    return 10;
                }
                return number;
            }
            ErrorMessage = "Nieprawidłowa liczba";
            return 0;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}