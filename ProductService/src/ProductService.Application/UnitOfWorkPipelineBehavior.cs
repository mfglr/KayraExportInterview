using MediatR;

namespace ProductService.Application
{
    internal class UnitOfWorkPipelineBehavior<TRequest, TResponse>(IUnitOfWork unitOfWork) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next(cancellationToken);
            await unitOfWork.CommitAsync(cancellationToken);
            return response;
        }
    }
}
