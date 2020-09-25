using CourtCaseManagement.ApplicationCore.TOs;
using System.Collections.Generic;

namespace CourtCaseManagement.ApplicationCore.Exceptions
{
    public class NotFoundException : ErrorsException
    {
        public NotFoundException(List<ErrorsTO> errors) : base(errors)
        {
        }

        public NotFoundException(string code, string message) : base(code, message)
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