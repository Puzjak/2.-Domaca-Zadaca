using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zad7_8
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }

        private static async Task LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }
        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }
        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            int a, b;
            a = await IKnowWhoKnowsThis(10);
            b = await IKnowWhoKnowsThis(5);
            return (a + b);
        }
        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSum(n);
        }

        public static async Task<int> FactorialDigitSum(int n)
        {
            return await Task.Run(() =>
            {
                int fact = 1;
                while (n > 1)
                {
                    fact *= n--;
                }
                int sum = 0;
                while (fact > 0)
                {
                    sum += fact % 10;
                    fact /= 10;
                }
                return sum;
            });
        }
        
    }
}
