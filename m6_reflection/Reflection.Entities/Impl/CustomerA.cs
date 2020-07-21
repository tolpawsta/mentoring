using System;
using Reflection.DIContainer.Attributes;
using Reflection.Entities.Interfaces;

namespace Reflection.Entities.Impl
{
    [ImportConstructor]
    public class CustomerA
    {
        private ICustomer _customerDal;
        private Logger _logger;

        public CustomerA(ICustomer customerDal, Logger logger)
        {
            _customerDal = customerDal;
            _logger = logger;
        }

        public ICustomer GetCustomer() => _customerDal;
        public Logger GetLogger => _logger;

        public void Run()
        {
            _logger.InfoToConsole("CustomerA :");
           _logger.InfoToConsole( _customerDal.Buy("products"));
        }
    }
}
