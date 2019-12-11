using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace LokiLogger.WebExtension.ViewModel
{
	public static class OperationResult
	{
		public static OperationResult<object> Fail(string code, string description)
		{
			return OperationResult<object>.Fail(code, description);
		}
		
		public static OperationResult<object> Fail(string code, IEnumerable<string> description)
		{
			return OperationResult<object>.Fail(code, description);
		}
		public static OperationResult<object> Fail(string code, params string[] description)
		{
			return OperationResult<object>.Fail(code, description);
		}
		
		public static OperationResult<object> Fail(Dictionary<string, IEnumerable<string>> err)
		{
			return OperationResult<object>.Fail(err);
		}
		
		public static OperationResult<object> Success(object result)
		{
			return OperationResult<object>.Success(result);
		}
	}
	
	public class OperationResult<T>
	{
		public readonly bool Succeeded;
		public readonly T SuccessResult;
		public readonly Dictionary<string, IEnumerable<string>> Errors;
		
		/// <summary>
		/// Use when Operation succeeded
		/// </summary>
		/// <returns></returns>
		private OperationResult(T successResult)
		{
			Succeeded = true;
			SuccessResult = successResult;
		}

		/// <summary>
		/// Use when Operation fails
		/// </summary>
		/// <param name="errors"></param>
		private OperationResult(Dictionary<string, IEnumerable<string>> errors)
		{
			Succeeded = false;
			if (errors == null)
			{
				errors = new Dictionary<string, IEnumerable<string>>();
			}

			Errors = errors;
		}
		
		
		public static OperationResult<T> Fail(string code, string description)
		{
			var err = new Dictionary<string, IEnumerable<string>>
			{
				{code, new List<string>() {description}}
			};
			return new OperationResult<T>(err);
		}
		
		public static OperationResult<T> Fail(string code, IEnumerable<string> description)
		{
			var err = new Dictionary<string, IEnumerable<string>>();
			if(description == null) description = new List<string>();
			err.Add(code,description);
			return new OperationResult<T>(err);
		}
		public static OperationResult<T> Fail(string code, params string[] description)
		{
			var err = new Dictionary<string, IEnumerable<string>>();
			if(description == null) description = new string[0];
			err.Add(code,description);
			return new OperationResult<T>(err);
		}
		
		public static OperationResult<T> Fail(Dictionary<string, IEnumerable<string>> err)
		{
			if (err == null)
			{
				err = new Dictionary<string, IEnumerable<string>>();
			}
			return new OperationResult<T>(err);
		}
		
		public static OperationResult<T> Success(T result)
		{
			return new OperationResult<T>(result);
		}
	}
}