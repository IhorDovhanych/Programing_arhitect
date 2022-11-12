using System;
using System.Collections.Generic;
/*
3. Необхідно написати систему керуванням дроном.
Припустимо, що дрон матеріальна точка яка вміє рухатись в 6 напрямах:
вгору (U), вниз (D), вперед (F), назад (B), вліво (L) та вправо (R) .
Нехай дрон приймає програму переміщення як послідовність команд у форматі
<напрям руху> <кількість метрів>, наприклад рух вгору на 1 метр,
а потім вперед на 3 метри і посадка вниз на 1 метр запишется як: U 1 F 3 D 1.
Визначити координати дрона після виконання такої програми.
Передбачити можливість додавання нових команд, наприклад скидування вантажу.
(Інтерпритатор) */
namespace Lab4Arch
{
    class Program
    {
        static void Main(string[] args)
        {
            string operations = "U1 D3 F5 U12 B2 L3 R7";

            Drone drone = new Drone(operations);
            drone.Interpret(drone);
            Console.WriteLine(drone);
        }
    }
    
    interface IExpresion
    {
        public Drone Interpret(IExpresion context);
    }
    class Drone : IExpresion
    {
        public int x = 0, y = 0, z = 0;
        List<IExpresion> operations = new List<IExpresion>();
        Drone drone;
        public Drone(int x, int y, int z, string operations)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            foreach (string operation in operations.Split(' '))
            {
                if (operation[0] == 'U')
                {
                    this.operations.Add(new Up(operation.Remove(0, 1)));
                }
                else if (operation[0] == 'D')
                {
                    this.operations.Add(new Down(operation.Remove(0, 1)));
                }
                else if (operation[0] == 'R')
                {
                    this.operations.Add(new Right(operation.Remove(0, 1)));
                }
                else if (operation[0] == 'L')
                {
                    this.operations.Add(new Left(operation.Remove(0, 1)));
                }
                else if (operation[0] == 'F')
                {
                    this.operations.Add(new Front(operation.Remove(0, 1)));
                }
                else if (operation[0] == 'B')
                {
                    this.operations.Add(new Back(operation.Remove(0, 1)));
                }
            }
        }
        public Drone(string operations)
        {
            foreach (string operation in operations.Split(' '))
            {
                if(operation[0] == 'U')
                {
                    this.operations.Add(new Up(operation.Remove(0, 1)));
                }
                else if (operation[0] == 'D')
                {
                    this.operations.Add(new Down(operation.Remove(0, 1)));
                }
                else if(operation[0] == 'R')
                {
                    this.operations.Add(new Right(operation.Remove(0, 1)));
                }
                else if(operation[0] == 'L')
                {
                    this.operations.Add(new Left(operation.Remove(0, 1)));
                }
                else if(operation[0] == 'F')
                {
                    this.operations.Add(new Front(operation.Remove(0, 1)));
                }
                else if(operation[0] == 'B')
                {
                    this.operations.Add(new Back(operation.Remove(0, 1)));
                }
            }
        }


        public Drone Interpret(IExpresion context)
        {
            foreach(IExpresion operation in operations)
            {
                drone = operation.Interpret(context);
            }

            return drone;
        }

        public override string ToString()
        {
            return $"X: {x}, Y: {y}, Z:{z}";
        }
    }
    class Operation: IExpresion
    {
        public string operation { get; set; }
        public Operation(string operation)
        {
            this.operation = operation;
        }
        public Drone Interpret(IExpresion context)
        {
            return context as Drone;
        }
    }
    class Up : IExpresion
    {
        public int operation { get; set; }
        public int getOperation()
        {
            return this.operation;
        }
        public Up(string operation)
        {
            this.operation = Convert.ToInt32(operation);

        }

        public Drone Interpret(IExpresion context)
        {
            Drone drone = context as Drone;
            drone.z += operation;
            return drone;
        }

    }
    class Down : IExpresion
    {
        public int operation { get; set; }
        public int getOperation()
        {
            return this.operation;
        }
        public Down(string operation)
        {
            this.operation = Convert.ToInt32(operation);
        }

        public Drone Interpret(IExpresion context)
        {
            Drone drone = context as Drone;
            drone.z -= operation;
            return drone;
        }
    }
    class Left : IExpresion
    {
        public int operation { get; set; }
        public int getOperation()
        {
            return this.operation;
        }
        public Left(string operation)
        {
            this.operation = Convert.ToInt32(operation);
        }

        public Drone Interpret(IExpresion context)
        {
            Drone drone = context as Drone;
            drone.y -= operation;
            return drone;
        }
    }
    class Right : IExpresion
    {
        public int operation { get; set; }
        public int getOperation()
        {
            return this.operation;
        }
        public Right(string operation)
        {
            this.operation = Convert.ToInt32(operation);
        }

        public Drone Interpret(IExpresion context)
        {
            Drone drone = context as Drone;
            drone.y += operation;
            return drone;
        }
    }
    class Front : IExpresion
    {
        public int operation { get; set; }
        public int getOperation()
        {
            return this.operation;
        }
        public Front(string operation)
        {
            this.operation = Convert.ToInt32(operation);
        }

        public Drone Interpret(IExpresion context)
        {
            Drone drone = context as Drone;
            drone.x += operation;
            return drone;
        }
    }
    class Back : IExpresion
    {
        public int operation { get; set; }
        public int getOperation()
        {
            return this.operation;
        }
        public Back(string operation)
        {
            this.operation = Convert.ToInt32(operation);
        }

        public Drone Interpret(IExpresion context)
        {
            Drone drone = context as Drone;
            drone.x -= operation;
            return drone;
        }
    }

}
