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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BetDataAcquisition;

namespace SimpleRiskApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DataProviderManager _dataProviderManager;

        public MainWindow()
        {
            InitializeComponent();

            var betDataProviderFactory = new BetDataProviderFactory();
            _dataProviderManager = new DataProviderManager(betDataProviderFactory);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _dataProviderManager.CreateDataProvidersFromConfig();
            _dataProviderManager.StartAllDataProviders();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _dataProviderManager.StopAllDataProviders();
        }
    }
}
