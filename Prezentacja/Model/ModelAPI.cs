using Logika;
using System.Collections.Generic;

namespace Model
{
    public abstract class ModelAPI
    {
        public abstract List<BallInModel> Balls { get; }
        public abstract void AddBallsAndStart(int ballsAmount);

        public static ModelAPI CreateApi()
        {
            return new ModelBall();
        }

        internal class ModelBall : ModelAPI
        {
            private Board board;
            public override List<BallInModel> Balls => ChangeBallToBallInModel();

            public ModelBall()
            {
                this.board = new Board();
            }

            public override void AddBallsAndStart(int ballsAmount)
            {
                for (int i = 0; i < ballsAmount; i++)
                    board.AddBall();

                board.RunSimulation();
            }

            public List<BallInModel> ChangeBallToBallInModel()
            {
                List<BallInModel> result = new List<BallInModel>();

                foreach (Ball ball in board.balls)
                {
                    result.Add(new BallInModel(ball));
                }
                return result;
            }
        }

    }
}