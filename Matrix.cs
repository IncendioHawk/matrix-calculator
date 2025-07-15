class Matrix
{
    public static int GetRows(int[,] a)
    {
        return a.GetLength(0);
    }

    public static int GetCols(int[,] a)
    {
        return a.GetLength(1);
    }

    public static void DisplayMatrix(int[,] a)
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

    public static int[,] Add(int[,] a, int[,] b)
    {
        int rowsA = GetRows(a);
        int rowsB = GetRows(b);
        int colsA = GetCols(a);
        int colsB = GetCols(b);

        if (rowsA != rowsB || colsA != colsB)
        {
            throw new InvalidDataException("Both matrices must have the same dimensions");
        }

        int[,] result = new int[rowsA, colsA];

        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < colsA; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }

        return result;
    }

    public static int[,] Scale(int[,] a, int s)
    {
        int rows = GetRows(a);
        int cols = GetCols(a);
        int[,] result = new int[rows, cols];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[i, j] = a[i, j] * s;
            }
        }

        return result;
    }

    public static int[,] Multiply(int[,] a, int[,] b)
    {
        int rowsA = GetRows(a);
        int rowsB = GetRows(b);
        int colsA = GetCols(a);
        int colsB = GetCols(b);

        if (colsA != rowsB)
        {
            throw new InvalidDataException("The number of columns of matrix a must match the number of rows of matrix b");
        }

        int[,] result = new int[rowsA, colsB];

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

        return result;
    }

    public static int[,] Transpose(int[,] a)
    {
        int rows = GetRows(a);
        int cols = GetCols(a);

        int[,] result = new int[cols, rows];

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

    public static int[,] Minor(int[,] a, int row, int col, int? rows = null, int? cols = null)
    {
        rows ??= GetRows(a);
        cols ??= GetCols(a);

        if (rows != cols)
        {
            throw new InvalidDataException("The matrix must be square");
        }

        int[,] result = new int[(int) rows - 1, (int) cols - 1];
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
        return result;
    }

    public static int Determinant(int[,] a, int? rows = null, int? cols = null)
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

        int result = 0;
        
        int sign = 1;
        for (int i = 0; i < cols; i++)
        {
            result += sign * a[0, i] * Determinant(Minor(a, 0, i, rows, cols), rows - 1, cols - 1);
            sign *= -1;
        }

        return result;
    }
}