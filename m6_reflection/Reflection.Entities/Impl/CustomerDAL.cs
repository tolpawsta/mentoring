using Reflection.DIContainer.Attributes;
using Reflection.Entities.Interfaces;

namespace Reflection.Entities.Impl
{
    [Export(typeof(ICustomer))]
    public class CustomerDAL:ICustomer
    {
        [Import]
        public string Name { get; set; }
    }
}