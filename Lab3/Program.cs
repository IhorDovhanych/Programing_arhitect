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
            Proxy log1 = new Proxy(DateTime.Now, "Andrashko");
            RealSubject sub = new RealSubject(DateTime.Now, "Putin Died");


            log1.Request(DateTime.Now, "Andrashko");
            log1.Request(DateTime.Now, "Pivo");
            log1.Request(sub);
            log1.Request(DateTime.Now, "Andrashko");
            log1.Request(DateTime.Now, "Pivo");
            log1.Request(DateTime.Now, "Pivo");
            log1.Request(DateTime.Now, "Putin Died");
            log1.Request(DateTime.Now, "Pivo");
            log1.Request(DateTime.Now, "P#hub");
            log1.Request(sub);
            log1.Request(sub);
            log1.Request(sub);

            Console.WriteLine(log1.ShowTopRequests(3));



        }
    }

    interface ISubject
    {
        string Request(DateTime date, string request);
        string Request();
    }
    class RealSubject : ISubject
    {
        public DateTime date;
        public string req;
        public RealSubject(DateTime date, string request = "")
        {
            this.date = date;
            this.req = request;
        }
        public string Request(DateTime date , string request = "")
        {
            return $"Date: {date}, Req: {request}, result = //Google //Facebook";
        }
        public string Request() {
            return $"Date: {this.date}, Req: {this.req}, result = //Google //Facebook";
        }
    }
    class Proxy: ISubject
    {
        private List<string[]> requestArr = new List<string[]>();
        public Proxy(DateTime date, string request = "")
        {
            
            this.requestArr.Add(new string[] { date.ToString(), request });
        }
        public Proxy(RealSubject sub)
        {
            this.requestArr.Add(new string[] { sub.date.ToString(), sub.req });
        }


        public string Request(DateTime date, string request = "")
        {
            this.requestArr.Add(new string[] { $"{date}", request });
            return $"Date: {date}, Req: {request}, result = //Google //Facebook";
        }
        public string Request()
        {
            return $"Date: {this.requestArr[this.requestArr.Count-1][0]}," +
                $" Req: {this.requestArr[this.requestArr.Count - 1][1]}, result = //Google //Facebook";
        }
        public string Request(RealSubject sub)
        {
            this.requestArr.Add(new string[] { sub.date.ToString(), sub.req });
            return $"Date: {sub.date}," +
                $" Req: {sub.req}, result = //Google //Facebook";
        }
        public string ShowTopRequests(int n)
        {

            Dictionary<string, int> requestsCount = new Dictionary<string, int>();
            string str = "";

             foreach(string[] el1 in this.requestArr)
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
            foreach(var item in requestsCount) { 
                str += $"#{i + 1} Req count: " + $"{item.Value}" + "; Req name: " + $"{item.Key}"+"\n";
                i++;
                if( n == i)
                {
                    break;
                }
            }

            return str;
            
        }

    }
}
