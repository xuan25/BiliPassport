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
        /// <summary>
        /// Show a image in a new window
        /// </summary>
        /// <param name="image">image</param>
        /// <returns>Window thread</returns>
        public static Thread ShowImage(BitmapImage image)
        {
            Thread windowThread = new Thread(() =>
            {
                Window window = new Window()
                {
                    Title = "Captcha",
                    Width = image.Width * 2,
                    Height = image.Height * 2 + 36,
                    Content = new Image() { Source = image }
                };
                window.ShowDialog();
            });
            windowThread.SetApartmentState(ApartmentState.STA);
            windowThread.Start();

            return windowThread;
        }

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

            try
            {
                // Login with captcha
                Console.WriteLine("-------- Login with captcha --------");
                BitmapImage captchaImage = BiliLogin.GetCaptcha();

                Thread captchThread = ShowImage(captchaImage);
                Console.Write("Please type in the captcha: ");
                string captcha = Console.ReadLine();
                captchThread.Abort();

                LoginInfo loginInfo = BiliLogin.Login(username, password, captcha);
                Console.WriteLine(loginInfo);
                Console.WriteLine();

                // Refresh token
                Console.WriteLine("-------- Refresh token --------");
                LoginToken newLoginToken = BiliLogin.RefreshToken(loginInfo.Token);
                Console.WriteLine(newLoginToken);
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
