using FourSeasons.ViewModel;
using System.Windows.Controls;

namespace FourSeasons.View
{
    /// <summary>
    /// Логика взаимодействия для RevenueWindow.xaml
    /// </summary>
    public partial class RevenueWindow : UserControl
    {
        public RevenueWindow(ApplicationViewModel appViewModel)
        {
            InitializeComponent();
            DataContext = appViewModel;
        }
    }
}
