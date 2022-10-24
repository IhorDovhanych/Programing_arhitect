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
            Login log1 = new Login(DateTime.Now, "Andrashko");

            log1.addRequest(DateTime.Now, "Andrashko")
                .addRequest(DateTime.Now, "Pivo")
                .addRequest(DateTime.Now, "Putin Died")
                .addRequest(DateTime.Now, "Andrashko")
                .addRequest(DateTime.Now, "Pivo")
                .addRequest(DateTime.Now, "Pivo")
                .addRequest(DateTime.Now, "Putin Died")
                .addRequest(DateTime.Now, "Pivo")
                .addRequest(DateTime.Now, "P#hub")
                .addRequest(DateTime.Now, "Putin Died")
                .addRequest(DateTime.Now, "Putin Died")
                .addRequest(DateTime.Now, "Putin Died");

            Console.WriteLine(log1.ShowTopRequests(3));



        }
    }

    interface ISubject
    {
        string ShowTopRequests(int n);
        string[] Request(DateTime date, string request);
    }
    
    class Login: ISubject
    {
        private List<string[]> requestArr = new List<string[]>();
        public Login(DateTime date, string request = "")
        {
            
            this.requestArr.Add(new string[] { date.ToString(), request });
        }

        public Login addRequest(DateTime date, string request = "")
        {
            this.requestArr.Add(new string[] { $"{date}", request });
            return this;
        }


        public string[] Request(DateTime date, string request = "")
        {
            this.requestArr.Add(new string[] { $"{date}", request });      
            return new string[] { "//Google", "//Facebook" };
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
