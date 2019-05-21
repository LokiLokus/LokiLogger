using LokiWebExtension.Interception.Interfaces;

namespace LokiWebExtension.Interception.Strategies
{
    using System.Collections.Generic;

    /// <summary>
    /// Sequential Ordering Strategy for Interceptors
    /// </summary>
    public sealed class SequentialOrderStrategy : IOrderingStrategy
    {
        /// <inheritdoc />
        public IEnumerable<InvocationContext> OrderBeforeInterception(IEnumerable<InvocationContext> interceptors)
        {
            return interceptors;
        }

        /// <inheritdoc />
        public IEnumerable<InvocationContext> OrderAfterInterception(IEnumerable<InvocationContext> interceptors)
        {
            return interceptors;
        }
    }
}
