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

        /// <summary>
        /// Содержимое Grid 
        /// </summary>
        ObservableCollection<Clause> values;

        //Обновление содержимого при каждом открытии
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
        
        //Добавление записи
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Создание окна
            AddWindow window = new AddWindow();
            // Открытие окна
            window.Show();
            this.Hide();
        }

        //Сохранение изменений
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }

        //Удаление записи
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
            db.Close();
        }
    }
}