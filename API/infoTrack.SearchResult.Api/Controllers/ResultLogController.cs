using infoTrack.SearchResult.Core.Domain;
using infoTrack.SearchResult.Core.Dto;
using infoTrack.SearchResult.Services.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace infoTrack.SearchResult.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultLogController : ControllerBase
    {

        private readonly IResultLogService _resultService;
        public ResultLogController(IResultLogService resultService)
        {
            _resultService = resultService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupedResultDto>>> GetAllResults([FromQuery(Name = "offset")] int skip = 0, [FromQuery(Name = "returnCount")] int take = 10)
        {
            if (take > 100)
            {
                return BadRequest("Maximum allowed to return is 100");
            }

            var items = await _resultService.GetSearchResults(skip, take);

            return Ok(GroupResults(items));
        }

        [HttpPost]
        public async Task<ActionResult> AddResults([FromBody] List<ResultLogDto> results)
        {
            //if there was auth, get username from auth mechanism and pass it to mapper, hardcoding "testUser" for now
            var items = results.Select(x => MapFromDto(x, "testUser"));
            await _resultService.AddResults(items);
            return Ok();
        }

        private ResultLogDto MapDto(ResultLog item)
        {
            ResultLogDto resultLogDto = new ResultLogDto();
            resultLogDto.SearchPhrase = item.SearchPhrase;
            resultLogDto.SearchSite = item.SearchSite;
            resultLogDto.MatchingUrl = item.MatchingUrl;
            resultLogDto.ResultRank = item.ResultRank;
            resultLogDto.SearchTimeStamp = item.SearchTimeStamp;

            return resultLogDto;
        }

        private ResultLog MapFromDto(ResultLogDto dto, string userName)
        {
            ResultLog result = new ResultLog();
            result.SearchPhrase = dto.SearchPhrase;
            result.SearchSite = dto.SearchSite;
            result.MatchingUrl = dto.MatchingUrl;
            result.ResultRank = dto.ResultRank;
            result.SearchTimeStamp = dto.SearchTimeStamp;
            result.CreatedBy = userName;

            return result;
        }

        private List<GroupedResultDto> GroupResults(IEnumerable<ResultLog> items)
        {
            List<GroupedResultDto> ret = new List<GroupedResultDto>();

            //Temu: had to do this part last minute, could be improved to be done at the data layer, also this grouping makes skip and take useless
            var grouped = items.GroupBy(x => new { x.SearchTimeStamp, x.SearchPhrase, x.MatchingUrl, x.SearchSite });

            foreach (var group in grouped)
            {
                var dto = new GroupedResultDto();
                dto.MatchingUrl = group.Key.MatchingUrl;
                dto.SearchTimeStamp = group.Key.SearchTimeStamp;
                dto.SearchPhrase = group.Key.SearchPhrase;
                dto.SearchSite = group.Key.SearchSite;
                List<int> resultRanks = new List<int>();
                foreach (var item in group)
                {
                    resultRanks.Add(item.ResultRank);
                }
                dto.ResultRank = resultRanks;
                ret.Add(dto);
            }

            return ret;
        }
    }
}
