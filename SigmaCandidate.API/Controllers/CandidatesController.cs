using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using SigmaCandidate.Core.Models;
using SigmaCandidate.Infrastructure.Data;

namespace SigmaCandidate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController(SigmaCandidateDbContext context) : ControllerBase
    {
        private readonly SigmaCandidateDbContext _context = context;

        [HttpPost]
        public async Task<IActionResult> CreateCandidate(CandidateModel candidate)
        {
        }

        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateCandidate(string email, CandidateModel candidate)
        {
        }

        [HttpPatch("{email}")]
        public async Task<IActionResult> UpdateCandidatePartial(string email, [FromBody] JsonPatchDocument<CandidateModel> patchDoc)
        {
        }
    }
}