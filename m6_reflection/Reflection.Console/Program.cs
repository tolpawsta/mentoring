using System;
using System.Reflection;
using Reflection.DIContainer;
using System.Linq;
using Reflection.DIContainer.Exceptions;
using Reflection.Entities.Impl;
using Reflection.Entities.Interfaces;
using static System.Console;

namespace Reflection.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new DiContainer();
            container.AddAssembly(Assembly.GetExecutingAssembly());
            WriteLine($"Assemblies:");
            foreach (var assembly in container.Assemblies)
            {
                WriteLine($"{assembly.GetName().Name}");
                container.GetTypes(assembly).ForEach(t=>WriteLine($"\t{t.Name}"));
            }

            WriteLine("Create CustomerA without registration ICustomer");
            try
            {
                var customerA = (CustomerA) container.CreateInstance(typeof(CustomerA));
            }
            catch (DiException ex)
            {
                WriteLine(ex.Message,ex.InvalidType);
            }

            WriteLine("Create CustomerA and CustomerB after registered ICustomer");
            container.AddType(typeof(CustomerDAL),typeof(ICustomer));
            try
            {
                var customerA = (CustomerA) container.CreateInstance(typeof(CustomerA));
                WriteLine("CustomerA run");
                customerA.Run();

                WriteLine("CustomerB run");
                var customerB = container.CreateInstance<CustomerB>();
                customerB.Run();
            }
            catch (DiException ex)
            {
                WriteLine(ex.Message, ex.InvalidType);
            }

            WriteLine("Try register ICustomer again");
            container.AddType(typeof(CustomerDAL),typeof(ICustomer));

            WriteLine("Try register type CustomerC without attributes");
            container.AddType(typeof(CustomerC));

            WriteLine("Register CastomerDAL with interface IWorker and create IWorker");
            container.AddType(typeof(CustomerDAL), typeof(IWorker));
            var workerC = container.CreateInstance<IWorker>();
            WriteLine(workerC.Working("sleeping"));
            ReadLine();
        }
    }
}
