using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Diary
{
    /// <summary>
    /// Пункт
    /// </summary>
    public class Clause
    {
        private int _id; // id
        private string _description; //Описание
        private string _type; //Тип
        private bool? _close; // Закрытие(действует только при типе == задача)
        private Date _date; // Дата

        private Type _typeClause;

        /// <summary>
        /// ID Пункта
        /// </summary>
        public int Id
        { 
          get => _id; 
          set => _id = value; 
        }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description
        { 
          get => _description;
          set =>  _description = value; 
        }

        /// <summary>
        /// Тип
        /// </summary>
        public string Type
        {
            get => _type;
            set => _type = value;
        }

        /// <summary>
        /// Закрытие пункта
        /// </summary>
        public bool? Close
        {
            get => _close;
            set => _close = value;
        }

        public Date Date 
        {
            get => _date;
            set => _date = value;
        }

        /// <summary>
        /// Преобразование типа к строке
        /// </summary>
        private void TypeConversion()
        {
            switch (_typeClause) 
            {
                case Diary.Type.Task:
                    _type = "Задача";
                    _close = false;
                    break;
                case Diary.Type.Reminder:
                    _type = "Напоминание";
                    break;
                default:
                    _type = "Ошибка";
                    break;
            }
        }

        /// <summary>
        /// Добавление Задачи
        /// </summary>
        public void AddElement(ConnectionDB db, DateTime dateStart, DateTime dateEnd)
        {
            _date = new Date() { DateStart = dateStart, DateEnd = dateEnd };
            //db.Dates.Add(date);
            db.Clauses.Add(this);
            db.SaveChanges();
        }

        /// <summary>
        /// Добавление Напоминания
        /// </summary>
        public void AddElement(ConnectionDB db, DateTime dateStart)
        {
            _date = new Date() { DateStart = dateStart };
            //db.Dates.Add(date);
            db.Clauses.Add(this);
            db.SaveChanges();
        }

        /// <summary>
        /// Создание экземпляра с определением типа задачи
        /// </summary>
        /// <param name="typeClause"></param>
        public Clause(Type typeClause) 
        {
            _typeClause = typeClause;
            TypeConversion();
        }

        public Clause()
        {
        }
    }
}
