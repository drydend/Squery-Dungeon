namespace Utils
{
    public static class Math
    {
        public static bool QuadraticSolver(float a, float b, float c, ref float x1, ref float x2)
        {
            var preRoot = b * b - 4 * a * c;

            if (preRoot < 0)
            {
                return false;
            }
            else
            {
                x1 = (-b - (float)System.Math.Sqrt(preRoot)) / (2 * a);
                x2 = (-b + (float)System.Math.Sqrt(preRoot)) / (2 * a);
                return true;
            }

        }
    }
}

