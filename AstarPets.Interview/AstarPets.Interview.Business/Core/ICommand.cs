namespace AstarPets.Interview.Business.Core
{
    public interface ICommand<TRequest, TResponse> : IWorkflow<TRequest, TResponse>
    {
    }
}