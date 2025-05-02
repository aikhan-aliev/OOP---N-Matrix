using Assignment_3;
using System.Security.Cryptography.X509Certificates;

namespace TestNmatrix
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateNmatrix()
        {
            //Check for negative and zero size
            Assert.ThrowsException<Nmatrix.NegativeSizeException>(() => _ = new Nmatrix(0));
            Assert.ThrowsException<Nmatrix.NegativeSizeException>(() => _ = new Nmatrix(-1));

            Nmatrix N0 = new(1);

            Nmatrix N1 = new(3);
            Assert.AreEqual(N1.Size, 3);

            Nmatrix N2 = new(4);
            Assert.AreEqual(N2.Size, 4);

            Nmatrix N3 = new(5);
            Assert.AreEqual(N3.Size, 5);

            for (int i = 0; i < 5; ++i)
                for (int j = 0; j < 5; ++j)
                    Assert.AreEqual(N3[i, j], 0);

            Nmatrix N = new Nmatrix(1000);
            Assert.AreEqual(N.Size, 1000);
        }
        [TestMethod]
        public void Getting()
        {
            Nmatrix N = new(3);
            //First column
            N[0, 0] = 5;
            N[1, 0] = 5;
            N[2, 0] = 5;
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(N[i, 0], 5);
            }

            //Diagonal except [0, 0] and [2, 2]
            N[1, 1] = 4;
            Assert.AreEqual(N[1, 1], 4);

            //Last column
            N[0, 2] = 7;
            N[1, 2] = 7;
            N[2, 2] = 7;

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(N[i, 2], 7);
            }
            //Outside the Nmatrix - zero elements
            Assert.AreEqual(N[0, 1], 0);
            Assert.AreEqual(N[2, 1], 0);
        }
        [TestMethod]
        public void CheckMatrixesForEqual()
        {
            Nmatrix a = new Nmatrix(3);
            Nmatrix c = new Nmatrix(2);
            a[0, 0] = 1;
            a[1, 1] = 1;
            a[2, 2] = 1;

            a[1, 1] = 4;

            a[0, 2] = 7;
            a[1, 2] = 7;
            a[2, 2] = 7;

            Nmatrix b = new Nmatrix(a);
            c = a;
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(b.Equals(a));
            Assert.IsTrue(a.Equals(c));
            b[0, 0] = 5;
            Assert.IsFalse(a.Equals(b));
            c[0, 0] = 5;
            Assert.IsTrue(a.Equals(b));
            a = b = c;
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a.Equals(c));
            a = a;
            Assert.IsTrue(a.Equals(a));


        }
        [TestMethod]
        public void Sum()
        {
            Nmatrix a = new(3);
            Nmatrix b = new(3);
            Nmatrix zero = new(3);
            Nmatrix N = new(2);
            Nmatrix c = new(3);

            a[0, 0] = 1;
            a[1, 0] = 1;
            a[2, 0] = 1;

            a[1, 1] = 3;

            a[0, 2] = 2;
            a[1, 2] = 2;
            a[2, 2] = 2;
            //////
            b[0, 0] = 4;
            b[1, 0] = 4;
            b[2, 0] = 4;

            b[1, 1] = 5;

            b[0, 2] = 7;
            b[1, 2] = 7;
            b[2, 2] = 7;

            c = a + b;

            Assert.AreEqual(c[0, 0], 5);
            Assert.AreEqual(c[1, 0], 5);
            Assert.AreEqual(c[2, 0], 5);
            Assert.AreEqual(c[1, 1], 8);
            Assert.AreEqual(c[0, 2], 9);
            Assert.AreEqual(c[1, 2], 9);
            Assert.AreEqual(c[2, 2], 9);

            Assert.IsTrue((a + b).Equals(b + a));
            Assert.IsTrue(((a + b) + c).Equals(a + (b + c)));

            Assert.IsTrue(a.Equals(a + zero));
            Assert.IsTrue(a.Equals(zero + a));

            Assert.ThrowsException<Nmatrix.DifferentSizeException>(() => a + N);
        }

        [TestMethod]
        public void Mul()
        {
            Nmatrix a = new(3);
            Nmatrix b = new(3);
            Nmatrix c = new(3);
            Nmatrix zero = new(3);
            Nmatrix one = new(3);
            Nmatrix N = new(2);
            Nmatrix mul;

            one[0, 0] = 1;
            one[1, 1] = 1;
            one[2, 2] = 1;

            ////////
            a[0, 0] = 1;
            a[1, 0] = 1;
            a[2, 0] = 1;

            a[1, 1] = 3;

            a[0, 2] = 2;
            a[1, 2] = 2;
            a[2, 2] = 2;
            //////
            b[0, 0] = 4;
            b[1, 0] = 4;
            b[2, 0] = 4;

            b[1, 1] = 5;

            b[0, 2] = 7;
            b[1, 2] = 7;
            b[2, 2] = 7;

            ////////
            c[0, 0] = 8;
            c[1, 0] = 8;
            c[2, 0] = 8;

            c[1, 1] = 6;

            c[0, 2] = 8;
            c[1, 2] = 8;
            c[2, 2] = 8;

            mul = a * b;

            Assert.AreEqual(mul[0, 0], 12);
            Assert.AreEqual(mul[1, 0], 24);
            Assert.AreEqual(mul[2, 0], 12);
            Assert.AreEqual(mul[1, 1], 15);
            Assert.AreEqual(mul[0, 2], 21);
            Assert.AreEqual(mul[1, 2], 42);
            Assert.AreEqual(mul[2, 2], 21);


            Assert.IsTrue(zero.Equals(a * zero));
            Assert.IsTrue(a.Equals(a * one));
            Assert.IsTrue((a * (b * c)).Equals((a * b) * c)); // Associative 
            Assert.IsFalse((b * c).Equals(c * b)); // Commutative should be false
            Assert.IsTrue((a * (b + c)).Equals(a * b + a * c)); // Distributive on the left
            Assert.IsTrue(((b + c) * a).Equals(b * a + c * a));// Distributive on the right

            Assert.ThrowsException<Nmatrix.DifferentSizeException>(() => a * N);
        }
    }
}