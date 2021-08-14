﻿namespace CleanArchitecture.Common.Behaviours
{
    using System.Threading;
    using System.Threading.Tasks;
    using CleanArchitecture.Common.Interfaces;
    using MediatR.Pipeline;

    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ICurrentUserService currentUserService;
        private readonly IIdentityService identityService;

        public LoggingBehaviour(ICurrentUserService currentUserService, IIdentityService identityService)
        {
            this.identityService = identityService;
            this.currentUserService = currentUserService;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = this.currentUserService.UserId ?? string.Empty;
            var userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await this.identityService.GetUserNameAsync(userId);
            }

            Logging.Logging.LogInfo($"CleanArchitecture Request: {requestName} {userId} {userName}", request);
        }
    }
}