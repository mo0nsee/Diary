using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diary
{
    public class ConnectionDB : DbContext
    {
        private static ConnectionDB instance;

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

        /// <summary>
        /// получение таблицы с датами
        /// </summary>
        //public DbSet<Date> Dates => Set<Date>();

        private ConnectionDB()
        {
            //Проверка есть ли база данных, если нет, то ей создание
            //Database.EnsureCreated();
        }

        public void Close()
        {
            Database.CloseConnection();
            //Проверка есть ли база данных, если нет, то ей создание
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-5DQNS6K\SQLEXPRESS;Database=DiaryDB;Trusted_Connection=True; Encrypt=False");
        }
    }
}
