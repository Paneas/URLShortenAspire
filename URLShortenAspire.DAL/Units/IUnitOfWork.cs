namespace URLShortenAspire.DAL.Units
{
	public interface IUnitOfWork
	{
		void BeginTransaction();
		void Commit();
	}
}
