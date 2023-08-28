namespace EmailTrigger.Domain.Interfaces.Repostiories
{
    public interface IBaseRepository
    {
        IEnumerable<TResult> ReadRecentFile<TResult, TMappings>(Func<TResult, bool> predicate, string path, int headerIndex = 1,
                                                                bool deleteFile = false, string sheet = null)
            where TResult : class, new()
            where TMappings : class;

        IEnumerable<TResult> ReadFile<TResult, TMappings>(Func<TResult, bool> predicate, string fileName, int headerIndex = 1,
                                                        bool deleteFile = false, string sheet = null)
            where TResult : class, new()
            where TMappings : class;
    }
}
