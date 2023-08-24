﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TResponse : class, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }
            var context = new ValidationContext<TRequest>(request);

            var errorDictionary = _validators.Select(x => x.Validate(context)).SelectMany(x => x.Errors).Where(x => x != null).GroupBy(
                s => s.PropertyName,
                s => s.ErrorMessage, (propertyName, errorMessage) => new
                {
                    Key = propertyName,
                    Values = errorMessage.Distinct().ToArray()
                }
                ).ToDictionary(x => x.Key, x => x.Values[0]);
            if (errorDictionary.Any())
            {
                var errors = errorDictionary.Select(x => new ValidationFailure
                {
                    PropertyName = x.Value,
                    ErrorCode = x.Key
                });
                throw new ValidationException(errors);
            }
            return await next();
        }
    }
}
