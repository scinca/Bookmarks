namespace Bookmarks;
static class QueryExtensions
{ 
    extension<TEntity>(IQueryable<TEntity> query)
    {
        internal IQueryable<TEntity> Paginate(int page, int pageSize)
            => query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}