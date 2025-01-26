using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        private bool? _close; // Закрытие(действует только при типе == цель)

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

        /// <summary>
        /// Преобразование типа к строке
        /// </summary>
        private void TypeConversion()
        {
            switch (_typeClause) 
            {
                case Diary.Type.Task:
                    _type = "Задача";
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
        public void AddElement(ConnectionDB db, string description, DateTime dateStart, DateTime dateEnd)
        {
            Date date = new Date() { DateStart = dateStart, Description = description, Close = true, Type = _type };

            db.Clauses.Add(date);
            db.SaveChanges();
        }

        /// <summary>
        /// Добавление Напоминания
        /// </summary>
        public void AddElement(ConnectionDB db, string description, DateTime dateStart)
        {
            Date date = new Date() { DateStart = dateStart, Description = description, Type = _type };
            db.Clauses.Add(date);
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
