﻿namespace LokiWebExtension.Interception.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Exception thrown when an Interceptor Attribute is applied but the Interceptor hasnt been configured
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class InvalidInterceptorException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="InvalidInterceptorException"/> class
        /// </summary>
        /// <param name="errorMessage">Error Message Text</param>
        public InvalidInterceptorException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
