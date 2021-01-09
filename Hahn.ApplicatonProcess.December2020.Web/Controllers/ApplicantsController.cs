using Hahn.ApplicatonProcess.December2020.Data.Contracts;
using Hahn.ApplicatonProcess.December2020.Domain.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantsRepository _applicantsRepository;
        private readonly ILogger<ApplicantsController> _logger;

        public ApplicantsController(IApplicantsRepository applicantsRepository, ILogger<ApplicantsController> logger)
        {
            _applicantsRepository = applicantsRepository;
            _logger = logger;
        }

        // GET: api/Applicants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Applicant>> GetApplicant(int id)
        {
            var applicant = await _applicantsRepository.GetApplicant(id);

            if (applicant == null)
            {
                _logger.LogWarning($"Applicant {id} wasn`t found");
                return NotFound();
            }

            return applicant;
        }

        // PUT: api/Applicants/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplicant(int id, Applicant applicant)
        {
            if (id != applicant.ID)
            {
                _logger.LogWarning($"Applicant {id} wasn`t found");
                return BadRequest();
            }

            await _applicantsRepository.UpdateApplicant(id, applicant);

            return NoContent();
        }

        // POST: api/Applicants
        [HttpPost]
        public async Task<ActionResult<Applicant>> PostApplicant(Applicant applicant)
        {
            await _applicantsRepository.CreateApplicant(applicant);

            _logger.LogInformation($"Applicant {applicant.ID} was created");

            return CreatedAtAction("GetApplicant", new { id = applicant.ID }, applicant);
        }

        // DELETE: api/Applicants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            var applicant = await _applicantsRepository.DeleteApplicant(id);
            if (applicant == null)
            {
                _logger.LogWarning($"Applicant {id} wasn`t found");
                return NotFound();
            }


            _logger.LogInformation($"Applicant {id} was deleted");
            return NoContent();
        }
    }
}
