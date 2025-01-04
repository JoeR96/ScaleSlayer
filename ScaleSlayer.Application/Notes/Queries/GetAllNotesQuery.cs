using CSharpFunctionalExtensions;
using ScaleSlayer.Application.Scales.Responses;

namespace ScaleSlayer.Application.Notes.Queries;

public record GetAllNotesQuery : IRequest<Result<GetAllNotesResponse>>;
