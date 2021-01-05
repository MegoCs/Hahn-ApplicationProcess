using Hahn.ApplicatonProcess.December2020.Data.Contracts;
using Hahn.ApplicatonProcess.December2020.Domain.Models;

using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private readonly IApplicantsRepository _applicantsRepository;
        public ApplicantsController(IApplicantsRepository applicantsRepository)
        {
            _applicantsRepository = applicantsRepository;
        }

        // GET: api/Applicants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Applicant>> GetApplicant(int id)
        {
            var applicant = await _applicantsRepository.GetApplicant(id);

            if (applicant == null)
            {
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
            return CreatedAtAction("GetApplicant", new { id = applicant.ID }, applicant);
        }

        // DELETE: api/Applicants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplicant(int id)
        {
            var applicant = await _applicantsRepository.DeleteApplicant(id);
            if (applicant == null)
                return NotFound();
            return NoContent();
        }
    }
}
