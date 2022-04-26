

namespace Logika
{
    public class Ball
    {
        private const double SPEED_MIN = 0.2;
        private const double SPEED_MAX = 1;

        public double x { get; set; }
        public double y { get; set; }

        public double xSpeed { get; set; }
        public double ySpeed { get; set; }

        public Ball()
        {
            Random r = new Random();

            this.x = r.NextDouble() * Board.WIDTH;
            this.y = r.NextDouble() * Board.HEIGHT;

            this.xSpeed = r.NextDouble() * (SPEED_MAX - SPEED_MIN) + SPEED_MIN;
            this.ySpeed = r.NextDouble() * (SPEED_MAX - SPEED_MIN) + SPEED_MIN;
        }

        public void Move()
        {
            x += xSpeed;
            y += ySpeed;

            if (x >= Board.WIDTH || x <= 0)
                xSpeed = -xSpeed;

            if (y >= Board.HEIGHT || y <= 0)
                ySpeed = -ySpeed;
        }
    }
}
