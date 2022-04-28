using Logika;

namespace Model
{

    public class BallInModel
    {
        private const int VIEW_WIDTH = 854;
        private const int VIEW_HEIGHT = 480;
        
        private Ball ball;

        public BallInModel(Ball ball)
        {
            this.ball = ball;
        }

        public double PositionX => (ball.x * VIEW_WIDTH) / Board.WIDTH;

        public double PositionY => (ball.y * VIEW_HEIGHT) / Board.HEIGHT;

    }
}
