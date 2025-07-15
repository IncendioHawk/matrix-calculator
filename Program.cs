namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] a = {
                {5, 2, 7},
                {1, 3, 4},
                {6, 0, 10}
            };
            int[,] b = {
                {5, 6},
                {7, 8}
            };
            Matrix.DisplayMatrix(a);
            Console.WriteLine(Matrix.Determinant(a));
        }
    }
}