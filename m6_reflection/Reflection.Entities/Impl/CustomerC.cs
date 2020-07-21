using Reflection.Entities.Interfaces;

namespace Reflection.Entities.Impl
{
    public class CustomerC
    {
        private ICustomer _customer;
        private Logger _logger;
        public CustomerC(ICustomer customer, Logger logger)
        {
            _customer = customer;
            _logger = logger;
        }

        public void DoWork()
        {
            _logger.InfoToConsole("CustomerC:");
            _logger.InfoToConsole(_customer.Buy("sleep"));
        }
    }
}