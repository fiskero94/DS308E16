using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyPlatform.Classes.Exceptions
{
    public class ArgumentNotSecretaryException : Exception
    {
        public ArgumentNotSecretaryException(string message) : base(message)
        {

        }
        public ArgumentNotSecretaryException()
        {

        }
    }
}