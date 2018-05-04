using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ivey.Utils
{
    public static class Validate
    {
        public static Validation Begin()
        {
            return null;
        }
    }

    public sealed class Validation
    {
        private List<Exception> exceptions;

        public IEnumerable<Exception> Exceptions
        {
            get
            {
                return this.exceptions;
            }
        }

        public Validation AddException(Exception ex)
        {
            lock (this.exceptions)
            {
                this.exceptions.Add(ex);
            }

            return this;
        }

        public Validation()
        {
            this.exceptions = new List<Exception>(1); // optimize for only having 1 exception
        }

    }

    public static class ValidationExtensions
    {
        public static Validation IsNotNull<T>(this Validation validation, T theObject, string paramName) 
        {
            if (theObject == null)
            {
                return (validation ?? new Validation()).AddException(new ArgumentNullException(paramName));
            }
            else
            {
                return validation;
            }
        }

        public static Validation DateRange(this Validation validation, DateTime startDate, DateTime endDate)
        {
            if (endDate < startDate)
            {
                return (validation ?? new Validation()).AddException(new ArgumentException("StartDate cannot come before EndDate"));
            }
            else
            {
                return validation;
            }
        }


        public static Validation IsPositive(this Validation validation, long value, string paramName)
        {
            if (value < 0)
            {
                return (validation ?? new Validation()).AddException(new ArgumentOutOfRangeException(paramName, "must be positive, but was " + value.ToString()));
            }
            else
            {
                return validation;
            }
        }

        //public static Validation Check(this Validation validation)
        public static void Check(this Validation validation)
        {
            //if (validation == null)
            //{
            //    return validation;
            //}
            //else
            //{
                if (validation != null && validation.Exceptions.Any() == true)
                {
                    throw validation.Exceptions.First();
                }

                //if (validation.Exceptions.Take(2).Count() == 1)
                //{
                //    throw new ValidationException(message, validation.Exceptions.First()); // ValidationException is just a standard Exception-derived class with the usual four constructors
                //}
                //else
                //{
                //    throw new ValidationException(message, new MultiException(validation.Exceptions)); // implementation shown below
                //}
            //}
        }

    }


}
