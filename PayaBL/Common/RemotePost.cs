using System.Collections.Specialized;
using System.Web;

namespace PayaBL.Common
{
    internal class RemotePost
    {
        // Fields
        private NameValueCollection inputValues = new NameValueCollection();

        // Methods
        public RemotePost()
        {
            Url = "http://www.someurl.com";
            Method = "post";
            FormName = "formName";
        }

        public void Add(string name, string value)
        {
            inputValues.Add(name, value);
        }

        public void Post()
        {
            HttpContext context = HttpContext.Current;
            context.Response.Clear();
            context.Response.Write("<html><head>");
            context.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
            context.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName,
                                                 Method, Url));
            for (int i = 0; i < inputValues.Keys.Count; i++)
            {
                context.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">",
                                                     HttpUtility.HtmlEncode(inputValues.Keys[i]),
                                                     HttpUtility.HtmlEncode(inputValues[inputValues.Keys[i]])));
            }
            context.Response.Write("</form>");
            context.Response.Write("</body></html>");
            context.Response.End();
        }

        // Properties
        public string FormName { get; set; }

        public string Method { get; set; }

        public NameValueCollection Params
        {
            get { return inputValues; }
        }

        public string Url { get; set; }
    }

}