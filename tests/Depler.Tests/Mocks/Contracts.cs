using System.Collections.Generic;
using System.IO;
using Depler.Abstractions.Contracts;
using Depler.Lib.Contracts;
using Depler.Lib.IO;
using Nuke.Common.IO;

namespace depler.tests.Mocks;

internal static class Contracts
{
    public static AbsolutePath MockRootPath => Path.GetFullPath("./Mocks").ToAbsolutePath();

    public static Dictionary<string, PackageConsumer> Repo1Consumers =
        new()
        {
            {"project1-1", new PackageConsumer("project1-2", new Dictionary<string, Package>
            {
                {"package1", new Package("package1")}
            })}
        };
    
    public static Dictionary<string, PackageProducer> Repo1Producers =
        new()
        {
            {"project1-1", new PackageProducer("project1-2")}
        };

    public static Repository Repo1 =
        new Repository(
            new RepositoryIdentity("repo1", "https://mock.local"),
            MockRootPath / "repo1",
            Repo1Consumers,
            Repo1Producers);
        
    public static Repository[] Repositories = 
    {
        Repo1
    };
}
