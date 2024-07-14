using Microsoft.EntityFrameworkCore;
using URLShortenAspire.DB;
using URLShortenAspire.Models.Database;

namespace URLShortenAspire.DAL.Repositories
{
	public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
	{
		private DBContext _context;
		private readonly DbSet<TEntity> _dbSet;

		public GenericRepository(DBContext dBContext)
		{
			_context = dBContext;
			_dbSet = _context.Set<TEntity>();
		}

		public TEntity? GetById(Guid id) => _dbSet.Find(id);

		public IEnumerable<TEntity> GetAll() => _dbSet.ToList();

		public TEntity Add(TEntity entity)
		{

			entity.CreatedAt = DateTime.UtcNow;
			return _dbSet.Add(entity).Entity;
		}

		public TEntity Update(TEntity entity)
		{
			entity.UpdatedAt = DateTime.UtcNow;
			return _dbSet.Update(entity).Entity;
		}

		public void Delete(TEntity entity) => _dbSet.RemoveRange(entity);
	}
}
