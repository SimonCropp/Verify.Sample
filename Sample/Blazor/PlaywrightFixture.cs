﻿using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using PlaywrightSharp;
using PlaywrightSharp.Chromium;
using VerifyTests;
using Xunit;

public class PlaywrightFixture :
    IAsyncLifetime
{
    IPlaywright? playwright;
    Process? process;
    IChromiumBrowser? browser;

    public async Task InitializeAsync()
    {
        StartBlazorApp();
        playwright = await Playwright.CreateAsync();

        browser = await playwright.Chromium.LaunchAsync();

        Page = await browser.NewPageAsync();
        Page.ViewportSize.Height = 768;
        Page.ViewportSize.Width = 1024;
    }

    public IPage Page { get; private set; } = null!;

    void StartBlazorApp()
    {
        var projectDir = Path.Combine(AttributeReader.GetSolutionDirectory(), "BlazorWebApp");
        ProcessStartInfo startInfo = new("dotnet", "run")
        {
            WorkingDirectory = projectDir
        };
        process = Process.Start(startInfo);
    }

    public async Task DisposeAsync()
    {
        if (browser != null)
        {
            await browser.DisposeAsync();
        }

        playwright?.Dispose();

        if (process != null)
        {
            process.Kill();
            process.Dispose();
        }
    }
}