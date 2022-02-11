using System;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Bili;
using Bili.Exceptions;
using Bili.Models;

namespace BiliPassport
{
    class Program
    {
        static void Main(string[] args)
        {
            // Init
            Console.WriteLine("-------- Info --------");
            string username, password;
            if (File.Exists("login.txt"))
            {
                string[] lines = File.ReadAllLines("login.txt");
                username = lines[0];
                password = lines[1];
                Console.WriteLine($"username: {username}");
                Console.WriteLine($"password: {password}");
            }
            else
            {
                Console.Write("username: ");
                username = Console.ReadLine().Trim();
                Console.Write("password: ");
                password = Console.ReadLine().Trim();
            }
            Console.WriteLine();

            try
            {
                // Normal login
                Console.WriteLine("-------- Normal login --------");
                LoginInfo loginInfo = BiliLogin.Login(username, password);
                Console.WriteLine(loginInfo);
                Console.WriteLine();
            }
            catch (LoginFailedException ex)
            {
                Console.WriteLine(ex);
            }
            catch (LoginStatusException ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("-------- Finished --------");
            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
