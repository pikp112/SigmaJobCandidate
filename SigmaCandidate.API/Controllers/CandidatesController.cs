using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SigmaCandidate.Core.Dtos;
using SigmaCandidate.Core.Models;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;

namespace SigmaCandidate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<IActionResult> CreateCandidates([FromBody] IEnumerable<CandidateDto> candidates)
        {
            if (candidates == null || !candidates.Any())
                return BadRequest("No candidate data provided.");

            foreach (var candidate in candidates)
                if (!TryValidateModel(candidate))
                    return BadRequest(ModelState);

            var existingEmails = await _unitOfWork.CandidateRepository.GetExistingEmailsAsync(candidates.Select(c => c.Email));

            if (existingEmails.Any())
                return Conflict("One or more candidates with the same email already exist.");

            var candidateModels = _mapper.Map<IEnumerable<CandidateModel>>(candidates);

            await _unitOfWork.CandidateRepository.AddRangeAsync(candidateModels);

            return CreatedAtAction(nameof(GetCandidateByEmail), new { email = candidates.First().Email }, candidates);
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> GetCandidateByEmail(string email)
        {
            var candidate = await _unitOfWork.CandidateRepository.GetCandidateByEmailAsync(email);

            if (candidate == null)
                return NotFound();

            return Ok(candidate);
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateCandidate(string email, CandidateDto candidate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCandidate = await _unitOfWork.CandidateRepository.GetCandidateByEmailAsync(email);

            if (existingCandidate == null)
                return NotFound();

            _mapper.Map(candidate, existingCandidate);

            await _unitOfWork.CandidateRepository.UpdateAsync(candidate.Email, existingCandidate);

            return NoContent();
        }

        [HttpPatch("{email}")]
        public async Task<IActionResult> UpdateCandidatePartial(string email, JsonPatchDocument<CandidateDto> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var existingCandidate = await _unitOfWork.CandidateRepository.GetCandidateByEmailAsync(email);

            if (existingCandidate == null)
                return NotFound();

            var candidateToPatch = _mapper.Map<CandidateDto>(existingCandidate);

            patchDoc.ApplyTo(candidateToPatch, ModelState);

            if (!TryValidateModel(candidateToPatch))
                return BadRequest(ModelState);

            _mapper.Map(candidateToPatch, existingCandidate);

            await _unitOfWork.CandidateRepository.UpdateAsync(existingCandidate.Email, existingCandidate);

            return NoContent();
        }
    }
}