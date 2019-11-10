using System;
using LokiLogger.WebExtension.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LokiLogger.WebExtension.Controller
{
    public abstract class LokiController:Microsoft.AspNetCore.Mvc.Controller
    {
        public bool RethrowException { get; set; }
        public string GeneralErrorMessage { get; set; }
        public IActionResult CallRest<D>(Func<OperationResult<D>> result)
        {
            try
            {
                OperationResult<D> res = result();
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                if (RethrowException) throw;
                return BadRequest(GeneralErrorMessage);
            }
        }

        public IActionResult CallRest<D,T>(Func<T,OperationResult<D>> result,T input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                if (RethrowException) throw;
                return BadRequest(GeneralErrorMessage);
            }
        }
        
        
        public IActionResult CallRest<D,T,G>(Func<T,G,OperationResult<D>> result,T input,G input1)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input,input1);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                if (RethrowException) throw;
                return BadRequest(GeneralErrorMessage);
            }
        }
        public IActionResult CallRest<D,T,G,A>(Func<T,G,A,OperationResult<D>> result,T input,G input1,A input2)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input,input1,input2);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                if (RethrowException) throw;
                return BadRequest(GeneralErrorMessage);
            }
        }
        public IActionResult CallRest<D,T,G,A,B>(Func<T,G,A,B,OperationResult<D>> result,T input,G input1,A input2, B input3)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input,input1,input2,input3);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                if (RethrowException) throw;
                return BadRequest(GeneralErrorMessage);
            }
        }
        
        public IActionResult CallRest<D,T,G,A,B,C>(Func<T,G,A,B,C,OperationResult<D>> result,T input,G input1,A input2, B input3, C input4)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input,input1,input2,input3,input4);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                if (RethrowException) throw;
                return BadRequest(GeneralErrorMessage);
            }
        }
        
        public IActionResult CallRest<D,T,G,A,B,C,E>(Func<T,G,A,B,C,E,OperationResult<D>> result,T input,G input1,A input2, B input3, C input4,E input5)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input,input1,input2,input3,input4,input5);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                if (RethrowException) throw;
                return BadRequest(GeneralErrorMessage);
            }
        }
    }
}