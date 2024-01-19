namespace Husa.Extensions.Api.Conventions
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ApplicationModels;

    public class RoutePrefixConvention : IControllerModelConvention
    {
        private readonly AttributeRouteModel attributeRouteModel;
        private readonly string[] excludedNamespace;

        public RoutePrefixConvention(string prefix, params string[] excluded)
        {
            this.attributeRouteModel = new AttributeRouteModel(new RouteAttribute(prefix));
            this.excludedNamespace = excluded ?? Array.Empty<string>();
        }

        public void Apply(ControllerModel controller)
        {
            if (Array.Exists(this.excludedNamespace, n => controller.DisplayName.Contains(n)))
            {
                return;
            }

            foreach (var selector in controller.Selectors)
            {
                selector.AttributeRouteModel = selector.AttributeRouteModel != null ?
                    AttributeRouteModel.CombineAttributeRouteModel(this.attributeRouteModel, selector.AttributeRouteModel) :
                    this.attributeRouteModel;
            }
        }
    }
}
