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
    public interface IPartialDecoratorInit1
    {
        void Init(MethodBase iMethod);
    }
    public interface IPartialDecoratorInit2
    {
        void Init(object iInstance,MethodBase iMethod);
    }
    public interface IPartialDecoratorInit3
    {
        void Init(object iInstance, MethodBase iMethod, object[] iParameters);
    }
    public interface IPartialDecoratorEntry
    {
        void OnEntry();
    }
    public interface IPartialDecoratorExit
    {
        void OnExit();
    }
    public interface IPartialDecoratorExit1
    {
        void OnExit(object iRetval);
    }
    public interface IPartialDecoratorException
    {
        void OnException(Exception iException);
    }
    interface IPartialDecoratorContinuation
    {
        void OnTaskContinuation(Task task);
    }
    interface IPartialDecoratorNeedBypass
    {
        bool NeedBypass();
    }
    interface IPartialAlterRetval
    {
        object AlterRetval(object iRetval);
    }
}
