using Reflection.DIContainer.Attributes;
using Reflection.Entities.Interfaces;

namespace Reflection.Entities.Impl
{
    public class CustomerB
    {
        [Import]
        public ICustomer Customer { get; set; }
        [Import]
        public Logger AppLogger { get; set; }
       public void Run()
        {
            AppLogger.InfoToConsole("Customerb :");
            AppLogger.InfoToConsole(Customer.Buy("buy technik"));
        }
    }
}