// ---------------------------------------------------------------
// Copyright (c) The Standard Community. All rights reserved.
// ---------------------------------------------------------------

using EFxceptions.Infrastructure.Build.Services;

namespace EFxceptions.Infrastructure.Build
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var scriptGenerationService = new ScriptGenerationService();

            scriptGenerationService.GenerateBuildScript(
                branchName: "main",
                projectName: "EFxceptions.Core",
                dotNetVersion: "10.x");

            scriptGenerationService.GeneratePrLintScript(branchName: "main");
        }
    }
}