using Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading;
using System;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ViewModelMainWindow : INotifyPropertyChanged
    {
        private ModelAPI modelApi;
        // private readonly double scale = 5.35;
        public ObservableCollection<BallInModel> Balls { get; set; }
        public ICommand StartButtonClick { get; set; }
        private string inputText;
        private Task? task;

        private bool state;

        public bool State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
                RaisePropertyChanged(nameof(State));
            }
        }


        public string InputText
        {
            get
            {
                return inputText;
            }
            set
            {
                inputText = value;
                RaisePropertyChanged(nameof(InputText));
            }
        }

        private string errorMessage;

        public string ErrorMessage
        {
            get 
            { 
                return errorMessage; 
            }
            set 
            { 
                errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ViewModelMainWindow() : this(ModelAPI.CreateApi())
        {

        }

        public ViewModelMainWindow(ModelAPI baseModel)
        {
            State = true;
            this.modelApi = baseModel;
            StartButtonClick = new RelayCommand(() => StartButtonClickHandler());
            Balls = new ObservableCollection<BallInModel>();
        }

        private void StartButtonClickHandler()
        {
            modelApi.AddBallsAndStart(readFromTextBox());
            task = new Task(UpdatePosition);
            task.Start();
        }

        public void UpdatePosition()
        {
            while(true)
            {
                ObservableCollection<BallInModel> treadList = new ObservableCollection<BallInModel>();

                foreach (BallInModel ball in modelApi.Balls)
                {
                    treadList.Add(ball);
                }

                Balls = treadList;
                RaisePropertyChanged(nameof(Balls));
                Thread.Sleep(10); 
            }
        }

        public int readFromTextBox()
        {
            int number;
            if (Int32.TryParse(InputText, out number) && InputText != "0")
            {
                number = Int32.Parse(InputText);
                ErrorMessage = "";
                State = false;
                if (number > 100)
                {
                    return 100;
                }
                return number;
            }
            ErrorMessage = "Nieprawidłowa liczba";
            return 0;
        }
             
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}