using AutoMapper;
using SigmaCandidate.Core.Dtos;
using SigmaCandidate.Core.Models;

namespace SigmaCandidate.API.Models
{
    public class MappingCandidates : Profile
    {
        public MappingCandidates()
        {
            CreateMap<CandidateDto, CandidateModel>().ReverseMap();
        }
    }
}