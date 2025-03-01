using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    [Table("Dates")] //Определение то, что для этого класса будет создана отдельная таблица
    /// <summary>
    /// Дата пункта
    /// </summary>
    public class Date/* : Clause*/
    {
        private int _id; // id
        private DateTime _dateStart; //Дата начала
        private DateTime? _dateEnd; //Дата конца (действует только при типе == цель)

        public int Id
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime DateStart
        {
            get => _dateStart;
            set => _dateStart = value;
        }

        /// <summary>
        /// Дата конца
        /// </summary>
        public DateTime? DateEnd
        {
            get => _dateEnd;
            set => _dateEnd = value;
        }
    }
}
