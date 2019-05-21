using LokiWebExtension.Interception.Interfaces;

namespace LokiWebExtension.Interception.Strategies
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Pyramid Ordering Strategy for Interceptors
    /// </summary>
    public sealed class PyramidOrderStrategy : IOrderingStrategy
    {
        /// <inheritdoc />
        public IEnumerable<InvocationContext> OrderBeforeInterception(IEnumerable<InvocationContext> interceptors)
        {
            return interceptors;
        }

        /// <inheritdoc />
        public IEnumerable<InvocationContext> OrderAfterInterception(IEnumerable<InvocationContext> interceptors)
        {
            return interceptors.Reverse();
        }
    }
}
