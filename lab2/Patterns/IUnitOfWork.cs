using Lab2.Patterns.Repositories;

namespace Lab2.Patterns
{
    public interface IUnitOfWork: System.IDisposable
    {
        IBookRepository Books { get; }
        ISageRepository Sages { get; }
        int Complete();
    }
}
