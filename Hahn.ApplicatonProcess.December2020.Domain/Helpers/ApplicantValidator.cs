using FluentValidation;

using Hahn.ApplicatonProcess.December2020.Domain.Models;

using System.Net.Http;

namespace Hahn.ApplicatonProcess.December2020.Domain.Helpers
{
    public class ApplicantValidator : AbstractValidator<Applicant>
    {
        public ApplicantValidator()
        {
            RuleFor(x => x.Name)
                .MinimumLength(5);

            RuleFor(x => x.FamilyName)
                .MinimumLength(5);

            RuleFor(x => x.Address)
                .MinimumLength(10);

            RuleFor(x => x.CountryOfOrigin)
                .CustomAsync(async (country, context, cancelToken) =>
                {
                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync($"https://restcountries.eu/rest/v2/name/{country}", cancelToken);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                        context.AddFailure($"{country} is Invalid Country");
                    client.Dispose();
                });

            RuleFor(x => x.EmailAdress)
                .NotEmpty()
                .EmailAddress()
                .Matches(@".*@.*\.(com|net|org|gov)$");

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(20)
                .LessThanOrEqualTo(60);
        }
    }
}