using FourSeasons.ViewModel;
using System.Windows.Controls;

namespace FourSeasons.View
{
    /// <summary>
    /// Логика взаимодействия для EditWindow.xaml
    /// </summary>
    public partial class ShowServicesWindow : UserControl
    {
        public ShowServicesWindow(ApplicationViewModel appViewModel)
        {
            InitializeComponent();
            DataContext = appViewModel;
        }
    }
}
