using Microsoft.EntityFrameworkCore;
using URLShortenAspire.Models.Database;

namespace URLShortenAspire.DB
{
	public class DBContext : DbContext
	{
		public DBContext(DbContextOptions<DBContext> options) : base(options)
		{ }

		public DbSet<URLEntity> URLS { get; set; }
	}
}
