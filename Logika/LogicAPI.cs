using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logika
{
    public abstract class LogicAPI
    {
        public abstract void AddBall();
        public abstract void RemoveBall();
        public abstract void RunSimulation();
        public abstract int GetBallCount();
    }
}
