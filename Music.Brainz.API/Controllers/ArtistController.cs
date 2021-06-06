using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Music.Brainz.Common.Domain.MusicBrainz.Enum;
using Music.Brainz.Common.Response;
using Music.Brainz.CQRS.Artist.Models;
using Music.Brainz.CQRS.Artist.Queries.GetArtist;
using Music.Brainz.CQRS.Artist.Queries.SearchArtists;

namespace Music.Brainz.API.Controllers
{
    public class ArtistController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(typeof(ArtistListModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ArtistListModel>> SearchArtist([FromQuery(Name = "query")][Required]string query, int limit = 10, int offSet = 0, CancellationToken cancellationToken = default)
        {
            // Create Mediator request
            var results = await Mediator.Send(new SearchArtistQuery { Query = query, Limit = limit, OffSet = offSet }, cancellationToken);
            
            // If there is only one match then return Artist with release collection
            if (results.IsSuccessfulResponse && ((ValueResponse<ArtistListModel>)results).Value?.Artists?.Count == 1)
            {
                var artist = ((ValueResponse<ArtistListModel>) results).Value.Artists.FirstOrDefault();
                return RedirectToAction("LookupArtist", new {id = artist?.Id});
            }

            // Transform response to Type
            return results.ToActionResult<ArtistListModel>();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ArtistModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ArtistModel>> LookupArtist([FromRoute][Required]string id, CancellationToken cancellationToken = default)
        {
            // Create Mediator request
            var results = await Mediator.Send(new LookUpArtistQuery { Id = id, Includes = new[] { IncEnum.Releases } }, cancellationToken);
            
            // Transform response to Type
            return results.ToActionResult<ArtistModel>();
        }
    }
}
