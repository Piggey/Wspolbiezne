
using System;
using System.ComponentModel;
using Dane;

namespace Logika
{
    public class BallLogic : INotifyPropertyChanged
    {
        private readonly Ball _ball;
        public BallLogic(Ball ball)
        {
            _ball = ball;
            ball.PropertyChanged += DataBallChanged;
        }

        public void DataBallChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("VectorCurrent");
        }

        public float X
        {
            get => _ball.X;
            set
            {
                _ball.X = value;
                RaisePropertyChanged(nameof(X));
            }
        }
        public float Y
        {
            get => _ball.Y;
            set
            {
                _ball.Y = value;
                RaisePropertyChanged(nameof(Y));
            }
        }
        public int R
        {
            get => _ball.Radius;
        }

        public float VX
        {
            get => _ball.VX;
            set
            {
                _ball.VX = value;
            }
        }

        public float VY
        {
            get => _ball.VY;
            set
            {
                _ball.VY = value;
            }
        }
        public Ball Ball { get => _ball; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}