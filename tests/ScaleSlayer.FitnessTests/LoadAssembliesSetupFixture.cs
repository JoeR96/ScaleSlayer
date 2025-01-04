using System.Reflection;

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
}