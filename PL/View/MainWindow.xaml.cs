using FourSeasons.ViewModel;
using System.Windows;

namespace FourSeasons
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //связываем представление и модель через ViewModel
            DataContext = new ApplicationViewModel(this);
        }
    }
}
