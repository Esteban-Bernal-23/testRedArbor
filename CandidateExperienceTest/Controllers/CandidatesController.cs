using CandidateExperienceTest.Commands;
using CandidateExperienceTest.Entities;
using CandidateExperienceTest.Middleware;
using CandidateExperienceTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace CandidateExperienceTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly CandidateCommandService _candidateCommandService;
        private readonly CandidateQueryService _candidateQueryService;

        public CandidatesController(CandidateCommandService candidateCommandService, CandidateQueryService candidateQueryService)
        {
            _candidateCommandService = candidateCommandService;
            _candidateQueryService = candidateQueryService;
        }

        [HttpGet]
        [Route("allCandidates")]
        [RequiredApiKey]
        public async Task<ActionResult<IEnumerable<Candidate>>> GetCandidates()
        {
            try
            {
                var candidates = await _candidateQueryService.GetAllCandidates();
                return Ok(candidates);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            
        }

        [HttpPost]
        [Route("insert")]
        [RequiredApiKey]
        public async Task<ActionResult<Candidate>> CreateCandidate(CreateCandidateCommand command)
        {
            try
            {
                var candidate = await _candidateCommandService.CandidateCreate(command);
                return CreatedAtAction(nameof(GetCandidates), new { id = candidate.IdCandidate }, candidate);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Route("update")]
        [RequiredApiKey]
        public async Task<IActionResult> UpdateCandidate(UpdateCandidateCommand command)
        {
            try
            {
                var candidate = await _candidateCommandService.CandidateUpdate(command);

                if (candidate == null)
                {
                    return NotFound();
                }

                return Ok("Candidate Updated");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [RequiredApiKey]
        public async Task<IActionResult> DeleteCandidate(int id)
        {
            try
            {
                var command = new DeleteCandidateCommand { IdCandidate = id };
                var result = await _candidateCommandService.CandidateDelete(command);

                if (!result)
                {
                    return NotFound();
                }

                return Ok("Candidate Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
