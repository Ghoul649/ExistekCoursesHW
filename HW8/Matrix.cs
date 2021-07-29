using HWCommon.Commands.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HW8
{
    
    public class Matrix<T>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        private T[,] matrix;
        public T this[int x, int y]
        {
            set
            {
                matrix[x, y] = value;
            }
            get
            {
                return matrix[x, y];
            }
        }
        public Matrix(int width, int height) 
        {
            Width = width;
            Height = height;
            matrix = new T[width, height];
        }
        public Matrix(int width, int height, string value, char splitter = ',') : this(width,height)
        {
            if (!Converter._converters.ContainsKey(typeof(T)))
                throw new Exception($"There is no converter to type \"{typeof(T)}\"");
            var converter = Converter._converters[typeof(T)];
            var values = value.Split(splitter).GetEnumerator();
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    if (!values.MoveNext())
                        throw new IndexOutOfRangeException();
                    else
                        this[x, y] = (T)converter((values.Current as string).Trim());
        }
        public static Matrix<T> Multiply(Matrix<T> A, Matrix<T> B, Func<T,T,T> mFunc, Func<T,T,T> aFunc) 
        {
            if (A.Width != B.Height)
                throw new Exception("Matrices are not compatible");
            Matrix<T> result = new Matrix<T>(B.Width, A.Height);
            for (int x = 0; x < B.Width; x++)
                for(int y = 0; y < A.Height; y++)
                    result[x, y] = multiplyRowCol(x, y, A, B, mFunc, aFunc);
            return result;
        }
        public static Matrix<T> ParalelMultiply(Matrix<T> A, Matrix<T> B, Func<T, T, T> mFunc, Func<T, T, T> aFunc)
        {
            if (A.Width != B.Height)
                throw new Exception("Matrices are not compatible");
            Matrix<T> result = new Matrix<T>(B.Width, A.Height);
            for (int x = 0; x < B.Width; x++)
                for (int y = 0; y < A.Height; y++)
                    result[x, y] = multiplyRowCol(x, y, A, B, mFunc, aFunc);
            return result;
        }
        private static T multiplyRowCol(int x, int y, Matrix<T> A, Matrix<T> B, Func<T, T, T> mFunc, Func<T, T, T> aFunc) 
        {
            T result = default;
            for (int i = 0; i < A.Width; i++) 
                result = aFunc(result, mFunc(A[i, y], B[x, i]));
            return result;
        }
        public override string ToString() 
        {
            StringBuilder sb = new StringBuilder();
            for (int x = 0; x < Width; x++) 
            {
                for (int y = 0; y < Height; y++) 
                {
                    sb.Append(matrix[x, y]);
                    if (x + 1 != Width || y + 1 != Height)
                        sb.Append(", ");
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}
