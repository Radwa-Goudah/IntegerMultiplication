using Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Problem
{

    public class Problem : ProblemBase, IProblem
    {
        #region ProblemBase Methods
        public override string ProblemName { get { return "IntegerMultiplication"; } }

        public override void TryMyCode()
        {
            int N = 0;
            byte[] output;

            N = 2;
            byte[] X1 = { 1, 1 };
            byte[] Y1 = { 2, 2 };
            byte[] expected = { 2, 4, 2, 0 };
            output = IntegerMultiplication.IntegerMultiply(X1, Y1, N);
            PrintCase(N, X1, Y1, output, expected);

            N = 4;
            byte[] X2 = { 1, 2, 3, 4 }; 
            byte[] Y2 = { 0, 0, 0, 0 };
            byte[] expected2 = { 0, 0, 0, 0, 0, 0, 0, 0 };
            output = IntegerMultiplication.IntegerMultiply(X2, Y2, N);
            PrintCase(N, X2, Y2, output, expected2);

            N = 4;
            byte[] X21 = { 9, 9, 9, 9 };
            byte[] Y21 = { 9, 9, 9, 9 };
            byte[] expected21 = { 1, 0, 0, 0, 8, 9, 9, 9 };
            output = IntegerMultiplication.IntegerMultiply(X21, Y21, N);
            PrintCase(N, X21, Y21, output, expected21);

            N = 8;
            byte[] X3 = { 0, 0, 0, 0, 1, 2, 3, 4 };
            byte[] Y3 = { 0, 0, 0, 0, 1, 0, 0, 0 };
            byte[] expected3 = { 0, 0, 0, 0, 0, 0, 0, 0, 1, 2, 3, 4, 0, 0, 0, 0};
            output = IntegerMultiplication.IntegerMultiply(X3, Y3, N);
            PrintCase(N, X3, Y3, output, expected3);

            N = 8;
            byte[] X4 = { 2, 7, 0, 1, 3, 1, 0, 0 };
            byte[] Y4 = { 2, 7, 0, 1, 3, 1, 0, 0 };
            byte[] expected4 = { 4, 8, 1, 9, 6, 8, 9, 7, 1, 7, 1, 0, 0, 0, 0, 0 };
            output = IntegerMultiplication.IntegerMultiply(X4, Y4, N);
            PrintCase(N, X4, Y4, output, expected4);

            N = 16;
            byte[] X5 = { 4, 8, 1, 9, 6, 8, 9, 7, 1, 7, 1, 0, 0, 0, 0, 0 };
            byte[] Y5 = { 4, 8, 1, 9, 6, 8, 9, 7, 1, 7, 1, 0, 0, 0, 0, 0 };
            byte[] e5 = { 6, 5, 8, 5, 2, 8, 2, 5, 3, 9, 7, 1, 5, 0, 9, 7, 4, 1, 5, 9, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            output = IntegerMultiplication.IntegerMultiply(X5, Y5, N);
            PrintCase(N, X5, Y5, output, e5);
        }

 
        Thread tstCaseThr;
        bool caseTimedOut ;
        bool caseException;

        protected override void RunOnSpecificFile(string fileName, HardniessLevel level, int timeOutInMillisec)
        {
            int testCases;
            int N = 0;
            byte[] output = null;
            byte[] actualResult = null;
            int j=0;

            Stream s = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(s);

            testCases = br.ReadInt32();

            int totalCases = testCases;
            int correctCases = 0;
            int wrongCases = 0;
            int timeLimitCases = 0;

            int i = 1;
            while (testCases-- > 0)
            {
                N = br.ReadInt32();
                byte[] X = new byte[N];
                byte[] Y = new byte[N];
                actualResult = new byte[2*N];

                for (j = 0; j < N; j++)
                {
                    X[j] = br.ReadByte();
                }
                for (j = 0; j < N; j++)
                {
                    Y[j] = br.ReadByte();
                }
                for (j = 0; j < 2*N; j++)
                {
                    actualResult[j] = br.ReadByte();
                }
                caseTimedOut = true;
                caseException = false;
                {
                    tstCaseThr = new Thread(() =>
                    {
                        try
                        {
                            Stopwatch sw = Stopwatch.StartNew();
                            output = IntegerMultiplication.IntegerMultiply(X, Y, N);
                            sw.Stop();
                            Console.WriteLine("N = {0}, time in ms = {1}", N, sw.ElapsedMilliseconds);
                        }
                        catch
                        {
                            caseException = true;
                            output = null;
                        }
                        caseTimedOut = false;
                    });

                    //StartTimer(timeOutInMillisec);
                    tstCaseThr.Start();
                    tstCaseThr.Join(timeOutInMillisec);
                }
                                
                if (caseTimedOut)       //Timedout
                {
                    Console.WriteLine("Time Limit Exceeded in Case {0}.", i);
                    tstCaseThr.Abort();
                    timeLimitCases++;
                }
                else if (caseException) //Exception 
                {
                    Console.WriteLine("Exception in Case {0}.", i);
                    wrongCases++;
                }
                else if (output != null && CheckOutput(output, actualResult))    //Passed
                {
                    Console.WriteLine("Test Case {0} Passed!", i);
                    correctCases++;
                }
                else                    //WrongAnswer
                {
                    Console.WriteLine("Wrong Answer in Case {0}.", i);
                    if (level == HardniessLevel.Easy)
                    {
                        if (output != null)
                        {
                            PrintCase(N, X, Y, output, actualResult);
                        }
                        else
                        {
                            Console.WriteLine("Exception is occur");
                        }
                    }
                    wrongCases++;
                }

                i++;
            }
            s.Close();
            br.Close();
            Console.WriteLine();
            Console.WriteLine("# correct = {0}", correctCases);
            Console.WriteLine("# time limit = {0}", timeLimitCases);
            Console.WriteLine("# wrong = {0}", wrongCases);
            Console.WriteLine("\nFINAL EVALUATION (%) = {0}", Math.Round((float)correctCases / totalCases * 100, 0)); 
        }

       

        protected override void OnTimeOut(DateTime signalTime)
        {
        }

        public override void GenerateTestCases(HardniessLevel level, int numOfCases)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Helper Methods
        private static void PrintNum(byte[] X)
        {

            int N = X.Length;

            for (int i = N - 1; i >= 0; i--)
            {
                    Console.Write(X[i]);
            }
            Console.WriteLine();
        }

        
        private void PrintCase(int N, byte[] X, byte[] Y, byte[] output, byte[] expected)
        {
            Console.WriteLine("N = {0}", N);
            Console.Write("X = "); PrintNum(X);
            Console.Write("Y = "); PrintNum(Y);
            Console.Write("output = "); if (output != null) PrintNum(output); else Console.WriteLine("NULL");
            Console.Write("expect = "); PrintNum(expected);
            if (output != null)
            {
                if (CheckOutput(output, expected)) Console.WriteLine("CORRECT");
                else Console.WriteLine("WRONG");
            }
            Console.WriteLine();
        }

        private bool CheckOutput(byte[] output, byte[] actualResult)
        {
            int N = output.Length;
            for (int idx = 0; idx < N; idx++)
            {
                if (output[idx] != actualResult[idx]) return false;
            }
            
            return true;
        }
        #endregion
   
    }
}
