﻿using System.Runtime.CompilerServices;
using VerifyTests;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifyXaml.Enable();

        VerifyImageMagick.RegisterComparers(.02);
    }
}