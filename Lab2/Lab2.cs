using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



//Варіант 3.Розробити метод класу Календар  для копіювання об'єкту Події (Дата події, опис, секретний ключ для online сеансу зв’язку та список учасників, де кожен учасник – об’єкт Користувач (логін, пароль, e-mail )) в календарі на іншу дату. Передбачити можливість існування двох видів подій: 

// 1. Зустріч - подія, яка має час початку, час завершення та 2. День народження - подія , яка має дату та контакти іменинника  – об'єкт Користувач. 
//Основні вимоги: Секретний ключ та пароль - захищені поля, решта полів - публічні.  Календар - об'єкт що містить список
//подій та методи: додавання нової події, та вивід всіх подій.

namespace ArhitectureLab2
{
    class Program
    {
        static void Main(string[] args)
        {
            User Igor = new User("Igor", "123", "lol@gmail.com");
            User Sashka = new User("Sashka", "LovePivo", "sashkaprogramist@gmail.com");
            //User[] usersArr = new User[2] {Igor, Sashka};
            List<User> usersArr = new List<User> { Igor, Sashka };


            Meeting meetWithSashka = new Meeting(new DateTime(2022, 10, 17), "в курілці", "LovePHP", usersArr, Igor, new DateTime(2022, 10, 17, 9, 20, 0), new DateTime(2022, 10, 17, 14, 20, 0));
            BithdayDate bithdayInSashka = new BithdayDate(new DateTime(2023, 6, 21), "днюха у сашки купити кільце з хелоу кіті", "Bithday", usersArr, Igor, new DateTime(2023, 6, 25), Sashka);

            List<Action> actions = new List<Action> { meetWithSashka, bithdayInSashka };

            Console.WriteLine(meetWithSashka + "\n");
            Console.WriteLine(bithdayInSashka + "\n");

            Calendar IgorsCalendar = new Calendar(actions);
            Console.WriteLine(IgorsCalendar);

            IgorsCalendar.Clone(1, new DateTime(2024, 6, 21));
            Console.WriteLine(IgorsCalendar);
            IgorsCalendar.actionArr[2].userArr.Add(Igor);
            Console.WriteLine(IgorsCalendar.actionArr[2].userArr[2]);

            Console.WriteLine(IgorsCalendar.actionArr[1].userArr[2]);

        }
    }

    public interface IPrototype{
      
        IPrototype Clone();

    }

    class User : IPrototype
    {
        public string name,email;
        private string _password;
        public string Password { set { _password = value; } get { return _password; } }

        public User(string name, string password, string email) {
            this.name = name;
            this._password = password;
            this.email = email;
        }
        public override string ToString()
        {
            return name+ ", " + Password+ ", " + email;
        }

        public IPrototype Clone()
        {
            return new User(name, Password, email);
        }
    }

    class Calendar
    {
        public List<Action> actionArr;
        public Calendar(List<Action> actionArr)
        {
            this.actionArr = actionArr;
        }

        public void addAction(Action value)
        {
            actionArr.Add(value);
        }
        public override string ToString()
        {
            string str = "";
            foreach (Action el in actionArr)
            {
                str += el.description + ", ";
            }
            return str;
        }

        public Calendar Clone(int i, DateTime newDate)
        {
            actionArr.Add(actionArr[i].Clone() as Action);

            actionArr[actionArr.Count-1].date = newDate;
            return this;
        }
    }


    class Action : IPrototype
    {
        public DateTime date;
        public string description;
        public User user;
        public List<User> userArr;
        private string _secretKey;
        public string SecretKey { get { return _secretKey; } set { _secretKey = value; } }
        public Action(DateTime date, string description, string secretKey, List<User> userArr, User user)
        {
            this.date = date;
            this.description = description;
            this._secretKey = secretKey;
            this.userArr = userArr;
            this.user = user;
        }
        public override string ToString()
        {
            return date + ", " + description + ", " + user;
        }
        public virtual IPrototype Clone()
        {
            return new Action(date, description, SecretKey, new List<User>(userArr), user);
        }
    }

    class Meeting: Action
    {
        public DateTime startDate, finishDate;
        public Meeting(DateTime date, string description, string secretKey, List<User> userArr, User user, DateTime startDate, DateTime finishDate) : base(date, description, secretKey, userArr,user)
        {
            this.startDate = startDate;
            this.finishDate = finishDate;
        }
        public override string ToString()
        {
            return date + ", " + description + ", " + user + ", " + startDate + ", " + finishDate;
        }
        public override IPrototype Clone()
        {
            return new Meeting(date, description, SecretKey, new List<User>(userArr), user, startDate, finishDate);
        }
    }

    class BithdayDate: Action
    {
        public DateTime bithdayDate;
        public User bithdayMan;
        public BithdayDate(DateTime date, string description, string secretKey, List<User> userArr, User user, DateTime bithdayDate, User bithdayMan) : base(date, description, secretKey, userArr, user)
        {
            this.bithdayDate = bithdayDate;
            this.bithdayMan = bithdayMan;
        }
        public override string ToString()
        {
            return date + ", " + description + ", " + user + ", " + bithdayDate + ", " + bithdayMan;
        }

        public override IPrototype Clone()
        {
            return new BithdayDate(date, description, SecretKey, new List<User>(userArr), user, bithdayDate, bithdayMan);
        }
    }
}
