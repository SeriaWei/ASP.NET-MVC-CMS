using Easy.HTML.Validator;
using Easy.Web.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Easy.Web.Validator
{
    /// <summary>
    /// 远程验证
    /// </summary>
    public class RemoteModelValidator : ModelValidatorBase<RemoteAttribute>
    {
        string action;
        string controller;
        string area;
        public RemoteModelValidator(EasyModelMetaData metadata, ControllerContext context, RemoteValidator remotevalidator)
            : base(metadata, context)
        {
            this.area = remotevalidator.Area;
            this.action = remotevalidator.Action;
            this.controller = remotevalidator.Controller;
            this.Attribute = new RemoteAttribute(action, controller);
            //this.Attribute.ErrorMessageResourceType = Metadata.ContainerType;
            //this.Attribute.ErrorMessageResourceName = Metadata.PropertyName;
            this.Attribute.ErrorMessage = remotevalidator.ErrorMessage;
            this.Attribute.AdditionalFields = remotevalidator.AdditionalFields;
        }

        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            string url = (this.ControllerContext.Controller as System.Web.Mvc.Controller).Url.Content(string.Format("~/{0}/{1}", this.controller, this.action));
            if (!string.IsNullOrEmpty(area))
            {
                url = (this.ControllerContext.Controller as System.Web.Mvc.Controller).Url.Content(string.Format("~/{0}/{1}/{2}", this.area, this.controller, this.action));
            }
            return new[] { new ModelClientValidationRemoteRule(this.Attribute.ErrorMessage, url, this.Attribute.HttpMethod, this.Attribute.AdditionalFields) };
        }
    }
}
