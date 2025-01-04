using System.Net.Mime;
using System.Reflection;
using LoopLearner.Application.Notes.Queries;
using LoopLearner.Domain.ScaleAggregate;
using LoopLearner.Infrastructure.Persistence;

namespace ScaleSlayer.FitnessTests;

[SetUpFixture]
public class LoadAssembliesSetupFixture
{
    [OneTimeSetUp]
    public void LoadAllReferencedAssemblies()
    {
        LoadAllReferencedAssemblies(GetType().Assembly);
    }

    private static void LoadAllReferencedAssemblies(Assembly assembly)
    {
        foreach (var assemblyName in assembly.GetReferencedAssemblies()
                     .Where(n => n.FullName.StartsWith("LoopLearner")))
        {
            if (AppDomain.CurrentDomain.GetAssemblies().All(a => a.FullName != assemblyName.FullName))
            {
                LoadAllReferencedAssemblies(Assembly.Load(assemblyName));
            }
        }
    }

    private Type PresentationType = typeof(Program);
    private Type InfrastructureType = typeof(LoopLearnerDbContext);
    private Type ApplicationType = typeof(GetAllNotesQuery);
    private Type DomainType = typeof(Scale);
}