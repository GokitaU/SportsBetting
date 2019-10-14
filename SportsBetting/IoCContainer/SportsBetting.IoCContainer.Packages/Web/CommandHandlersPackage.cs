﻿namespace SportsBetting.IoCContainer.Packages.Web
{
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Packaging;

    using SportsBetting.Common.Results;
    using SportsBetting.Data.Models;
    using SportsBetting.Handlers.Commands.Accounts;
    
    using SportsBetting.Handlers.Commands.Contracts;

    public sealed class CommandHandlersPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<ICommandHandler<CreateAccountCommand, Account>, CreateAccountCommandHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<EncryptPasswordCommand, string>, EncryptPasswordCommandHandler>(new WebRequestLifestyle());
            container.Register<ICommandHandler<LoginAccountCommand, ValidationResult>, LoginAccountCommandHandler>(new WebRequestLifestyle());
            container.Register<IValidationHandler<CreateAccountCommand>, CreateAccountValidationHandler>(new WebRequestLifestyle());
        }
    }
}