using Hahn.ApplicatonProcess.December2020.Data.Contracts;
using Hahn.ApplicatonProcess.December2020.Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Infrastructure
{
    public class ApplicantsRepository : IApplicantsRepository
    {
        private readonly ApplicantsDbContext _dbContext;
        private readonly ILogger<ApplicantsRepository> _logger;

        public ApplicantsRepository(ApplicantsDbContext dbContext,ILogger<ApplicantsRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<int> CreateApplicant(Applicant applicant)
        {
            _dbContext.Applicants.Add(applicant);
            await _dbContext.SaveChangesAsync();
            return applicant.ID;
        }

        public async Task<Applicant> DeleteApplicant(int id)
        {
            var applicant = await _dbContext.Applicants.FindAsync(id);
            if (applicant is null) {
                _logger.LogInformation("Can`t find applicant to delete");
                return null;
            }

            _dbContext.Applicants.Remove(applicant);
            await _dbContext.SaveChangesAsync();
            return applicant;
        }

        public Task<Applicant> GetApplicant(int id)
        {
            return _dbContext.Applicants.AsNoTracking().FirstOrDefaultAsync(a=>a.ID==id);
        }

        public async Task<Applicant> UpdateApplicant(int id, Applicant applicant)
        {
            if (await GetApplicant(id) != null)
            {
                _dbContext.Entry(applicant).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return applicant;
            }
            _logger.LogInformation("Update applicant Failed");
            return null;
        }
    }
}