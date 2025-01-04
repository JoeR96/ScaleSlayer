namespace LoopLearner.Domain.Errors.General;

public class TaskCancelledError : BadRequestError
{
    public TaskCancelledError(
        string? errorMessage = null) : base(nameof(TaskCancelledError), errorMessage: errorMessage)
    {

    }
}