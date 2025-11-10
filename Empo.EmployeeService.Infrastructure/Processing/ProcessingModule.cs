//using Autofac;

//namespace Empo.EmployeeService.Infrastructure.Processing;
//public class ProcessingModule : Autofac.Module
//{
//    protected override void Load(ContainerBuilder builder)
//    {
//        builder.RegisterType<DomainEventsDispatcher>()
//            .As<IDomainEventsDispatcher>()
//            //			.InstancePerRequest();
//            .InstancePerLifetimeScope();

//        builder.RegisterType<DomainEventsAccessor>()
//            .As<IDomainEventsAccessor>()
//                //		.InstancePerRequest();
//                .InstancePerLifetimeScope();

//    }
//}
