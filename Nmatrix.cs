using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class Nmatrix
    {

        public class NegativeSizeException : Exception { };
        public class DifferentSizeException : Exception { };


        private readonly List<int> x = new();

        public Nmatrix(int k)
        {
            if (k <= 0) throw new NegativeSizeException();
            for (int i = 0; i < 3*k-2; ++i)
            {
                x.Add(0);
            }
        }

        public Nmatrix(Nmatrix N)
        {
            for (int i = 0; i < N.x.Count; ++i)
            {
                x.Add(N.x[i]);
            }
        }

        public int Size
        {
            get { return ((x.Count + 2) / 3); }
        }

        public int this[int i, int j]
        {
            get
            {
                if (i < 0 || i >= Size || j < 0 || j >= Size) throw new IndexOutOfRangeException();
                if (j == 0) return x[ind_first(i, j)];
                if (i == j && j != 0 && j != Size - 1) return x[ind_second(i, j)];
                if (j == Size - 1) return x[ind_last(i, j)];
                else return 0;
            }
            set
            {
                if (i < 0 || i >= Size || j < 0 || j >= Size) throw new IndexOutOfRangeException();
                if (j == 0) x[ind_first(i, j)] = value;
                if (i == j && j != 0 && j != Size - 1) x[ind_second(i, j)] = value;
                if (j == Size - 1) x[ind_last(i, j)] = value;
            }
        }

        private int ind_first(int i, int j)
        {
            return i;
        }
        private int ind_second(int i, int j)
        {
             return Size - 1 + j;
        }
        private int ind_last(int i, int j)
        {
            return (2 * j) + i;
        }

        public override int GetHashCode()
        {
            return (base.GetHashCode() << 2);
        }

        public override bool Equals(Object? obj)
        {
            if (obj == null || !(obj is Nmatrix))
                return false;
            else
            {
                Nmatrix? N = obj as Nmatrix;
                if (N!.Size != this.Size) return false;
                for (int i = 0; i < x.Count; i++)
                {
                    if (x[i] != N.x[i]) return false;
                }
                return true;
            }
        }


        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {
                    str += "\t" + this[i, j];
                }
                str += "\n";
            }
            return str;
        }

        public static Nmatrix operator +(Nmatrix a, Nmatrix b)
        {
            if (a.Size != b.Size) throw new DifferentSizeException();
            Nmatrix c = new Nmatrix(a.Size);
            for (int i = 0; i < 3*c.Size-2; ++i)
            {
                c.x[i] = a.x[i] + b.x[i];
            }
            return c;
        }

        public static Nmatrix operator *(Nmatrix a, Nmatrix b)
        {
            if (a.Size != b.Size) throw new DifferentSizeException();
            Nmatrix c = new(a.Size);
            int special_index = c.Size + c.Size - 2;
            if (c.Size == 1) c.x[0] = a.x[0] * b.x[0];
            else
            {
                for (int i = 0; i < c.Size; ++i)
                {
                    if (i == 0 || i == c.Size - 1)
                    {
                        c.x[i] = a.x[i] * b.x[0] + a.x[special_index + i] * b.x[c.Size - 1];
                    }
                    else
                    {
                        c.x[i] = a.x[i] * b.x[0] + a.x[special_index + i] * b.x[c.Size - 1] + a.x[c.Size - 1 + i] * b.x[i];
                    }
                }
                for (int i = 0; i < c.Size; ++i)
                {
                    if (i == 0 || i == c.Size - 1)
                    {
                        c.x[special_index + i] = a.x[i] * b.x[special_index] + a.x[special_index + i] * b.x[3 * c.Size - 3];
                    }
                    else
                    {
                        c.x[special_index + i] = a.x[i] * b.x[special_index] + a.x[special_index + i] * b.x[3 * c.Size - 3] + a.x[c.Size - 1 + i] * b.x[special_index + i];
                    }
                }
                if (c.Size >= 3)
                    for (int i = 0; i < c.Size - 2; ++i)
                    {
                        c.x[i + c.Size] = a.x[i + c.Size] * b.x[i + c.Size];
                    }
            }
            return c;
        }
    }
}
