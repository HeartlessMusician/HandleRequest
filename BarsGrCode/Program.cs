using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarsGrCode
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Приложение запущено");
            Console.WriteLine("Введите текст отправки для запроса. Для выхода из приложения введите /exit");
            //сообщение
            string message = Console.ReadLine();

            while (message != "/exit")
            {
                //ввод аргументов
                var arguments = new string[500];
                Console.WriteLine("Введите аргументы запроса. Если завершили ввод, введите /end");
                var argument = Console.ReadLine();
                var i = 0;
                while (argument != "/end")
                {
                    arguments[i] = argument;
                    Console.WriteLine("Введите второй аргумент запроса сообщения. Для завершения ввода введите /end");
                    argument = Console.ReadLine();
                    i += 1;
                }
                ThreadPool.QueueUserWorkItem(callBack => HandleRequest(message, arguments));
                Console.WriteLine("Введите запрос для пересылки сообщения. Для выхода из приложения введите /exit");
                message = Console.ReadLine();

            }
            Console.WriteLine("Завершение работы приложения");
        }

        public static void HandleRequest(string message, string[] arguments)
        {
            var dummyRequestHandler = new DummyRequestHandler();
            var id = Guid.NewGuid().ToString("D");
            Console.WriteLine($"Было отправлено сообщение '{message}'. Присвоен идентификатор {id}");
            try
            {
                var answer = dummyRequestHandler.HandleRequest(message, arguments);
                Console.WriteLine($"Сообщение с идентификатором {id} получило ответ {answer}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Сообщение с индефикатором {id} упало с ошибкой: {e.Message} ");
            }
        }
    }
}