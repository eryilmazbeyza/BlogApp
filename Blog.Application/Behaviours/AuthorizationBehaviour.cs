using Blog.Application.Common.Exceptions;
using Blog.Application.Common.Interfaces;
using Blog.Application.Common.Models;
using Blog.Application.Common.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System.Reflection;


namespace Blog.Application.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IPipelineManagerService _pipelineManagerService;

    public AuthorizationBehaviour(ICurrentUserService currentUserService, IPipelineManagerService pipelineManagerService)
    {
        _currentUserService = currentUserService;
        _pipelineManagerService = pipelineManagerService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_pipelineManagerService.IsSkipPipeline(typeof(AuthorizationBehaviour<,>)))
            return await next();

        var allowAnonymousAttr = request.GetType().GetCustomAttribute<Common.Security.AllowAnonymousAttribute>();

        if (allowAnonymousAttr != null)
            return await next();

        if (_currentUserService.UserId <= 0)
            throw new ForbiddenAccessException(ResultMessages.User.UnauthenticatedUser);

        return await next();
    }
}
