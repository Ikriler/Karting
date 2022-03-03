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
    public partial class InfoMainWindow : Window
    {
        public InfoMainWindow()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void kartSkills2017_Click(object sender, RoutedEventArgs e)
        {
            new InfoKartSkills().Show();
            this.Close();
        }

        private void SpisokOrganiztion_Click(object sender, RoutedEventArgs e)
        {
            new CharityList().Show();
            this.Close();
        }

        private void oldResults_Click(object sender, RoutedEventArgs e)
        {
            new AllOldResults().Show();
            this.Close();
        }
    }
}
