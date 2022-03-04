﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Karting
{
    /// <summary>
    /// Логика взаимодействия для SponsorsReview.xaml
    /// </summary>
    public partial class SponsorsReview : Window
    {
        public SponsorsReview()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            CoordinatorPanel coordinatorPanel = new CoordinatorPanel();
            coordinatorPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            coordinatorPanel.Show();
            this.Close();
        }
    }
}