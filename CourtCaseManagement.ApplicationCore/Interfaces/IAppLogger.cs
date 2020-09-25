using System;

namespace CourtCaseManagement.ApplicationCore.Interfaces
{
    public interface IAppLogger<T>
    {
        void LogError(Exception ex, string message, params object[] args);
    }
}