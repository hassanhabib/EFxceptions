// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV3s;

var githubPipeLine = new GithubPipeline
{
    Name = "EFxceptions Build Pipeline",

    OnEvents = new Events
    {
        PullRequest = new PullRequestEvent
        {
            Branches = new string[] { "master" }
        },
        Push = new PushEvent
        {
            Branches = new string[] { "master" }
        }
    },

    Jobs = new Dictionary<string, Job>
    {
        {
            "build",
            new Job
            {
                RunsOn = BuildMachines.UbuntuLatest,

                Steps = new List<GithubTask>
                {
                    new CheckoutTaskV3
                    {
                        Name = "Check out"
                    },

                    new SetupDotNetTaskV3
                    {
                        Name = "Setup .Net",

                        With = new TargetDotNetVersionV3
                        {
                            DotNetVersion = "9.0.101"
                        }
                    },

                    new RestoreTask
                    {
                        Name = "Restore"
                    },

                    new DotNetBuildTask
                    {
                        Name = "Build"
                    },

                    new TestTask
                    {
                        Name = "Test"
                    }
                }
            }
        }
    }
};

var adotNetClient = new ADotNetClient();

string buildScriptPath = "../../../../.github/workflows/dotnet.yml";
string directoryPath = Path.GetDirectoryName(buildScriptPath);

if (!Directory.Exists(directoryPath))
{
    Directory.CreateDirectory(directoryPath);
}

adotNetClient.SerializeAndWriteToFile(adoPipeline: githubPipeLine, path: buildScriptPath);

