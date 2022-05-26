using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logika;

namespace Prezentacja.Model
{
    public class BallModel : INotifyPropertyChanged
    {
        private float xPosition;
        private float yPosition;
        private int radius;
        public BallModel(BallLogic ball)
        {
            ball.PropertyChanged += BallPropertyChanged;
            XPosition = ball.X;
            YPosition = ball.Y;
            Radius = ball.R;
        }
        public int Radius
        {
            get => radius;
            set
            {
                radius = value;
                RaisePropertyChanged("Radius");
            }
        }
        public float XPosition
        {
            get => xPosition;
            set
            {
                xPosition = value;
                RaisePropertyChanged("XPosition");
            }
        }
        public float YPosition
        {
            get => yPosition;
            set
            {
                yPosition = value;
                RaisePropertyChanged("YPosition");
            }
        }
        public float CenterTransform { get => -1 * Radius; }
        public int Diameter { get => 2 * Radius; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void BallPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            BallLogic b = (BallLogic)sender;

            XPosition = b.X;
            YPosition = b.Y;
            RaisePropertyChanged("XPosition");
            RaisePropertyChanged("YPosition");
        }
    }
}