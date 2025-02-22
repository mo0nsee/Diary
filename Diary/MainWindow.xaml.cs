using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Globalization;
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
        ConnectionDB db;

        public MainWindow()
        {
            InitializeComponent();
            db = ConnectionDB.getInstance();
            this.IsVisibleChanged += MainWindow_IsVisibleChanged;
        }

        ObservableCollection<Clause> values;

        ObservableCollection<Clause> valuesPrer;
        private void MainWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.IsVisible) // Если окно стало видимым
            {
                values = new ObservableCollection<Clause>();
                foreach (Clause clause in db.Clauses.Include(c => c.Date).ToList())
                {
                    values.Add(clause);
                }
                ClauseGrid.ItemsSource = values;
            }
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Создание окна
            AddWindow window = new AddWindow();
            // Открытие окна
            window.Show();
            this.Hide();

            //db.Database.EnsureDeleted();
            //db.Database.EnsureCreated();
            //Clause clause1 = new Clause(Type.Reminder) { Description = "Проверка" };
            //clause1.AddElement(db, new DateTime(2025, 1, 26));

            //Clause clause2 = new Clause(Type.Task) { Description = "Проверка" };
            //clause2.AddElement(db, new DateTime(2025, 1, 26), new DateTime(2025, 1, 28));

            //var values = new ObservableCollection<Clause>
            //{
            //    clause1,
            //    clause2
            //};
            //ClauseGrid.ItemsSource = values;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //foreach (Clause selectedClause in ClauseGrid.SelectedItems)
            //{
            //    db.Clauses.Update(selectedClause);
            //}
            db.SaveChanges();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (ClauseGrid.ItemsSource is ObservableCollection<Clause> clausesList)
            {
                var selectedClauses = ClauseGrid.SelectedItems.Cast<Clause>().ToList();

                foreach (Clause selectedClause in selectedClauses)
                {
                    db.Entry(selectedClause.Date).State = EntityState.Deleted;
                    db.Clauses.Remove(selectedClause);
                }

                foreach (Clause clause in selectedClauses)
                {
                    clausesList.Remove(clause);
                }

                db.SaveChanges();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Все несохраннённые изменения будут потеряны");
            //valuesPrer = (ObservableCollection<Clause>)ClauseGrid.ItemsSource;
            //int i = 0;
            //foreach (Clause selectedClause in db.Clauses.ToList())
            //{
            //    if (!valuesPrer[i].Equals(selectedClause))
            //        MessageBox.Show("Изменения не сохраненены");
            //    i++;
            //}
            db.Close();
        }
    }
}