using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace LokiLogger.WebExtension.ViewModel
{
	public static class OperationResult {
		
		public static OperationResult<T> Success<T>(T obj)
		{
			return OperationResult<T>.Success(obj);
		}
		public static OperationResult<T> Fail<T>(string code, string description)
		{
			return OperationResult<T>.Failed<T>(code,description);
		}
	}
	
	public static class OpRes
	{
		public static OperationResult<T> Success<T>(T obj)
		{
			return OperationResult<T>.Success(obj);
		}
		public static OperationResult<T> Fail<T>(string code, string description)
		{
			return OperationResult<T>.Failed<T>(code,description);
		}
	}
	
	public class OperationResult<T>{
		public bool Succeeded { get; set; }
		public T SuccessResult { get; set; }
		public IEnumerable<OperationOutput> Errors { get; set; }

		public void Failed(params OperationOutput[] output)
		{
			if (output == null) output = new OperationOutput[0];
			if (Errors == null) Errors = new List<OperationOutput>();
			foreach (OperationOutput tmp in output) Errors.Append(tmp);
		}
		
		public static OperationResult<G> Failed<G>(string code, string description)
		{
			return new OperationResult<G>
			{
				Succeeded = false,
				Errors = new List<OperationOutput>
				{
					new OperationOutput(code, description)
				}
			};
		}
		public static OperationResult<G> Success<G>(G result)
		{
			return new OperationResult<G>
			{
				Succeeded = true,
				SuccessResult = result
			};
		}
	}

	public class OperationOutput {
		public OperationOutput(string code, string desc)
		{
			Code = code;
			Description = desc;
		}

		public OperationOutput()
		{
		}

		public string Code { get; set; }
		public string Description { get; set; }

		public static OperationOutput Operation(string code, string desc)
		{
			return new OperationOutput(code, desc);
		}
	}

}