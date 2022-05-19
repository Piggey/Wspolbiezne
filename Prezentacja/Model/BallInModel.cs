using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Prezentacja.Model
{
    public class BallInModel : IBall
    {
        public float Radius { get; }
        public event PropertyChangedEventHandler? PropertyChanged;

        public BallInModel(float top, float left, float radius)
        {
            Top = top;
            Left = left;
            Radius = radius;
        } 
        
        private float _top;
        public float Top
        {
            get => _top;
            private set 
            {
                _top = value;
                RaisePropertyChanged();
            }
        }

        private float _left;
        public float Left
        {
            get => _left;
            private set
            {
                _left = value;
                RaisePropertyChanged();
            }
        }
        
        public void Move(float positionX, float positionY)
        {
            Left = positionX;
            Top = positionY;
        }

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
    }
}