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

        /// <summary>
        /// Добавление записи в бд
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Проверка обязательных полей
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

            //Добавление в бд
            ConnectionDB db = ConnectionDB.getInstance();
            db.Entry(clause.Date).State = EntityState.Added;
            db.Clauses.Add(clause);
            db.SaveChanges();

            //Закрытие формы и открытие другой
            this.Close();
            Application.Current.Windows[0].Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Закрытие формы и открытие другой
            Application.Current.Windows[0].Show();
        }
    }
}
