using FourSeasons.ViewModel;
using System.Windows.Controls;

namespace FourSeasons.View
{
    /// <summary>
    /// Логика взаимодействия для ReservationWindow.xaml
    /// </summary>
    public partial class ReservationWindow : UserControl
    {
        public ReservationWindow(ApplicationViewModel appViewModel)
        {
            InitializeComponent();
            DataContext = appViewModel;
        }
    }
}
