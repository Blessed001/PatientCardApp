using PatientCardApp.UI.ViewModel;
using System;
using System.Windows;

namespace PatientCardApp.UI
{

    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
           await _viewModel.LoadAsync();
        }
    }
}
