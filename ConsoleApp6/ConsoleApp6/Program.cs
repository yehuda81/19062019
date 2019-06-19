using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        
        static void Main(string[] args)
        {
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            Task<int> t = Task.Run<int>(() =>
            {
                return AddNumber(tokenSource);
            }).ContinueWith<int>((Task<int> papa) =>
            {
                Console.WriteLine(papa.Result);
                return papa.Result;
            });
            Console.ReadLine();
            tokenSource.Cancel();
            //Console.WriteLine(t.Result);
        }
       
        private static int AddNumber(CancellationTokenSource tokenSource)
        {
            int sum = 0;
            int i = 1;
            while (i < 5 && !tokenSource.IsCancellationRequested)
            {
                Console.WriteLine(i);
                Thread.Sleep(500);
                sum = sum + i;
                i++;
            }
            tokenSource.Token.ThrowIfCancellationRequested();
            return sum;
        }
       
    }
}
