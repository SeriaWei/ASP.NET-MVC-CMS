using System;

namespace Easy.Web.RazorEngineTemplate
{
    public class CombileFailException : Exception
    {
        public CombileFailException()
            : base("编译模板失败，请检查模板的正确性。")
        {

        }

        public CombileFailException(string message)
            :base(message)
        {
            
        }
    }
}
