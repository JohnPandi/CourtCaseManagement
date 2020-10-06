using CourtCaseManagement.ApplicationCore.Helpers.Query;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IIncludeQuery
    {
        Dictionary<IIncludeQuery, string> PathMap { get; }
        IncludeVisitor Visitor { get; }
        HashSet<string> Paths { get; }
    }
    public interface IIncludeQuery<TEntity, out TPreviousProperty> : IIncludeQuery
    {
    }
}