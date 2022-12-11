namespace Game
{
    namespace Play
    {
        public interface IStatistic
        {
            public MovementPlayer[] GetPlayers { get; }

            public void SetStatistics(MovementPlayer[] finishedPlayers);
        }
    }
}

