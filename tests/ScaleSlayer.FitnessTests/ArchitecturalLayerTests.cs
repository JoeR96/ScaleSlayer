using NetArchTest.Rules;
using NetArchTest.Rules.Policies;
using ScaleSlayer.FitnessTests.Extensions;

namespace ScaleSlayer.FitnessTests;

public class ArchitecturalLayerTests
{
    [Test]
    public void ArchitectureLayerTest()
    {
        var policy = Policy.Define("Architectural Layers Policy", "The architectural layers must be adhered to.")
            .For(Types.InCurrentDomain)
            .Add(t => t
                .That()
                .ResideInNamespace("LoopLearner.Domain")
                .ShouldNot()
                .HaveDependencyOn("LoopLearner.Web.Server")
                .Or()
                .HaveDependencyOn("LoopLearner.Infrastructure")
                .Or()
                .HaveDependencyOn("LoopLearner.Application"),
                "Domain layer must not depend on presentation, infrastructure or application.",
                "Domain types should not depend on presentation, infrastructure or application.")
            
            .Add(t => t
                .That()
                .ResideInNamespace("LoopLearner.Application")
                .ShouldNot()
                .HaveDependencyOn("LoopLearner.Web.Server")
                .Or()
                .HaveDependencyOn("LoopLearner.Infrastructure"),
                "Application layer must not depend on presentation, infrastructure layer.",
                "Application types should not depend on presentation, infrastructure.")
            
            .Add(t => t
                .That()
                .ResideInNamespace("LoopLearner.Infrastructure")
                .ShouldNot()
                .HaveDependencyOn("LoopLearner.Web.Server"),
                "Infrastructure layer must not depend on presentation layer.",
                "Infrastructure types should not depend on presentation.");

        policy
            .Evaluate()
            .AssertIsSuccessful();
    }
}