using System;

namespace Prezentacja.Model
{
    public class ModelBallChangedEventArgs : EventArgs
    {
        public IBall Ball { get; init; } = null!;
    }
}