using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using AstarPets.Interview.Business.Core;

namespace AstarPets.Interview.Web.IoC
{
    public class WindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                AllTypes.Of<IController>().FromAssembly (Assembly.GetExecutingAssembly()).Configure(c => c.LifestyleTransient()));
                

            container.Register(AllTypes.Pick().FromAssemblyNamed("AstarPets.Interview.Business")
                                   .Configure(c => c.LifestyleTransient())
                                   .If(t => t.FindInterfaces((t1, o) => t1.Name == "IWorkflow", null).Length > 0)
                                   .BasedOn(typeof (IWorkflow<,>)).WithService.FromInterface(typeof(IWorkflow<,>)));
        }
    }
}