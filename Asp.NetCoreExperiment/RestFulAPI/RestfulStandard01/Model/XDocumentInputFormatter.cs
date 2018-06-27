using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RestfulStandard01.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class XDocumentInputFormatter : InputFormatter, IInputFormatter, IApiRequestFormatMetadataProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public XDocumentInputFormatter()
        {
            SupportedMediaTypes.Add("application/xml");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected override bool CanReadType(Type type)
        {
            if (type.IsAssignableFrom(typeof(XDocument)))
            {
                return true;
            }
            return base.CanReadType(type);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var xmlDoc = await XDocument.LoadAsync(context.HttpContext.Request.Body, LoadOptions.None, CancellationToken.None);
            return InputFormatterResult.Success(xmlDoc);
        }
    }
}
