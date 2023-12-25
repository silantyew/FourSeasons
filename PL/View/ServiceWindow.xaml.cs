using FourSeasons.ViewModel;
using System.Windows.Controls;

namespace FourSeasons.View
{
    /// <summary>
    /// Логика взаимодействия для ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : UserControl
    {
        public ServiceWindow(ApplicationViewModel appViewModel)
        {
            InitializeComponent();
            DataContext = appViewModel;

        }

    }
}
