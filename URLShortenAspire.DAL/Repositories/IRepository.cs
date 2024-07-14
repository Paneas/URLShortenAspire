namespace URLShortenAspire.DAL.Repositories
{
	public interface IRepository<T>
	{
		T? GetById(Guid id);
		IEnumerable<T> GetAll();
		T Add(T entity);
		T Update(T entity);
		void Delete(T entity);
	}
}
