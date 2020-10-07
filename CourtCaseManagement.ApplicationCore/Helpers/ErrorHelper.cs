using CourtCaseManagement.ApplicationCore.Messages;
using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Text;

namespace CourtCaseManagement.ApplicationCore.Helpers
{
    public static class ErrorHelper
    {
        public static ErrorTraceTO GenericError(Exception ex)
        {
            var bytes = Encoding.Unicode.GetBytes(ex.StackTrace);
            return new ErrorTraceTO
            {
                TraceId = Convert.ToBase64String(bytes),
                Message = Messaging.UnexpectedError
            };
        }

        public static ErrorMessageTO NotFoundProcess = new ErrorMessageTO
        {
            Message = Messaging.NotFoundProcess
        };

        public static ErrorMessageTO NotFoundResponsible = new ErrorMessageTO
        {
            Message = Messaging.NotFoundResponsible
        };
    }
}