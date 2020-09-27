using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp11
{
    class Test0
    {
        protected static int TestVariable = 2;
        public void MyMethod()
        {
            var res = TestVariable;
        }
    }
    class Test2 : Test0
    {
        public void MyMethod2()
        {
            var res = TestVariable;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var test = new Test();


            //var summa3Awaiter = test.Summa3().GetAwaiter();
            //summa3Awaiter.OnCompleted(() => Console.WriteLine(summa3Awaiter.GetResult()));

            var summa2Awaiter = test.Summa2().GetAwaiter();
            summa2Awaiter.OnCompleted(() => Console.WriteLine(summa2Awaiter.GetResult()));

            var summaAwaiter = test.Summa().GetAwaiter();
            summaAwaiter.OnCompleted(() => Console.WriteLine(summaAwaiter.GetResult()));

            var awaiter = test.GetAnswer().GetAwaiter();
            awaiter.OnCompleted(() => Console.WriteLine(awaiter.GetResult()));

            var summa4Awaiter = test.Summa4().GetAwaiter();
            summa4Awaiter.OnCompleted(() => Console.WriteLine(summa4Awaiter.GetResult()));

            Console.Read();
        }


    }
    public class Test
    {
        public Task<string> GetAnswer()
        {
            var tcs = new TaskCompletionSource<string>();
            var timer = new System.Timers.Timer(1) { AutoReset = false };
            timer.Elapsed += delegate { tcs.SetResult("Answer"); };
            timer.Start();
            return tcs.Task;
        }
        public Task<int> Summa()
        {
            var tcs = new TaskCompletionSource<int>();
            Task.Delay(0).ContinueWith(task =>
            {
                string test = "";

                for (int i = 0; i < 10; i++)
                {
                    test = i.ToString();
                }

                var res = test.Length;

                tcs.SetResult(res);
            });

            return tcs.Task;
        }

        public Task<int> Summa2()
        {
            var tcs = new TaskCompletionSource<int>();
            string test = "";
            new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    test = i.ToString();
                }

                var res = test.Length;

                tcs.SetResult(res);
            })
            { IsBackground = true }.Start();

            return tcs.Task;
        }

        public Task<int> Summa3()
        {

            var tcs = new TaskCompletionSource<int>();

            string test = "";

            for (int i = 0; i < 10; i++)
            {
                test = i.ToString();
            }
            var res = test.Length;

            tcs.SetResult(res);

            return tcs.Task;
        }

        public Task<string> Summa4()
        {

            var tcs = new TaskCompletionSource<string>();

            WebClient webClient = new WebClient();
            DownloadStringCompletedEventHandler handler = null;
            handler = (obj, args) => { tcs.SetResult(args.Result); };

            webClient.DownloadStringCompleted += handler;
            webClient.DownloadStringAsync(new Uri("https://docs.microsoft.com/"));

            string test = "";

            for (int i = 0; i < 10; i++)
            {
                test = i.ToString();
                Console.WriteLine(test);
            }
            var res = test.Length;
            return tcs.Task;
        }
    }
}
