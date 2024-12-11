using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            this._logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, " >>> Erro - Exceção NÂO tratada: STATUS CODE 500 <<<");
            context.Result = new ObjectResult(" >>> ERRO ao tratar solicitação: Status Code 500 <<<")
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
