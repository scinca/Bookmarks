using Bookmarks.Common.PagedResponse;

namespace Bookmarks;
static class QueryExtensions
{ 
    extension<TEntity>(IQueryable<TEntity> query)
    {
        /// <summary>
        ///  Implements pagination to limit the amount of bookmarks returned.
        /// </summary>
        /// <remarks>
        /// Always use an OrderBy before using this method
        /// </remarks>
        /// <param name="page">The number of the page, defaults to one</param>
        /// <param name="pageSize">The size of the page, see <see cref="PageSize"/> for the different options</param>
        /// <returns> An <see cref="IQueryable{TEntity}"/> to chain LINQ methods </returns>
        public IQueryable<TEntity> Paginate(int page, PageSize pageSize)
        {
            var sizeAsInt = (int) pageSize;
            return query.Skip((page - 1) * sizeAsInt).Take(sizeAsInt);
        }
    }
}