﻿using System;
using System.Linq;
using System.Text;

namespace LoggingHelper
{
    //todo: localize
    public static class ExceptionExtensions
    {
        private static string CreateRecord(string data, string separator, bool insertSeparator)
        {
            return insertSeparator ? $"{separator}{data}" : data;
        }

        public static string GetAllMessages(this Exception exception, string separator = null, bool addStackTrace = false, bool innerExceptionsFirst = false)
        {
            string sep = separator ?? Environment.NewLine;
            StringBuilder sb = new StringBuilder();
            Exception error = exception;
            string message;
            bool insertSeparator = false;
            Exception topError = exception;

            while (error != null)
            {
                message = error is AggregateException
                    ? error.GetType().FullName + ": " + error.Message + sep + string.Join(sep, ((AggregateException)error).InnerExceptions.Select(o => o.GetType().FullName + ": " + o.Message))
                    : error.GetType().FullName + ": " + error.Message;

                sb.Append(CreateRecord(message, sep, insertSeparator));
                insertSeparator = true;

                // AggregateException sets InnerException value for some reason, we use InnerExceptions collection to retrieve messages
                if (error is AggregateException)
                {
                    break;
                }

                error = error.InnerException;
            }

            string result = sb.ToString();

            if (innerExceptionsFirst)
            {
                result = string.Join(sep, result.Split(new[] { sep }, StringSplitOptions.RemoveEmptyEntries).Reverse());
            }

            if (addStackTrace)
            {
                sb.Clear();
                sb.Append(result);
                sb.Append(CreateRecord(topError.StackTrace ?? "No stack trace information.", sep + sep, true));
                result = sb.ToString();
            }

            return result;
        }

        public static T FindFirst<T>(this Exception source)
            where T : Exception
        {
            T result = null;

            Exception error = source;

            while (error != null)
            {
                if (error is T)
                {
                    result = (T)error;
                    break;
                }

                //todo: AggregateException omitted for this implementation
                if (error is AggregateException)
                {
                }
                 
                error = error.InnerException;
            }

            return result;
        }
    }
}
