using System;
using System.Threading.Tasks;
using LokiLogger.WebExtension.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace LokiLogger.WebExtension.Controller
{
   public abstract class LokiController:Microsoft.AspNetCore.Mvc.Controller {
        public bool RethrowException { get; set; } = LokiObjectAdapter.LokiConfig.DefaultLokiControllerRethrowException;
        public string GeneralErrorMessage { get; set; } = LokiObjectAdapter.LokiConfig.DefaultControllerErrorMessage;
        public string GeneralErrorCode { get; set; } = LokiObjectAdapter.LokiConfig.DefaultControllerErrorCode;
        public async Task<IActionResult> CallRest<D>(Func<Task<OperationResult<D>>> result)
        {
            try
            {
                OperationResult<D> res = await result();
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                return BadRequest(OperationResult<D>.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
        public async Task<IActionResult> CallRest<D,T>(Func<T,Task<OperationResult<D>>> result,T input)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = await result(input);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
        
        public async Task<IActionResult> CallRest<D,T,G>(Func<T,G,Task<OperationResult<D>>> result,T input,G input1)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = await result(input,input1);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
        
        public async Task<IActionResult> CallRest<D,T,G,A>(Func<T,G,A,Task<OperationResult<D>>> result,T input,G input1,A input2)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = await result(input,input1,input2);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
        
        public async Task<IActionResult> CallRest<D,T,G,A,B>(Func<T,G,A,B,Task<OperationResult<D>>> result,T input,G input1,A input2, B input3)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = await result(input,input1,input2,input3);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
        
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
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
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
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
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
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
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
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
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
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
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
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
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
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
        
        public IActionResult CallRest<D,T,G,A,B,C,E,F>(Func<T,G,A,B,C,E,F,OperationResult<D>> result,T input,G input1,A input2, B input3, C input4,E input5,F input6)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input,input1,input2,input3,input4,input5,input6);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
        
        public IActionResult CallRest<D,T,G,A,B,C,E,F,H>(Func<T,G,A,B,C,E,F,H,OperationResult<D>> result,T input,G input1,A input2, B input3, C input4,E input5,F input6,H input7)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input,input1,input2,input3,input4,input5,input6,input7);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
        
        public IActionResult CallRest<D,T,G,A,B,C,E,F,H,I>(Func<T,G,A,B,C,E,F,H,I,OperationResult<D>> result,T input,G input1,A input2, B input3, C input4,E input5,F input6,H input7,I input8)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input,input1,input2,input3,input4,input5,input6,input7,input8);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
        
        public IActionResult CallRest<D,T,G,A,B,C,E,F,H,I,J>(Func<T,G,A,B,C,E,F,H,I,J,OperationResult<D>> result,T input,G input1,A input2, B input3, C input4,E input5,F input6,H input7,I input8,J input9)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                OperationResult<D> res = result(input,input1,input2,input3,input4,input5,input6,input7,input8,input9);
                if (res.Succeeded)
                {
                    return Ok(res.SuccessResult);
                }
                return BadRequest(res.Errors);
            }
            catch (Exception e)
            {
                Loki.ExceptionError(e);
                HttpContext.Items["Exception"] = e;
                if (RethrowException) throw;
                
                return BadRequest(OperationResult.Fail(GeneralErrorCode,GeneralErrorMessage).Errors);
            }
        }
    }

}