using URLShortenAspire.DAL.Repositories;
using URLShortenAspire.DB;
using URLShortenAspire.Models.Database;

namespace URLShortenAspire.DAL.Units
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly DBContext _context;
		private GenericRepository<URLEntity>? _urlRepository = null;

		public UnitOfWork(DBContext context) => _context = context;

		public GenericRepository<URLEntity> UrlRepository
		{
			get
			{
				_urlRepository ??= new GenericRepository<URLEntity>(_context);
				return _urlRepository;
			}
		}

		public void BeginTransaction()
		{
			_context.Database.BeginTransaction();
		}

		public void Commit()
		{
			_context.Database.CommitTransaction();
		}
	}
}
