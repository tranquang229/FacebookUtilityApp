﻿using FM.Application.Constants;
using FM.Application.Utilities;
using FM.Domain.Common;
using System.Linq.Expressions;

namespace FM.Application.Interfaces.Repositories
{
    public interface IRepository<T, TId> where T : AudiTableEntity<TId>
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> condition, Expression<Func<T, int>> orderCondition, SortType sortType = SortType.Descending);

        Task<IEnumerable<T>> GetWithPagingAsync(
            Expression<Func<T, bool>> condition,
            Expression<Func<T, int>> orderCondition,
            SortType sortType = SortType.Descending,
            int page = AppConstant.PageIndex,
            int pageSize = AppConstant.PageSize);

        Task<T> GetByIdAsync(TId id);

        Task<T> AddAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteByIdsAsync(IEnumerable<TId> ids);

        Task<bool> DeleteByIdAsync(TId id);

        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
    }
}
