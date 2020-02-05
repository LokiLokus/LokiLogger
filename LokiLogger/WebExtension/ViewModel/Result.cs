using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace LokiLogger.WebExtension.ViewModel
{
	public  class Result: Result<object>
	{
		public static Result<T> Fail<T>(string code, string description)
		{
			var err = new Dictionary<string, IEnumerable<string>>
			{
				{code, new List<string>() {description}}
			};
			return new Result<T>(err);
		}
		public static Result<T> Fail<T>(string code, IEnumerable<string> description)
		{
			var err = new Dictionary<string, IEnumerable<string>>();
			if(description == null) description = new List<string>();
			err.Add(code,description);
			return new Result<T>(err);
		}
		public static Result<T> Fail<T>(string code, params string[] description)
		{
			var err = new Dictionary<string, IEnumerable<string>>();
			if(description == null) description = new string[0];
			err.Add(code,description);
			return new Result<T>(err);
		}
		
		public static Result<T> Fail<T>(Dictionary<string, IEnumerable<string>> err)
		{
			if (err == null)
			{
				err = new Dictionary<string, IEnumerable<string>>();
			}
			return new Result<T>(err);
		}
		
		public static Result<T> Ok<T>(T result)
		{
			return new Result<T>(result);
		}
		
		public static Result Fail(string code, string description)
		{
			var err = new Dictionary<string, IEnumerable<string>>
			{
				{code, new List<string>() {description}}
			};
			return new Result(err);
		}
		public static Result Fail(string code, IEnumerable<string> description)
		{
			var err = new Dictionary<string, IEnumerable<string>>
			{
				{code, description}
			};
			return new Result(err);
		}
		public static Result Fail(string code, params string[] description)
		{
			var err = new Dictionary<string, IEnumerable<string>>();
			if(description == null) description = new string[0];
			err.Add(code,description);
			return new Result(err);
		}
		
		public static Result Fail(Dictionary<string, IEnumerable<string>> err)
		{
			if (err == null)
			{
				err = new Dictionary<string, IEnumerable<string>>();
			}
			return new Result(err);
		}
		
		public static Result Ok()
		{
			return new Result((object)null);
		}

		internal Result(object successResult) : base(successResult)
		{
		}

		internal Result(Dictionary<string, IEnumerable<string>> errors) : base(errors)
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
		internal Result(T successResult)
		{
			Succeeded = true;
			SuccessResult = successResult;
		}

		/// <summary>
		/// Use when Operation fails
		/// </summary>
		/// <param name="errors"></param>
		internal Result(Dictionary<string, IEnumerable<string>> errors)
		{
			Succeeded = false;
			if (errors == null)
			{
				errors = new Dictionary<string, IEnumerable<string>>();
			}

			Errors = errors;
		}
	}
}