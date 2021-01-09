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
                .Matches(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$");
                //.(@".*@.*[.][com,net,org,gov]");

            RuleFor(x => x.Age)
                .GreaterThanOrEqualTo(20)
                .LessThanOrEqualTo(60);
        }
    }
}