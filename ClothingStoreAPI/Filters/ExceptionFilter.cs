using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Filters
{
    
        public class CustomExceptionFilter : IExceptionFilter
        {
            public void OnException(ExceptionContext context)
            {
                if (context.Exception != null)
                {
                 
                    string errorMessage = context.Exception.Message;

                 
                    var result = new ObjectResult(errorMessage)
                    {
                        StatusCode = 500 
                    };
                    
                    context.Result = result;
                   
                    context.ExceptionHandled = true;
                }
            }
        }
    
}
