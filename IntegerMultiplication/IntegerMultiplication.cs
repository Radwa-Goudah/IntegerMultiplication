using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>

        //Subtraction
        public static byte[] subtraction(byte[] a, byte[] b)
        {

            int max_length = Math.Max(a.Length, b.Length);
            int min_length = Math.Min(a.Length, b.Length);
            byte[] result = new byte[max_length];
            for (int i = 0; i < max_length; i++)
            {

                if (i < min_length)
                {
                    if (b[i] > a[i])
                    {
                        for (int j = i + 1; j < a.Length; j++)
                        {
                            if (a[j] == 0)
                            {
                                a[j] += 9;
                            }
                            else
                            {
                                a[i] = (byte)(a[i] + 10);
                                a[j] = (byte)(a[j] - 1);
                                result[i] = (byte)(a[i] - b[i]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        result[i] = (byte)(a[i] - b[i]);

                    }
                }
                else
                {
                    if (max_length == a.Length)
                    {
                        result[i] = a[i];

                    }
                    else
                    {
                        result[i] = b[i];
                    }
                }

            }
            return result;
        }
        //Additio
        /* public static byte[] Addtion(byte[] a, byte[] b)
         {
             int max_length = Math.Max(a.Length, b.Length);
             int min_length = Math.Min(a.Length, b.Length);
             List<byte> L = new List<byte>(max_length);
             int carry = 0, sum;
             for (int i = 0; i < max_length; i++)
             {
                 if (min_length != max_length)
                 {
                     if (i < min_length)
                     {
                         sum = a[i] + b[i] + carry;
                         L.Add((byte)(sum % 10));
                         carry = sum / 10;
                     }
                     else
                     {

                         if (max_length == a.Length)
                         {
                             if (carry != 0)
                             {
                                 sum = a[i] + carry;
                                 L.Add((byte)(sum % 10));
                                 carry = sum / 10;
                             }
                             else
                             {
                                 L.Add((byte)a[i]);
                             }
                             if (i == a.Length - 1)
                             {
                                 if (carry != 0)
                                 {
                                     L.Add((byte)(carry % 10));
                                     break;
                                 }
                             }
                         }
                         else
                         {
                             if (carry != 0)
                             {
                                 sum = b[i] + carry;
                                 L.Add((byte)(sum % 10));
                                 carry = sum / 10;
                             }
                             else
                             {
                                 L.Add((byte)b[i]);
                             }
                             if (i == b.Length - 1)
                             {
                                 if (carry != 0)
                                 {
                                     L.Add((byte)(carry % 10));
                                     break;
                                 }
                             }
                         }
                     }

                 }
                 else
                 {
                     sum = a[i] + b[i] + carry;
                     L.Add((byte)(sum % 10));
                     carry = sum / 10;
                     if (i == a.Length - 1)
                     {
                         if (carry != 0)
                         {
                             L.Add((byte)(carry % 10));
                         }
                     }
                 }

             }
             byte[] ab = L.ToArray();
             return ab;

         }*/
        /*public static byte[] Addtion(byte[] a, byte[] b)
        {
            int max_length = Math.Max(a.Length, b.Length);
            int min_length = Math.Min(a.Length, b.Length);
            if (max_length != min_length)
            {
                if (a.Length == max_length)
                {
                    b = Padding(b, Math.Abs(a.Length - b.Length));
                }
                else
                {
                    a = Padding(a, Math.Abs(a.Length - b.Length));
                }
            }
            List<byte> L = new List<byte>(max_length);
            int carry = 0, sum;
            for (int i = 0; i < max_length; i++)
            {
                sum = a[i] + b[i] + carry;
                L.Add((byte)(sum % 10));
                carry = sum / 10;
                if (i == a.Length - 1)
                {
                    if (carry != 0)
                    {
                        L.Add((byte)(carry % 10));
                    }
                }
            }
            byte[] ab = L.ToArray();
            return ab;
        }*/
        public static byte[] Addtion(byte[] a, byte[] b)
        {
            int max_length = Math.Max(a.Length, b.Length);
            int min_length = Math.Min(a.Length, b.Length);
            List<byte> L = new List<byte>(max_length);
            int carry = 0, sum;
            if (a.Length > b.Length)
            {
                for (int i = 0; i < b.Length; i++)
                {
                    sum = a[i] + b[i] + carry;
                    L.Add((byte)(sum % 10));
                    carry = sum / 10;
                }
                for (int i = b.Length; i < a.Length; i++)
                {
                    if (carry != 0)
                    {
                        sum = a[i] + carry;
                        L.Add((byte)(sum % 10));
                        carry = sum / 10;
                    }
                    else
                    {
                        L.Add((byte)a[i]);
                    }
                    if (i == a.Length - 1)
                    {
                        if (carry != 0)
                        {
                            L.Add((byte)(carry % 10));
                            break;
                        }
                    }
                }
            }
            else if (b.Length > a.Length)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    sum = a[i] + b[i] + carry;
                    L.Add((byte)(sum % 10));
                    carry = sum / 10;
                }
                for (int i = a.Length; i < b.Length; i++)
                {
                    if (carry != 0)
                    {
                        sum = b[i] + carry;
                        L.Add((byte)(sum % 10));
                        carry = sum / 10;
                    }
                    else
                    {
                        L.Add((byte)a[i]);
                    }
                    if (i == b.Length - 1)
                    {
                        if (carry != 0)
                        {
                            L.Add((byte)(carry % 10));
                            break;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < max_length; i++)
                {
                    sum = a[i] + b[i] + carry;
                    L.Add((byte)(sum % 10));
                    carry = sum / 10;
                    if (i == a.Length - 1)
                    {
                        if (carry != 0)
                        {
                            L.Add((byte)(carry % 10));
                        }
                    }
                }
            }
            byte[] ab = L.ToArray();
            return ab;

        }
        //Padding
        static public byte[] Padding(byte[] arr, int n)
        {
            byte[] result = new byte[arr.Length + n];
            for (int i = 0; i < arr.Length; i++)
            {
                result[i] = arr[i];
            }

            return result;
        }
        //Ten_Power
        public static byte[] Ten_Power(byte[] arr, int n, int N)
        {
            byte[] result = new byte[2 * N];
            for (int i = 0; i < arr.Length && (i + n) < 2 * N; i++)
            {
                result[i + n] = arr[i];
            }

            return result;
        }
        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            //throw new NotImplementedException();

            //Base case
            /*            if (N == 1)
                        {
                            byte[] Result = new byte[2];
                            Result[0] = (byte)((X[0] * Y[0]) % 10);
                            Result[1] = (byte)((X[0] * Y[0]) / 10);
                            return Result;
                        }*/
            if (N <= 64)
            {
                byte[] product = new byte[2 * N];
                int P = 0;
                int carry = 0;
                for (int i = 0; i < X.Length; i++)
                {
                    for (int j = 0; j < Y.Length; j++)
                    {
                        P = X[i] * Y[j] + product[i + j] + carry;
                        carry = P / 10;
                        P = P % 10;
                        product[i + j] = (byte)P;

                    }
                    if (carry != 0)
                    {
                        product[i + Y.Length] += (byte)carry;
                        carry = 0;
                    }

                }
                return product;
            }

            //equals
            if (X.Length > Y.Length)
            {
                Y = Padding(Y, (X.Length - Y.Length));
            }
            else
            {
                X = Padding(X, (Y.Length - X.Length));

            }
            // N must be even
            if (N % 2 != 0)
            {
                N += 1;
            }

            //Split X & Y into halves: A & B ,C & D.
            int mid_x = (N) / 2;
            int mid_y = (N) / 2;
            byte[] A = X.Take(mid_x).ToArray();
            byte[] B = X.Skip(mid_x).ToArray();
            byte[] C = Y.Take(mid_y).ToArray();
            byte[] D = Y.Skip(mid_y).ToArray();

            //conqure
            byte[] AB = Addtion(A, B);
            byte[] CD = Addtion(C, D);
            int max_size = Math.Max(CD.Length, AB.Length);
            byte[] Z = IntegerMultiply(AB, CD, max_size);
            byte[] M1 = IntegerMultiply(A, C, A.Length);//"AC"
            byte[] M2 = IntegerMultiply(B, D, B.Length);//"BD"
            byte[] First_sub = subtraction(Z, M2);
            byte[] BC_AD = subtraction(First_sub, M1);
            M2 = Ten_Power(M2, N, N);
            BC_AD = Ten_Power(BC_AD, N / 2, N);
            byte[] M2_BC_AD = Addtion(M2, BC_AD);
            byte[] RESULT = Addtion(M2_BC_AD, M1);
            return RESULT;
        }

        #endregion
    }
   
}
