using BingePop.Data;
using BingePop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingePop.Services
{
    public class FavoriteService
    {
        private readonly Guid _userId; // private field

        public FavoriteService(Guid userId) // constructor
        {
            _userId = userId;
        }

        public bool CreateFavorite(FavoriteCreate model)
        {
            var entity =
                new Favorite()
                {
                    OwnerId = _userId,
                    Title = model.Title,
                    Content = model.Content,
                    ContentType = model.ContentType,
                    CreatedUtc = DateTimeOffset.Now,
                    //CategoryId = model.CategoryId
                };

            using (var ctx = new ApplicationDbContext())    // allows us to close the connection to the database right here
            // when the DbContext is connected and we will be using it for something
            {
                ctx.Favorites.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<FavoriteListItem> GetFavorites()
        {
            using (var ctx = new ApplicationDbContext())
            {
                // var ownerIdGuid = Guid.Parse(ownerId);
                var query =
                    ctx
                    .Favorites
                    // .DefaultIfEmpty()
                    .Where(e => e.OwnerId == _userId) // Where(note => note.OwnerId == ownerIdGuid);
                    .Select(
                        e =>
                            new FavoriteListItem
                            {
                                ContentId = e.ContentId,
                                Title = e.Title,
                                ContentType = e.ContentType,
                                CreatedUtc = e.CreatedUtc
                            }
                            );
                return query.ToArray();
            }
        }

        public FavoriteDetail GetFavoriteById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                // var query = ctx.Notes.FirstorDefault(note => note.NoteId == noteId && note.OwnerId == ownerIdGuid);
                var entity =
                    ctx
                    .Favorites
                    .Single(e => e.ContentId == id && e.OwnerId == _userId);
                return
                    new FavoriteDetail
                    {
                        ContentId = entity.ContentId,
                        Title = entity.Title,
                        Content = entity.Content,
                        ContentType = entity.ContentType,
                        CreatedUtc = entity.CreatedUtc,
                        ModifiedUtc = entity.ModifiedUtc
                    };
            }
        }

        public bool UpdateFavorite(FavoriteEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Favorites
                    .Single(e => e.ContentId == model.ContentId && e.OwnerId == _userId);

                entity.Title = model.Title;
                entity.Content = model.Content;
                entity.ContentType = model.ContentType;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteFavorite(int contentId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Favorites
                    .Single(e => e.ContentId == contentId && e.OwnerId == _userId);
                ctx.Favorites.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}

