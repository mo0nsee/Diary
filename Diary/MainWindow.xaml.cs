using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
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
        ConnectionDB db;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db = new ConnectionDB();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();

            Clause clause1 = new Clause(Type.Reminder) { Description = "Проверка" };
            clause1.AddElement(db, new DateTime(2025, 1, 26));

            //ClauseGrid.Items.Add(clause);

            Clause clause2 = new Clause(Type.Task) { Description = "Проверка" };
            clause2.AddElement(db, new DateTime(2025, 1, 26), new DateTime(2025, 1, 28));

            var values = new ObservableCollection<Clause>
            {
                clause1,
                clause2
            };
            ClauseGrid.ItemsSource = values;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (Clause selectedClause in ClauseGrid.SelectedItems)
            {
                MessageBox.Show($"Выбрано: {selectedClause.Description}, {selectedClause.Date.DateStart:dd.MM.yyyy}, {selectedClause.Date.DateEnd:dd.MM.yyyy}, Закрыто: {selectedClause.Close}");
                db.Clauses.Update(selectedClause);
                db.SaveChanges();
            }
        }
    }
}