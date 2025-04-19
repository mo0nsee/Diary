using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    /// <summary>
    /// Соединение с бд
    /// </summary>
    public class ConnectionDB : DbContext
    {
        private static ConnectionDB instance;
        /// <summary>
        /// Создание экземпляра
        /// </summary>
        /// <returns></returns>
        public static ConnectionDB getInstance()
        {
            if (instance == null)
                instance = new ConnectionDB();
            return instance;
        }

        /// <summary>
        /// Получение таблицы с пунктами
        /// </summary>
        public DbSet<Clause> Clauses => Set<Clause>();

        public void Close()
        {
            Database.CloseConnection();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-5DQNS6K\SQLEXPRESS;Database=DiaryDB;Trusted_Connection=True; Encrypt=False");
        }
    }
}
