using System;
using System.Collections.Generic;
using System.Linq;


//Варіант 3. Нехай дано пошукову систему
//яка приймає пошуковий запит користувача
//і надає йому відповідь – список URL
//сайтів(реалізувати як заглушку – на будь-який
// запит повертає однаковий список).
//Доповнити систему підсистемою логування,
//яка зберігає час і пошуковий запит непомітно
//для користувача в масиві.
//Також передбачити виводу ТОП N запитів, які
//отримувала система найчастіше.

namespace ConsoleApp2
{


    class Program
    {
        static void Main(string[] args)
        {
            RealSubject sub = new RealSubject();
            Proxy log1 = new Proxy(sub);
            Console.WriteLine(sub.Request(DateTime.Now, "Putin Died"));
            Console.WriteLine(log1.Request(DateTime.Now, "Andrashko"));
            Console.WriteLine(log1.Request(DateTime.Now, "Pivo"));
            Console.WriteLine(log1.Request(DateTime.Now, "Andrashko"));
            log1.Request(DateTime.Now, "Pivo");
            log1.Request(DateTime.Now, "Pivo");
            log1.Request(DateTime.Now, "Putin Died");
            log1.Request(DateTime.Now, "Pivo");
            log1.Request(DateTime.Now, "P#hub");
            Console.WriteLine(log1.ShowTopRequests(3));



        }
    }

    interface ISubject
    {
        string Request(DateTime date, string request);
    }
    class RealSubject : ISubject
    {
        public RealSubject()
        {
        }
        public string Request(DateTime date, string request = "")
        {
            return $"Date: {date}, Req: {request}, result = //Google //Facebook";
        }
    }
    class Proxy : ISubject
    {
        private List<string[]> requestArr = new List<string[]>();
        private RealSubject sub;
        public Proxy(RealSubject sub)
        {
            this.sub = sub;
        }


        public string Request(DateTime date, string request = "")
        {
            this.requestArr.Add(new string[] { date.ToString(), request});
            return sub.Request(date,request);
        }
        public string ShowTopRequests(int n)
        {

            Dictionary<string, int> requestsCount = new Dictionary<string, int>();
            string str = "";

            foreach (string[] el1 in this.requestArr)
            {
                bool check = true;
                int counter = 0;
                foreach (string[] el2 in this.requestArr)
                {
                    if (el1[1] == el2[1])
                    {
                        counter++;
                    }
                }
                if (!requestsCount.ContainsKey(el1[1]))
                {
                    requestsCount.Add(el1[1], counter);
                }
            }

            requestsCount = requestsCount.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            int i = 0;
            foreach (var item in requestsCount)
            {
                str += $"#{i + 1} Req count: " + $"{item.Value}" + "; Req name: " + $"{item.Key}" + "\n";
                i++;
                if (n == i)
                {
                    break;
                }
            }

            return str;

        }

    }
}