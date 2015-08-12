using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Text;

namespace Infrastructure.Web.Paging
{
    public class CustomQueryStringBuilder : NameValueCollection
    {
        private readonly string _parameterName;
        public CustomQueryStringBuilder(NameValueCollection collection, string parameterName)
            : base(collection)
        {
            _parameterName = parameterName;
        }
        public override string ToString()
        {
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            var result = new StringBuilder();
            if (base.AllKeys.Any())
                result.Append("?");
            foreach (var key in base.AllKeys)
            {
                string[] values = base.GetValues(key);
                if (values != null && values.Count() > 0)
                    result.Append(key + "=" + url.Encode(values[0]) + "&");
            }
            string resultString = result.ToString();
            return resultString.EndsWith("&") ? resultString.Substring(0, resultString.Length - 1) : resultString;
        }
        public string GetQueryStringForParameter(string parameterValue)
        {
            if (base[_parameterName] == null)
                base.Add(_parameterName, parameterValue);
            else
                base[_parameterName] = parameterValue;
            return ToString();
        }
    }
}