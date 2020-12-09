using AutoMapper;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeirdFlex.Api;
using WeirdFlex.Api.Models;
using WeirdFlex.Business.Interfaces;

namespace Tieto.Lama.PrintApi.Pages
{
    [Authorize(AuthenticationSchemes = OpenIdConnectDefaults.AuthenticationScheme, Policy = KnownPolicies.Flexer)]
    public abstract class CommonPageModel : PageModel
    {
        protected readonly IMediator Mediator;
        protected readonly IMapper Mapper;

        public CommonPageModel(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        protected IActionResult NotAllowed()
        {
            UpdateWarningText(text: $"{nameof(UnauthorizedAccessException)}: You are not authorized to perform this action.");
            return Page();
        }

        protected int? TryUpdatePageSize(string key, int? pageSize)
        {
            var keyed = string.Join(".", key, "PageSize");
            if (pageSize == null)
            {
                pageSize = HttpContext.Session.GetInt32(keyed);
            }
            if (pageSize != null)
            {
                HttpContext.Session.SetInt32(keyed, pageSize.Value);
            }
            return pageSize;
        }

        protected void UpdateWarningText(IResult? useCaseResult = null, Exception? exception = null, string? text = null)
        {
            if(useCaseResult != null)
            {
                ViewData["WarningText"] = "An error occured: " + JsonConvert.SerializeObject(useCaseResult);
            }
            else if(exception != null)
            {
                ViewData["WarningText"] = "An error occured: " + exception.Message;
            }
            else
            {
                ViewData["WarningText"] = text;
            }
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            UpdateWarningText();

            base.OnPageHandlerExecuting(context);
        }

        private async Task<TResponse?> HandleGet<TResponse>(IRequest<TResponse> request)
            where TResponse : class
        {
            TResponse? result = null;
            try
            {
                result = await Mediator.Send(request);
            }
            catch (Exception ex)
            {
                UpdateWarningText(exception: ex);
            }

            return result;
        }

        protected async Task<TItem?> HandleGetRequest<TResponse, TItem>(IRequest<TResponse> request, Func<TResponse, TItem>? map = null)
            where TResponse : class, IResult
            where TItem : class
        {
            var result = default(TItem?);
            var response = await HandleGet(request);

            if ((response?.IsSuccess).GetValueOrDefault() == false)
            {
                UpdateWarningText(useCaseResult: response);
            }
            else if ((response?.IsSuccess).GetValueOrDefault() == true && 
                map != null)
            {
                result = map(response!);
            }

            return result;
        }

        protected async Task<PaginatedList<TItem>?> HandlePaginatedGetRequest<TResponse, TItem>(IRequest<TResponse> request,
            Func<TResponse, IList<TItem>>? map = null)
            where TResponse : class, IPaginatedResult, IResult
            where TItem : class
        {
            var result = default(PaginatedList<TItem>?);
            var response = await HandleGet(request);

            if ((response?.IsSuccess).GetValueOrDefault() == false)
            {
                UpdateWarningText(useCaseResult: response);
            }
            else if ((response?.IsSuccess).GetValueOrDefault() == true &&
                map != null)
            {
                result = new PaginatedList<TItem>(map(response!), response?.PageInfo);
            }

            return result;
        }

        protected async Task<bool> HandlePostRequest<TResponse>(IRequest<TResponse> request, bool validate = true)
             where TResponse : class, IResult
        {
            var response = default(TResponse?);
            if(!validate || ModelState.IsValid)
            {
                response = await HandleGet(request);
            }

            if ((response?.IsSuccess).GetValueOrDefault() == false)
            {
                UpdateWarningText(useCaseResult: response);
            }

            return (response?.IsSuccess).GetValueOrDefault();
        }
    }
}
