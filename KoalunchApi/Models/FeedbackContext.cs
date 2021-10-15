using System;
using Microsoft.EntityFrameworkCore;

namespace koalunch_api.Models
{
	public class FeedbackContext: DbContext
	{
		public DbSet<FeedbackItem> FeedbackItems { get; set; }

        public string DbPath { get; private set; }

        public FeedbackContext() 
        {
            DbPath = $"{Environment.CurrentDirectory}{System.IO.Path.DirectorySeparatorChar}database.sqlite3";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite($"Data Source={DbPath}");
        }
	}

}