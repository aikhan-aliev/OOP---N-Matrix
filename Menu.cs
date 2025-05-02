using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_3
{
    public class Menu
    {
        private List<Nmatrix> vec = new List<Nmatrix>();
        public Menu() { }


        public void Run()
        {
            int n;
            do
            {
                PrintMenu();
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                }
                catch (System.FormatException) { n = -1; }
                switch (n)
                {
                    case 1:
                        GetElement();
                        break;
                    case 2:
                        PrintMatrix();
                        break;
                    case 3:
                        CreateMatrix();
                        break;
                    case 4:
                        Summation();
                        break;
                    case 5:
                        Multiplication();
                        break;
                }

            } while (n != 0);

        }

        static private void PrintMenu()
        {
            Console.WriteLine("\n\n 0. - Quit");
            Console.WriteLine(" 1. - Get an element");
            Console.WriteLine(" 2. - Print a matrix");
            Console.WriteLine(" 3. - Set a matrix");
            Console.WriteLine(" 4. - Add matrices");
            Console.WriteLine(" 5. - Multiply matrices");
            Console.Write(" Choose: ");
        }

        private int GetIndex()
        {
            if (vec.Count == 0) return -1;
            int n = 0;
            bool ok;
            do
            {
                Console.Write("Give a matrix index: ");
                ok = false;
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = true;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Integer is expected!");
                }
                if (n <= 0 || n > vec.Count)
                {
                    ok = false;
                    Console.WriteLine("No such matrix!");
                }
            } while (!ok);
            return n - 1;
        }

        private void GetElement()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            do
            {
                try
                {
                    Console.WriteLine("Give the index of the row: ");
                    int i = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Give the index of the column: ");
                    int j = int.Parse(Console.ReadLine()!);
                    Console.WriteLine($"a[{i},{j}]={vec[ind][i - 1, j - 1]}");
                    break;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine($"Index must be between 1 and {vec[ind].Size}");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine($"Index must be between 1 and {vec[ind].Size}");
                }
            } while (true);
        }

        private void PrintMatrix()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            int ind = GetIndex();
            Console.Write(vec[ind].ToString());
        }

        private void CreateMatrix()
        {
            int ind = vec.Count;
            bool ok = false;
            int n = -1;

            do
            {
                Console.Write("Size: ");
                try
                {
                    n = int.Parse(Console.ReadLine()!);
                    ok = n > 0;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Positive integer is expected!");
                }
            } while (!ok);
            Nmatrix N = new Nmatrix(n);
            

            ok = true;
            for (int i = 0; i < n; i++)
            {
                Console.Write("Element of First column: ");
                try
                {
                    int elem = int.Parse(Console.ReadLine()!);
                    N[i, 0] = elem;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Number is expected!");
                    ok = false;
                    break;
                }
            }
            if (n != 1)
                for (int i = 0; i < n; i++)
                {
                    Console.Write("Element of Last column: ");
                    try
                    {
                        int elem = int.Parse(Console.ReadLine()!);
                        N[i, n-1] = elem;
                    }
                    catch (System.FormatException)
                    {
                        Console.WriteLine("Number is expected!");
                        ok = false;
                        break;
                    }
                }

            for (int i = 1; i < n - 1; i++)
            {
                Console.Write("Elements of Diagonal: ");
                try
                {
                    int elem = int.Parse(Console.ReadLine()!);
                    N[i, i] = elem;
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Number is expected!");
                    ok = false;
                    break;
                }
            }

            vec.Add(N);
        }

        private void Summation()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Console.Write((vec[ind1] + vec[ind2]).ToString());
            }
            catch (Nmatrix.DifferentSizeException)
            {
                Console.WriteLine("Dimension mismatch!");
            }
        }

        private void Multiplication()
        {
            if (vec.Count == 0)
            {
                Console.WriteLine("Set a matrix first!");
                return;
            }
            Console.Write("1st matrix: ");
            int ind1 = GetIndex();
            Console.Write("2nd matrix: ");
            int ind2 = GetIndex();
            try
            {
                Console.Write((vec[ind1] * vec[ind2]).ToString());
            }
            catch (Nmatrix.DifferentSizeException)
            {
                Console.WriteLine("Dimension mismatch!");
            }
        }


    }
}
