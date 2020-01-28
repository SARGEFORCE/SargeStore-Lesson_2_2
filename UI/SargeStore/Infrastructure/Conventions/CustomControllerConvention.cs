using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace SargeStore.Infrastructure.Conventions
{
    public class CustomControllerConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            //controller.ControllerName
        }
    }
}