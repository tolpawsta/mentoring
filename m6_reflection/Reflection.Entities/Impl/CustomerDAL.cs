using Reflection.DIContainer.Attributes;
using Reflection.Entities.Interfaces;

namespace Reflection.Entities.Impl
{
    [Export(typeof(ICustomer))]
    [Export(typeof(IWorker))]

    public class CustomerDAL:ICustomer,IWorker
    {
        public string Name { get; set; }

        public string Buy(string item)
        {
            return $"I buy {item}";
        }

        public string Working(string name)
        {
            return $"I {name}";
        }
    }
}