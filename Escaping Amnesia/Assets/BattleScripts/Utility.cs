using System.Collections.Generic;

namespace BattleCards
{
    public static class Utility
    {
        // our randomizer for a list, we can always change the random algorithm
        public static void Shuffle<T>(List<T> list)
        {

            System.Random random = new System.Random();
            int n = list.Count;

            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (list[j], list[i]) = (list[i], list[j]);
            }
        }
    }
}
