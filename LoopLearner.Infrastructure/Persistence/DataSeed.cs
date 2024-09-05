using LoopLearner.Application.Contracts.Services;
using LoopLearner.Domain.UserAggregate;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence;

public static class DataSeed
{
    public static void Seed(ModelBuilder modelBuilder, IPasswordHasher<User> passwordHasher, IDateTimeProvider dateTimeProvider)
    {
        var alincoln = User.CreateNew("Abraham", "Lincoln", "alincoln", "lincoln.abraham@example.com", "HonestAbe1865");
        var jcaesar = User.CreateNew("Julius", "Caesar", "jcaesar", "caesar.julius@example.com", "EtTuBrute44BC");
        var aeinstein = User.CreateNew("Albert", "Einstein", "aeinstein", "einstein.albert@example.com", "E=mc2Genius");
        var mcurie = User.CreateNew("Marie", "Curie", "mcurie", "curie.marie@example.com", "Radioactive1898");
        var ldavinci = User.CreateNew("Leonardo", "da Vinci", "ldavinci", "davinci.leonardo@example.com", "Renaissance1452");
        var wshakespeare = User.CreateNew("William", "Shakespeare", "wshakespeare", "shakespeare.william@example.com", "ToBeOrNotToBe1600");
        var ccleopatra = User.CreateNew("Cleopatra", "", "ccleopatra", "cleopatra@example.com", "QueenOfEgypt30BC");
        var aalexander = User.CreateNew("Alexander", "the Great", "aalexander", "alexander@example.com", "Conqueror356BC"); ;
        var ntesla = User.CreateNew("Nikola", "Tesla", "ntesla", "tesla.nikola@example.com", "ACPowerGenius1856");
        var wgenghis = User.CreateNew("Genghis", "Khan", "wgenghis", "genghis.khan@example.com", "MongolEmpire1162");
            
        var users = new[] { alincoln, jcaesar, aeinstein, mcurie, ldavinci, wshakespeare, ccleopatra, aalexander, ntesla, wgenghis };
        foreach (var user in users)
        {
            var hashedPassword = passwordHasher.HashPassword(user, user.Password);
            user.UpdatePassword(hashedPassword);
        }

        modelBuilder.Entity<User>().HasData(users);

    }
}