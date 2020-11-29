using System;
using System.Web.Mvc;

namespace AnalyzableSampleLibruary
{

    public class Home : Controller
    {
        [Authorize]               
        public void FirstMethod() {}
        [Authorize]
        public bool LastMethod(int number) 
        {
            return true;
        }

    }
}
