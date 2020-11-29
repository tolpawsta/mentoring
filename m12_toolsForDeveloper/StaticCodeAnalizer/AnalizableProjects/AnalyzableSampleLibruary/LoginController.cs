using System.Web.Mvc;

namespace AnalyzableSampleLibruary
{
    public class LoginController : Controller
    {
        [Authorize]
        private void FirstMethod() { }

        [Authorize]
        public void SecondMethod() { }      
        [Authorize]
        public void LastMethod() { }

    }
}
