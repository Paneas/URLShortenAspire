using Microsoft.EntityFrameworkCore;

namespace URLShortenAspire.DB
{
	public class DBContext : DbContext
	{
		public DBContext(DbContextOptions<DBContext> options) : base(options)
		{ }

		public DbSet<URL> URLS { get; set; }
	}

	public class URL
	{
		public Guid Id { get; set; }
		public string OriginalURL { get; set; } = default!;

		public string Shorten { get; set; } = default!;
	}
}
