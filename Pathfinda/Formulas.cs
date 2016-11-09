using MongoModels.Models;
using System;

namespace Pathfinda
{
    public class Formulas
    {
        private static double[] _encumbranceArray = new double[] { 25, 28.75, 32.5, 37.5, 43.75, 50, 57.5, 65, 75, 87.5 };
        public static Loads GetEncumbrance(double weight, int str, Sizes size)
        {
            int maxLoad = str * 10; // already correct if str is under 10
            if (str > 10)
            {
                maxLoad = (int)(_encumbranceArray[str % 10] * Math.Pow(4, (int)(str / 10)));
            }

            if ((int)(maxLoad / 3) >= weight)
                return Loads.Light;
            else if ((int)(maxLoad / 3 * 2) >= weight)
                return Loads.Medium;
            else if (maxLoad >= weight)
                return Loads.Heavy;
            else
                return Loads.Overloaded;
        }
    }
}
