using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Diary
{
    /// <summary>
    /// Логика взаимодействия для AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public AddWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(DateStart.SelectedDate == null || Description == null)
            {
                MessageBox.Show("Одно из полей не заполнено!!!");
                return;
            }

            Clause clause = new Clause()
            {
                Description = Description.Text,
                Date = new Date { DateStart = (DateTime)DateStart.SelectedDate,
                    DateEnd = DateEnd.SelectedDate,
                },
                Close = DateEnd.SelectedDate == null ? null : false,
                TypeClause = DateEnd.SelectedDate == null ? Type.Reminder : Type.Task
            };

            ConnectionDB db = ConnectionDB.getInstance();
            db.Entry(clause.Date).State = EntityState.Added;
            db.Clauses.Add(clause);

            db.SaveChanges();

            this.Close();

            Application.Current.Windows[0].Show();
        }
    }
}
