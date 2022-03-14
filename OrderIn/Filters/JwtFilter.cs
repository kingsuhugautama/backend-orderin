using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using OrderInBackend.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderIn.Filters
{
    public class JwtFilter : ActionFilterAttribute
    {
        private ClassHelper _helper;

        public JwtFilter()
        {
            this._helper = new ClassHelper();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            #region Debug Req Body
            StringBuilder sb = new StringBuilder();

            foreach (var arg in context.ActionArguments)
            {
                sb.Append(arg.Key.ToString() + ":" + JsonConvert.SerializeObject(arg.Value) + "\n");
            }

            string requestBody = sb.ToString(); //untuk debugging requestt body

            #endregion

            var root = this._helper.GetConfigurationSettings();

            //ada jwt enable
            if (Convert.ToBoolean(root.GetSection("IsSecured").Value.ToString())){
                string token = context.HttpContext.Request?.Headers["uat"];

                //base64 userid
                string authid = context.HttpContext.Request?.Headers["auth"];
                string userId = authid == null ? "" : Encoding.UTF8.GetString(Convert.FromBase64String(authid));

                string validateToken = this._helper.ValidateToken(token);

                if (!string.IsNullOrEmpty(token))
                {

                    if (validateToken != userId)
                    {
                        if (validateToken.IndexOf("expired") > -1)
                        {
                            context.Result = new UnauthorizedObjectResult(new
                            {
                                data = "Akses tidak diizinkan",
                                refreshToken = this._helper.GenerateToken(userId.ToString())
                            });
                        }
                        else
                        {
                            context.Result = new UnauthorizedObjectResult(new
                            {
                                data = "Akses tidak diizinkan",
                                refreshToken = ""
                            });
                        }
                    }
                }
                else
                {
                    context.Result = new UnauthorizedObjectResult(new
                    {
                        data = "Akses tidak diizinkan",
                        refreshToken = ""
                    });
                }
            }

            base.OnActionExecuting(context);
        }

    }
}
