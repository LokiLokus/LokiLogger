using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace LokiLogger.WebExtension.ViewModel
{
	public  class Result: Result<object>
	{
		public static Result<T> Fail<T>(string code, string description,[CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			var err = new Dictionary<string, IEnumerable<string>>
			{
				{code, new List<string>() {description}}
			};
			return new Result<T>(err,lineNr,callerName,className);
		}
		public static Result<T> Fail<T>(string code, IEnumerable<string> description,[CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			var err = new Dictionary<string, IEnumerable<string>>();
			if(description == null) description = new List<string>();
			err.Add(code,description);;
			return new Result<T>(err,lineNr,callerName,className);
		}
		public static Result<T> Fail<T>(string code, string[] description,[CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			var err = new Dictionary<string, IEnumerable<string>>();
			if(description == null) description = new string[0];
			err.Add(code,description);
			return new Result<T>(err,lineNr,callerName,className);
		}
		
		public static Result<T> Fail<T>(Dictionary<string, IEnumerable<string>> err,[CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			if (err == null)
			{
				err = new Dictionary<string, IEnumerable<string>>();
			}
			return new Result<T>(err,lineNr,callerName,className);
		}
		
		public static Result<T> Ok<T>(T result,[CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			return new Result<T>(result,lineNr,callerName,className);
		}
		
		public static Result Fail(string code, string description,[CallerLineNumber] int lineNr = 0,
		[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			var err = new Dictionary<string, IEnumerable<string>>
			{
				{code, new List<string>() {description}}
			};
			return new Result(err,lineNr,callerName,className);
		}
		public static Result Fail(string code, IEnumerable<string> description,[CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			var err = new Dictionary<string, IEnumerable<string>>
			{
				{code, description}
			};
			return new Result(err,lineNr,callerName,className);
		}
		public static Result Fail(string code, string[] description,[CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			var err = new Dictionary<string, IEnumerable<string>>();
			if(description == null) description = new string[0];
			err.Add(code,description);
			return new Result(err,lineNr,callerName,className);
		}
		
		public static Result Fail(Dictionary<string, IEnumerable<string>> err,[CallerLineNumber] int lineNr = 0,
			[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			if (err == null)
			{
				err = new Dictionary<string, IEnumerable<string>>();
			}
			return new Result(err,lineNr,callerName,className);
		}
		
		public static Result Ok([CallerLineNumber] int lineNr = 0,
		[CallerMemberName] string callerName = "", [CallerFilePath] string className = "")
		{
			return new Result((object)null,lineNr,callerName,className);
		}

		internal Result(object successResult,int lineNr,string callerName, string className) : base(successResult,lineNr,callerName,className)
		{
		}

		internal Result(Dictionary<string, IEnumerable<string>> errors,int lineNr,string callerName, string className) : base(errors,lineNr,callerName,className)
		{
		}
	}
	
	public class Result<T>
	{
		public readonly bool Succeeded;
		public readonly T SuccessResult;
		public readonly Dictionary<string, IEnumerable<string>> Errors;
		
		/// <summary>
		/// Use when Operation succeeded
		/// </summary>
		/// <returns></returns>
		internal Result(T successResult,int lineNr,string callerName, string className)
		{
			Succeeded = true;
			SuccessResult = successResult;
			Loki.Return(this,lineNr,callerName,className);
		}

		/// <summary>
		/// Use when Operation fails
		/// </summary>
		/// <param name="errors"></param>
		internal Result(Dictionary<string, IEnumerable<string>> errors,int lineNr,string callerName, string className)
		{
			Succeeded = false;
			if (errors == null)
			{
				errors = new Dictionary<string, IEnumerable<string>>();
			}
			Errors = errors;
			Loki.Return(this,lineNr,callerName,className);
		}
	}
}