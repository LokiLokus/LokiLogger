﻿namespace LokiWebExtension.Interception.Attributes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Method Interception Attribute (inherit from this to create your interception attributes)
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    [ExcludeFromCodeCoverage]
    public abstract class MethodInterceptionAttribute : Attribute
    {
    }
}
