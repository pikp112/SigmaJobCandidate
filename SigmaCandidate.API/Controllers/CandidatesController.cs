using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SigmaCandidate.Core.Dtos;
using SigmaCandidate.Core.Models;
using SigmaCandidate.Infrastructure.Data;
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
        public async Task<IActionResult> CreateCandidate(CandidateDto candidate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingCandidate = await _unitOfWork.CandidateRepository.GetCandidateByEmailAsync(candidate.Email);

            if (existingCandidate != null)
                return Conflict("A candidate with the same email already exists.");

            var result = _mapper.Map<CandidateModel>(candidate);

            await _unitOfWork.CandidateRepository.AddAsync(result);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCandidateByEmail), new { email = candidate.Email }, candidate);
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