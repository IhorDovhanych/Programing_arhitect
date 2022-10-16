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

            Meeting meetWithSashka = new Meeting(new DateTime(2022, 10, 17), "в курілці", "LovePHP", Igor, new DateTime(2022, 10, 17, 10, 20, 0), new DateTime(2022, 10, 17, 14, 20, 0));
            BithdayDate bithdayInSashka = new BithdayDate(new DateTime(2023, 6, 21), "днюха у сашки купити кільце з хелоу кіті", "Bithday", Igor, new DateTime(2023, 6, 25), Sashka);

            Action[] actions = new Action[2] { meetWithSashka, bithdayInSashka };

            Console.WriteLine(meetWithSashka + "\n");
            Console.WriteLine(bithdayInSashka + "\n");

            Calendar IgorsCalendar = new Calendar(actions);
            Console.WriteLine(IgorsCalendar);
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

    class Calendar : IPrototype
    {
        public Action[] actionArr;
        public Calendar(Action[] actionArr)
        {
            this.actionArr = actionArr;
        }

        public void addAction(Action value)
        {
            actionArr.Append(value);
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

        public IPrototype Clone()
        {
            return new Calendar(actionArr);
        }
    }
    class Action : IPrototype
    {
        public DateTime date;
        public string description;
        public User user;
        private string _secretKey;
        public string SecretKey { get { return _secretKey; } set { _secretKey = value; } }
        public Action(DateTime date, string description, string secretKey, User user)
        {
            this.date = date;
            this.description = description;
            this._secretKey = secretKey;
            this.user = user;
        }
        public override string ToString()
        {
            return date + ", " + description + ", " + user;
        }
        public IPrototype Clone()
        {
            return new Action(date, description, SecretKey, user);
        }
    }

    class Meeting: Action
    {
        public DateTime startDate, finishDate;
        public Meeting(DateTime date, string description, string secretKey, User user, DateTime startDate, DateTime finishDate) : base(date, description, secretKey,user)
        {
            this.startDate = startDate;
            this.finishDate = finishDate;
        }
        public override string ToString()
        {
            return date + ", " + description + ", " + user + ", " + startDate + ", " + finishDate;
        }
        public IPrototype Clone()
        {
            return new Meeting(date, description, SecretKey, user, startDate, finishDate);
        }
    }

    class BithdayDate: Action
    {
        public DateTime bithdayDate;
        public User bithdayMan;
        public BithdayDate(DateTime date, string description, string secretKey, User user, DateTime bithdayDate, User bithdayMan) : base(date, description, secretKey, user)
        {
            this.bithdayDate = bithdayDate;
            this.bithdayMan = bithdayMan;
        }
        public override string ToString()
        {
            return date + ", " + description + ", " + user + ", " + bithdayDate + ", " + bithdayMan;
        }

        public IPrototype Clone()
        {
            return new BithdayDate(date, description, SecretKey, user,bithdayDate,bithdayMan);
        }
    }
}
