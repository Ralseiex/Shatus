﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shatus.WinForms.Configs;

namespace Shatus.WinForms.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureWritable<T>(this IServiceCollection services,
        IConfigurationSection section,
        string file = "appsettings.json") where T : class, new()
    {
        services.Configure<T>(section);
        services.AddTransient<IWritableOptions<T>>(provider =>
        {
            var options = provider.GetService<IOptionsMonitor<T>>();
            return new WritableOptions<T>(options, section.Key, file);
        });
    }
}