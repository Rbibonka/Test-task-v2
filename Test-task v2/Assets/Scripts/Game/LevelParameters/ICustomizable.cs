namespace Game
{
    namespace Play
    {
        public interface ICustomizable
        {
            public int GetPenaltyNumberPlatforms { get; }

            public int GetPlayersQuantity { get; }

            public int GetPlatformQuantity { get; }

            public int GetQuantitySpecialPlatforms { get; }

            public void ChangeQuantityPalyers(int value);

            public void ChangeQuantitySpecialPlatforms(int value);
        }
    }
}