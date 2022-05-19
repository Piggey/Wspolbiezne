using System.ComponentModel;
using System.Numerics;

namespace Prezentacja.Model
{
    public interface IBall : INotifyPropertyChanged
    {
        float Top { get; }
        float Left { get; }
        float Radius { get; }
    }
}