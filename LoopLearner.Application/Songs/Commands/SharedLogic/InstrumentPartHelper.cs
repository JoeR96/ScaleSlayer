using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.SongAggregate.Entities;
using CSharpFunctionalExtensions;
using LoopLearner.Application.Songs.CreateSong;
using LoopLearner.Domain.Errors;
using LoopLearner.Domain.Errors.General;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate.ValueObjects;

namespace LoopLearner.Application.Songs.Commands.SharedLogic
{
    public static class InstrumentPartHelpers
    {
        public static async Task<Result<List<InstrumentPart>, Error>> ValidateCreateAndGetInstrumentPartsAsync(
            List<InstrumentPartCommand> instrumentPartCommands, 
            IInstrumentPartRepository instrumentPartRepository)
        {
            var instrumentParts = new List<InstrumentPart>();
            var providedInstrumentPartIds = new List<InstrumentPartId>();

            foreach (var instrumentPartCommand in instrumentPartCommands)
            {
                if (instrumentPartCommand.Id is not null)
                {
                    providedInstrumentPartIds.Add(InstrumentPartId.Create(instrumentPartCommand.Id.Value));
                }
                else if (!string.IsNullOrWhiteSpace(instrumentPartCommand.InstrumentName) && instrumentPartCommand.Tabs.Any())
                {
                    var newInstrumentPart = InstrumentPart.CreateNew(
                        instrumentPartCommand.InstrumentName,
                        instrumentPartCommand.Tabs
                    );
                    
                    instrumentParts.Add(newInstrumentPart);
                    
                }
                else
                {
                    return new ValidationError(
                        $"InstrumentPart[{instrumentPartCommands.IndexOf(instrumentPartCommand)}]", 
                        "Either Id or both InstrumentName and Notes must be provided for InstrumentPart");
                }
            }

            var instrumentPartsFromDb = await instrumentPartRepository.GetInstrumentPartsById(providedInstrumentPartIds);

            if (providedInstrumentPartIds.Count != instrumentPartsFromDb.Count())
            {
                var foundInstrumentPartIds = instrumentPartsFromDb.Select(ip => ip.Id).ToList();
                var notFoundInstrumentPartIds = providedInstrumentPartIds
                    .Where(id => !foundInstrumentPartIds.Contains(id))
                    .Select(id => id.Value)
                    .ToList();

                return new NotFoundError(
                    $"InstrumentParts with the following Ids were not found: {string.Join(", ", notFoundInstrumentPartIds)}");
            }

            instrumentParts.AddRange(instrumentPartsFromDb);
            return instrumentParts;
        }
    }
}
