using System.ComponentModel;
using System.Numerics;
using System.Text.Json.Serialization;

namespace Dane
{
   public class Ball : INotifyPropertyChanged
    {
        private Vector2 _vectorCurrent;
        private int _radius;
        private float _mass;
        private Vector2 _velocity;
     
        public Ball()
        {
        }

        public Ball(Vector2 vector)
        {
            _vectorCurrent = vector;
            _radius = 15;
        }

        public Ball(float x, float y, int radius, float mass, Vector2 velocity)
        {
            _vectorCurrent.X = x;
            _vectorCurrent.Y = y;
            _radius = radius;
            _mass = mass;
            _velocity = velocity;
        }

        public void UpdatePosition()
        {
                _vectorCurrent += _velocity;
                RaisePropertyChanged("VectorCurrent");
        }

        [JsonIgnore]
        public Vector2 VectorCurrent
        {
            get => _vectorCurrent;
            set => _vectorCurrent = value; 
        }

        [JsonIgnore]
        public Vector2 Velocity
        {
            get => _velocity;
            set => _velocity = value;
        }

        public float X
        {
            get => _vectorCurrent.X;
            set
            {
                _vectorCurrent.X = value;
                RaisePropertyChanged("X"); 
            }
        }

        public float Y
        {
            get => _vectorCurrent.Y;
            set
            {
                _vectorCurrent.Y = value;
                RaisePropertyChanged("Y");
            }
        }

        public int Radius
        {
            get => _radius;
        }

        public float Mass
        {
            get => _mass;
            set
            {
                _mass = value;
            }
        }

        public float VX
        {
            get => _velocity.X;
            set
            {
                if (value > 5)
                    value = 5;
                else if (value < -5)
                    value = -5;
                _velocity.X = value;
            }
        }

        public float VY
        {
            get => _velocity.Y;
            set
            {
                if (value > 5)
                    value = 5;
                else if (value < -5)
                    value = -5;
                _velocity.Y = value;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    } 
}