namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = {
                {1, 2, 3},
                {4, 5, 6},
                {7, 8, 9}
            };
            int[,] b = {
                {5, 6},
                {7, 8}
            };
            Matrix.DisplayMatrix(Matrix.Minor(a, 0, 0));
        }
    }
}