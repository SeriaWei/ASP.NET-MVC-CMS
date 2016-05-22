using Easy.Web.CMS.Validate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.CMS.Common.Controllers
{
    public class ValidationController : Controller
    {
        private readonly IValidateService _validateService;
        public ValidationController(IValidateService validateService)
        {
            _validateService = validateService;
        }

        public ActionResult Code()
        {

            return File(_validateService.GenerateCode(), @"image/jpeg");
        }
        public JsonResult ValidCode(string code)
        {
            return Json(_validateService.ValidateCode(code), JsonRequestBehavior.AllowGet);
        }
    }
}
