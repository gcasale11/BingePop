using BingePop.Data;
using BingePop.Models;
using BingePop.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace BingePop.WebAPI.Controllers
{
    [Authorize]
    public class FavoriteController : ApiController
    {
        public IHttpActionResult Post(FavoriteCreate favorite)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFavoriteService();

            if (!service.CreateFavorite(favorite))
                return InternalServerError();

            // a link or Id to the object just created, in this case, notes
            return Ok();
        }

        public IHttpActionResult Get()
        {
            FavoriteService favoriteService = CreateFavoriteService();
            var favorites = favoriteService.GetFavorites();
            List<Favorite> list = (List<Favorite>)favorites;

            return Ok(list);
        }

        private FavoriteService CreateFavoriteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var favoriteService = new FavoriteService(userId);
            return favoriteService;
        }


        public IHttpActionResult Get(int id)
        {
            FavoriteService favoriteService = CreateFavoriteService();
            var favorite = favoriteService.GetFavoriteById(id);
            return Ok(favorite);
        }

        public IHttpActionResult Put(FavoriteEdit favorite)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateFavoriteService();

            if (!service.UpdateFavorite(favorite))
                return InternalServerError();

            return Ok();
        }

        public IHttpActionResult Delete(int id)
        {
            var service = CreateFavoriteService();

            if (!service.DeleteFavorite(id))
                return InternalServerError();
            return Ok();
        }
    }
}
