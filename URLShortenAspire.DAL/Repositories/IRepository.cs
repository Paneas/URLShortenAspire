using System.Linq.Expressions;

namespace URLShortenAspire.DAL.Repositories
{
	public interface IRepository<T>
	{
		T? GetById(Guid id);

		T? Get(Expression<Func<T, bool>> filter);
		IEnumerable<T> GetAll();
		T Add(T entity);
		T Update(T entity);
		void Delete(T entity);
	}
}
