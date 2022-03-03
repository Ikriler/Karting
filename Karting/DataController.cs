using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Input;

namespace Karting
{
    class DataController
    {
        public static DateTime DateDayX = new DateTime(2022, 3, 7, 14, 10, 0);
        public static Label dynamicDate;

        public static void ShowDate()
        {
            MessageBox.Show(new DateTime().ToString());
        }

        public static void StartTimerOnCurrentWindow(TextBlock staticDate, Label dynamicDatee)
        {
            staticDate.Text = String.Format("Москва, Россия {0} марта {1}", DateDayX.Day, DateDayX.Year);
            dynamicDate = dynamicDatee;
            DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Timer);
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(1000);
            dispatcherTimer.Start();
        }

        public static void Timer(object sender, EventArgs e)
        {
            try 
            {
                TimeSpan timerTime = DateDayX.Subtract(DateTime.UtcNow);
                DateTime date = new DateTime() + timerTime;
                dynamicDate.Content = String.Format("До начала события осталось {0} лет, {1} месяцев, {2} дней, {3} часов, {4} минут и {5} секунд", date.Year - 1, date.Month - 1, date.Day - 1, date.Hour, date.Minute, date.Second);
                CommandManager.InvalidateRequerySuggested();
            }
            catch
            {
                dynamicDate.Content = String.Format("До начала события осталось 0 лет, 0 месяцев, 0 дней, 0 часов, 0 минут и 0 секунд");
            }
        }

    }
}
