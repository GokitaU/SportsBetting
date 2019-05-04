﻿namespace SportsBetting.Services.Feeder.Factories
{
    using System;

    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Remote;

    using SportsBetting.Services.Feeder.Contracts.Factories;

    public class WebDriverFactory : IWebDriverFactory
    {
        private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string ResourcesDirectory = $"{BaseDirectory}/Resources";

        public RemoteWebDriver CreateWebDriver(int port)
        {
            ChromeOptions options = GetOptions(port);
            RemoteWebDriver webDriver = new ChromeDriver(ResourcesDirectory, options);

            return webDriver;
        }

        private ChromeOptions GetOptions(int port)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("window-size=1920,1080");
            options.AddArgument($"--remote-debugging-port={port}");
            options.AddArgument("--start-maximized");
            options.AddArgument("no-sandbox");
            options.AddArgument("--disable-gpu");
            options.AddUserProfilePreference("profile.default_content_setting_values.images", 2);

            return options;
        }
    }
}