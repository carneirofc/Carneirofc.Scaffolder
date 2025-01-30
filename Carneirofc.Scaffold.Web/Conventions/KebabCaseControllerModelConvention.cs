namespace Carneirofc.Scaffold.Web.Conventions
{

    using Microsoft.AspNetCore.Mvc.ApplicationModels;
    using System.Text.Json;

    public class KebabCaseControllerModelConvention : IControllerModelConvention
    {
        private readonly JsonNamingPolicy _namingPolicy = JsonNamingPolicy.KebabCaseLower;
        public void Apply(ControllerModel controller)
        {
            controller.ControllerName = _namingPolicy.ConvertName(controller.ControllerName);
        }
    }
}