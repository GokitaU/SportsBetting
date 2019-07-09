﻿namespace SportsBetting.Feeder.Bootstrap
{
    using System.Reflection;

    using SimpleInjector.Packaging;

    using SportsBetting.Common.Contracts;
    using SportsBetting.Common.Infrastructure.Mapping;
    using SportsBetting.Data.Cache.Contracts;
    using SportsBetting.Feeder.Core.Contracts;
    using SportsBetting.IoCContainer;
    using SportsBetting.IoCContainer.Packages;
    using SportsBetting.IoCContainer.Packages.Feeder;

    public class FeederBootstrapper
    {
        private readonly ISynchronizer synchronizer;

        public FeederBootstrapper()
        {
            InitializeDependencies();
            InitializeDb();
            InitializeCaches();
            InitializeMapping();

            synchronizer = SportsBettingContainer.Resolve<ISynchronizer>();
        }

        public void Start()
        {
            synchronizer.Sync();
        }

        public void Stop()
        {
            synchronizer.Stop();
        }

        private void InitializeDependencies()
        {
            IPackage[] packages = new IPackage[]
            {
                new DataPackage(),
                new DataCachePackage(),
                new FeederPackage(),
                new QueryHandlersPackage(),
                new CommandHandlersPackage()
            };

            SportsBettingContainer.Initialize(packages);
        }

        private void InitializeDb()
        {
            IAplicationInitializer aplicationInitializer = SportsBettingContainer.Resolve<IAplicationInitializer>();
            aplicationInitializer.Init();
        }

        private void InitializeCaches()
        {
            ICacheInitializer cacheInitializer = SportsBettingContainer.Resolve<ICacheInitializer>();
            cacheInitializer.Init();
        }

        private void InitializeMapping()
        {
            const string MAPPING_ASSEMBLY = "SportsBetting.Handlers.Commands";
            AutoMapperConfig.RegisterMappings(Assembly.Load(MAPPING_ASSEMBLY));
        }
    }
}