using System;
using System.IO;

namespace TaskSix
{
    internal class Program
    {
        private const string Path = @"Text.txt";
        static void Main(string[] args)
        {
            Console.WriteLine("Добрый день, что вы хотите сделать? " +
                "\nЕсли просмотреть данные сотрудников, то введите цифру 1" +
                "\nЕсли добавить данные новых сотрудников, то введите цифру 2" +
                "\nЕсли хотите закончить работу с модулем, то введите цифру 0");
            byte argument = byte.Parse(Console.ReadLine());
            Console.Clear();
            bool fileExists = File.Exists(Path);
            if (argument == 1) if (fileExists) Print(); else Console.WriteLine("Список сотрудников пуст");
            else if (argument == 2)
            {
                Console.WriteLine("Данные какого количества новых сотрудников вы хотите ввести?");
                byte argument2 = byte.Parse(Console.ReadLine());
                string[] newWorkerArr = new string[argument2];
                for (int i = 0; i < newWorkerArr.Length; i++)
                {
                    newWorkerArr[i] = GetString(i, fileExists);
                    Console.WriteLine(newWorkerArr[i]);
                }
                WriterFile(newWorkerArr, fileExists);

            }
            else if (argument == 0) Console.WriteLine("Хорошего Вам дня =)");
            else
            {
                Console.WriteLine("Вы ввели некорректные данные, повторите ввод");
                Main(args);
            }
        }
        
        /// <summary>
        /// Метод для получения строки с данными нового сотрудника
        /// </summary>
        /// <returns>Строка с данными сотрудника</returns>
        static string GetString(int i, bool fileExists)
        {
            int id;
            if (fileExists)
            {
                string[] arr = File.ReadAllLines(Path);
                if (arr.Length > 0)
                {  
                    string [] brr = arr[^1].Split('#');
                    id = int.Parse(brr[0]) + 1 + i;
                }
                else id = 1 + i;                
            } 
            else id = 1 + i;
            DateTime date = DateTime.Now;
            string dateOfCreation = Convert.ToString(date);
            Console.WriteLine("Введите фамилию сотрудника");
            string surname = Console.ReadLine();
            Console.WriteLine("Введите имя сотрудника");
            string name = Console.ReadLine();
            Console.WriteLine("Введите отчество сотрудника");
            string lastName = Console.ReadLine(); 
            Console.WriteLine("Введите возраст сотрудника");
            byte age = byte.Parse(Console.ReadLine());
            string fullName = surname + " " + name + " " + lastName;
            Console.WriteLine("Введите рост сотрудника");
            byte height = byte.Parse(Console.ReadLine());
            Console.WriteLine("Введите дату рождения сотрудника в пормате ДД.ММ.ГГГГ");
            DateTime dayOfBirth = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Введите место рождения сотрудника");
            string placeOfBirth = Console.ReadLine();
            return (id + "#" + dateOfCreation + "#" + fullName + "#" + age + "#" + height + "#"
                + dayOfBirth.ToShortDateString() + "#" + placeOfBirth);
        }
        /// <summary>
        /// Метод по выводу на экран данных из файла.
        /// </summary>
        static void Print()
        {
            using StreamReader sr = new(Path);
            string[] result;
            string line = sr.ReadLine();
            if (line == null) Console.WriteLine("Список сотрудников пуст");
            else
            {
                while (line != null)
                {
                    result = line.Split("#");
                    for (int j = 0; j < result.Length; j++) Console.Write(result[j] + " ");
                    Console.WriteLine();
                    line = sr.ReadLine();
                }
            }
        }

        /// <summary>
        /// Метод по записи данных в текстовый файл
        /// </summary>
        /// <param name="newWorkerArr" массив данных из метода GetString()></param>
        static void WriterFile(string[] newWorkerArr, bool fileExists)
        {

            if (fileExists)
            {
                string[] oldWorkerArr = File.ReadAllLines(Path);
                using StreamWriter sw = new(Path);
                for (int i = 0; i < oldWorkerArr.Length; i++) sw.WriteLine(oldWorkerArr[i]);
                for (int i = 0; i < newWorkerArr.Length; i++) sw.WriteLine(newWorkerArr[i]);
            }
            else
            {
                using StreamWriter sw2 = new(Path);
                for (int i = 0; i < newWorkerArr.Length; i++) sw2.WriteLine(newWorkerArr[i]);
            }
        }
    }
}
