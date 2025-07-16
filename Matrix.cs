class Matrix
{
    public static double[,] CleanMatrix(double[,] a)
    {
        int rows = GetRows(a);
        int cols = GetCols(a);
        double[,] result = new double[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int integerPart = (int)a[i, j];
                double fractionalPart = a[i, j] - integerPart;
                if (fractionalPart < 1e-10 && fractionalPart > -1e-10)
                {
                    result[i, j] = integerPart;
                }
                else
                {
                    result[i, j] = a[i, j];
                }
            }
        }
        return result;
    }
    public static int GetRows(double[,] a) => a.GetLength(0);

    public static int GetCols(double[,] a) => a.GetLength(1);

    public static void DisplayMatrix(double[,] a)
    {
        int rows = GetRows(a);
        int cols = GetCols(a);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write($"{a[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    public static double[,] Add(double[,] a, double[,] b)
    {
        int rowsA = GetRows(a);
        int rowsB = GetRows(b);
        int colsA = GetCols(a);
        int colsB = GetCols(b);

        if (rowsA != rowsB || colsA != colsB)
        {
            throw new InvalidDataException("Both matrices must have the same dimensions");
        }

        double[,] result = new double[rowsA, colsA];

        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsA; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }

        return CleanMatrix(result);
    }

    public static double[,] Scale(double[,] a, double s)
    {
        int rows = GetRows(a);
        int cols = GetCols(a);
        double[,] result = new double[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = a[i, j] * s;
            }
        }

        return CleanMatrix(result);
    }

    public static double[,] Multiply(double[,] a, double[,] b)
    {
        int rowsA = GetRows(a);
        int rowsB = GetRows(b);
        int colsA = GetCols(a);
        int colsB = GetCols(b);

        if (colsA != rowsB)
        {
            throw new InvalidDataException("The number of columns of matrix a must match the number of rows of matrix b");
        }

        double[,] result = new double[rowsA, colsB];

        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsB; j++)
            {
                result[i, j] = 0;
                for (int k = 0; k < colsA; k++)
                {
                    result[i, j] += a[i, k] * b[k, j];
                }
            }
        }
        return CleanMatrix(result);
    }

    public static double[,] Transpose(double[,] a)
    {
        int rows = GetRows(a);
        int cols = GetCols(a);

        double[,] result = new double[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = a[j, i];
            }
        }

        return result;
    }

    public static int[,] Identity(int size)
    {
        int[,] result = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                result[i, j] = (i == j) ? 1 : 0;
            }
        }
        return result;
    }

    public static double[,] Minor(double[,] a, int row, int col, int? rows = null, int? cols = null)
    {
        rows ??= GetRows(a);
        cols ??= GetCols(a);

        if (rows != cols)
        {
            throw new InvalidDataException("The matrix must be square");
        }

        double[,] result = new double[(int)rows - 1, (int)cols - 1];
        int r = 0, c = 0;
        for (int i = 0; i < rows; i++)
        {
            if (i == row) continue;
            c = 0;
            for (int j = 0; j < cols; j++)
            {
                if (j == col) continue;
                result[r, c] = a[i, j];
                c++;
            }
            r++;
        }
        return CleanMatrix(result);
    }

    public static double Determinant(double[,] a, int? rows = null, int? cols = null)
    {
        rows ??= GetRows(a);
        cols ??= GetCols(a);

        if (rows != cols)
        {
            throw new InvalidDataException("The matrix must be square");
        }

        if (rows == 1)
        {
            return a[0, 0];
        }
        if (rows == 2)
        {
            return a[0, 0] * a[1, 1] - a[0, 1] * a[1, 0];
        }

        double result = 0d;

        int sign = 1;
        for (int i = 0; i < cols; i++)
        {
            result += sign * a[0, i] * Determinant(Minor(a, 0, i, rows, cols), rows - 1, cols - 1);
            sign *= -1;
        }
        return result;
    }

    public static double[,] Inverse(double[,] a)
    {
        int rows = GetRows(a);
        int cols = GetCols(a);

        if (rows != cols)
        {
            throw new InvalidDataException("The matrix must be square");
        }

        double det = Determinant(a, rows, cols);
        if (det == 0)
        {
            throw new InvalidDataException("The matrix is singular and cannot be inverted");
        }

        if (rows == 1)
        {
            return Scale(a, 1 / det);
        }
        if (rows == 2)
        {
            double[,] adj = new double[2, 2] {
                        { a[1, 1], -a[0, 1] },
                        { -a[1, 0], a[0, 0] }
                    };
            return CleanMatrix(Scale(adj, 1 / det));
        }

        double[,] adjugate = new double[rows, cols];
        int sign = 1;

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                adjugate[j, i] = sign * Determinant(Minor(a, i, j, rows, cols), rows - 1, cols - 1);
                sign *= -1;
            }
        }
        double[,] inverse = Scale(adjugate, 1 / det);
        return CleanMatrix(inverse);
    }
}