namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            double[,] a = {
                {5, 2, 7},
                {1, 3, 4},
                {6, 0, 10}
            };
            double[,] b = {
                {5, 6},
                {7, 8}
            };
            Matrix.DisplayMatrix(Matrix.Multiply(Matrix.Inverse(a), a));
        }
    }
}