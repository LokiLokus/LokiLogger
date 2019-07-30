/*
 *The MIT License

Copyright (c) Contributors

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
https://github.com/Fody/MethodDecorator
 * 
 */
using System.Linq;
using Mono.Cecil;

    public static class TypeReferenceExtensions
    {
        public static bool Implements(this TypeDefinition typeDefinition, TypeReference interfaceTypeReference)
        {
            while (typeDefinition?.BaseType != null)
            {
                if (typeDefinition.Interfaces != null && typeDefinition.Interfaces.Any(i => i.InterfaceType.FullName == interfaceTypeReference.FullName))
                {
                    return true;
                }

                typeDefinition = typeDefinition.BaseType.Resolve();
            }

            return false;
        }

        public static bool Implements(this TypeDefinition typeDefinition, string interfaceTypeReference)
        {
            while (typeDefinition?.BaseType != null)
            {
                if (typeDefinition.Interfaces != null && typeDefinition.Interfaces.Any(i => i.InterfaceType.FullName == interfaceTypeReference))
                {
                    return true;
                }

                typeDefinition = typeDefinition.BaseType.Resolve();
            }

            return false;
        }

        public static bool DerivesFrom(this TypeReference typeReference, TypeReference expectedBaseTypeReference)
        {
            while (typeReference != null)
            {
                if (typeReference.FullName == expectedBaseTypeReference.FullName)
                {
                    return true;
                }

                typeReference = typeReference.Resolve().BaseType;
            }

            return false;
        }
}