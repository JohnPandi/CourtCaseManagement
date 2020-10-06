using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CourtCaseManagement.ApplicationCore.Helpers.Query;
using CourtCaseManagement.ApplicationCore.Interfaces;

namespace CourtCaseManagement.ApplicationCore.Specification
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {

        }

        public List<Expression<Func<T, bool>>> Criterias { get; } = new List<Expression<Func<T, bool>>>();
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; } = false;
        public virtual void AddCriteria(Expression<Func<T, bool>> expression)
        {
            Criterias.Add(expression);
        }
        public virtual void AddCriteria(DateTime? property, Expression<Func<T, bool>> expression)
        {
            if (property != null)
            {
                Criterias.Add(expression);
            }
        }
        public virtual void AddCriteria(Guid? property, Expression<Func<T, bool>> expression)
        {
            if (property != null)
            {
                Criterias.Add(expression);
            }
        }
        public virtual void AddCriteria(decimal? property, Expression<Func<T, bool>> expression)
        {
            if (property != null)
            {
                Criterias.Add(expression);
            }
        }
        public virtual void AddCriteria(string property, Expression<Func<T, bool>> expression)
        {
            if (!string.IsNullOrEmpty(property))
            {
                Criterias.Add(expression);
            }
        }
        public virtual void AddCriteria(int? property, Expression<Func<T, bool>> expression)
        {
            if (property != null)
            {
                Criterias.Add(expression);
            }
        }
        public virtual void AddCriteria(long? property, Expression<Func<T, bool>> expression)
        {
            if (property != null)
            {
                Criterias.Add(expression);
            }
        }
        public virtual void AddCriteria(short? property, Expression<Func<T, bool>> expression)
        {
            if (property != null)
            {
                Criterias.Add(expression);
            }
        }
        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected virtual void AddIncludes<TProperty>(Func<IncludeAggregator<T>, IIncludeQuery<T, TProperty>> includeGenerator)
        {
            var includeQuery = includeGenerator(new IncludeAggregator<T>());
            IncludeStrings.AddRange(includeQuery.Paths);
        }
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
        public virtual void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
        public virtual void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        public virtual void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
        //Not used anywhere at the moment, but someone requested an example of setting this up.
        protected virtual void ApplyGroupBy(Expression<Func<T, object>> groupByExpression)
        {
            GroupBy = groupByExpression;
        }
    }
}