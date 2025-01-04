using NetArchTest.Rules.Policies;

namespace ScaleSlayer.FitnessTests.Extensions;

public static class ResultsExtensions
{
    public static void AssertIsSuccessful(this PolicyResults policyResults)
    {
        if (!policyResults.HasViolations)
        {
            return;
        }
        
        throw new AssertionException(
            $"The policy '{policyResults.Name}' has violations:{Environment.NewLine}{Environment.NewLine}{policyResults.GetExceptionMessage()}");
    }

    public static string GetExceptionMessage(this PolicyResults policyResults)
    {
        return string
            .Join(
                $"{Environment.NewLine}{Environment.NewLine}",
                policyResults.Results
                    .Where(r => r.FailingTypes != null)
                    .Select(r => $"Rule: {r.Name}{Environment.NewLine}Violating types: {Environment.NewLine}{string.Join(Environment.NewLine, r.FailingTypes.Select(t => t.FullName))}"));
    }
}