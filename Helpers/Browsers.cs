﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace Cappario
{
    public class Browsers
    {
        public string OutputDirectory { get => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }

        public IWebDriver LaunchChrome()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("profile.default_content_setting_values.images", 2);
            if (bool.Parse(ConfigurationManager.AppSettings.Get("HeadlessBrowser")) == true)
            {
                chromeOptions.AddArguments("headless", "no-sandbox", "--disable-gpu", "--window-size=1920x1080");
            }
            return new ChromeDriver(OutputDirectory, chromeOptions, TimeSpan.FromSeconds(180));
        }
    }
}