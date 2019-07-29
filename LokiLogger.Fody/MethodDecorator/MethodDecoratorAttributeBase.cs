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
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace MethodDecorator.Fody.Interfaces
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
    public class MethodDecoratorAttribute : Attribute, IMethodDecorator {
        public virtual void Init(object instance, MethodBase method, object[] args) {}

        public virtual void OnEntry() {}

        public virtual void OnExit() {}

        public virtual void OnException(Exception exception) {}

        public virtual void OnTaskContinuation(Task task) {}
    }
}