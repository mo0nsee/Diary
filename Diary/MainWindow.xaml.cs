using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Diary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ConnectionDB db = new ConnectionDB();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Clause clause = new Clause(Type.Reminder);
            clause.AddElement(db, "Проверка", new DateTime(2025, 1, 26));

            clause = new Clause(Type.Task);
            clause.AddElement(db, "Проверка", new DateTime(2025, 1, 26), new DateTime(2025, 1, 28));
        }
    }
}