using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Produtos.Api.SeedWork;
using System.Linq;

namespace Produtos.Api.Controllers
{
    public abstract class ControllerBaseApi : ControllerBase
    {
        protected ErrorViewModel ErrorMessage(ModelStateDictionary modelState)
        {
            var errorMessages = modelState.Values.SelectMany(e => e.Errors).Select(x => x.ErrorMessage);

            return new ErrorViewModel(errorMessages);
        }
    }
}