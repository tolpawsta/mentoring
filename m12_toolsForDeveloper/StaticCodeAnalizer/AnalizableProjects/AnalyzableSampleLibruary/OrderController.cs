using System.Web.Mvc;

namespace AnalyzableSampleLibruary
{
    [Authorize]    
    class OrderController 
    {
        [Authorize]
        public void Method() { }
        [Authorize]
        public void LastMethod() { }
    }
}
