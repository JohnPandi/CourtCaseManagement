using CourtCaseManagement.ApplicationCore.TOs;
using System;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.Exceptions
{
    public class ErrorsException : Exception
    {
        public ErrorsResponseTO ErrorResponse { get; set; }

        public ErrorsException(List<ErrorsTO> errors) : base()
        {
            ErrorResponse = new ErrorsResponseTO
            {
                Errors = errors
            };
        }

        public ErrorsException(string code, string message) : base(message)
        {
            ErrorResponse = new ErrorsResponseTO
            {
                Errors = new List<ErrorsTO>()
                {
                    new ErrorsTO()
                    {
                        Field = code,
                        Validation = message,
                    }
                }
            };
        }
    }
}