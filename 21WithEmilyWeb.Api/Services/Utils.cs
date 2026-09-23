namespace _21WithEmilyWeb.Api.Services
{
    public class Utils
    {
        public const int GOAL = 21;

        public static int[]? ValidCounts(int score)
        {
            int[]? countsArray;
            var countsSet = new SortedSet<int>();
            for (var i = 1; i <= 3; i++)
            {
                var count = score + i;
                if (count > GOAL)
                    break;
                countsSet.Add(count);
            }
            if (countsSet.Count > 0)
                countsArray = countsSet.ToArray();
            else
                countsArray = null;
            return countsArray;
        }
    }
}
