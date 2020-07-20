using System;
using Reflection.DIContainer.Attributes;

namespace Reflection.Entities.Impl
{
    [ImportConstructor]
    public class CustomerA
    {
        private CustomerDAL _customerDal;
        private Logger _logger;

        public CustomerA(CustomerDAL customerDal, Logger logger)
        {
            _customerDal = customerDal;
            _logger = logger;
        }

        public CustomerDAL GetCustomerDal() => _customerDal;
        public Logger GetLogger => _logger;
    }
}
