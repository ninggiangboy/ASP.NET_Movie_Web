using System.Linq.Dynamic.Core;
using Group06_Project.Domain.Entities;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Group06_Project.Infrastructure.Data.Repositories;

public class CommentRepository : RepositoryBase<Comment, int>, ICommentRepository
{
    public CommentRepository(ApplicationDbContext appDbContext) : base(appDbContext)
    {
    }

    public void RemoveById(int commentId)
    {
        var comment = new Comment { Id = commentId };
        DbSet.Remove(comment);
    }

    public Page<CommentItem> GetByFilmId(int filmId, PageRequest<Comment> pageRequest)
    {
        var rawData = DbSet
            .Include(c => c.User)
            .Where(c => c.FilmId == filmId);
        var total = rawData.Count();
        var data = rawData
            .OrderBy(pageRequest.Sort ?? "Time Desc")
            .Skip((pageRequest.PageNumber - 1) * pageRequest.Size).Take(pageRequest.Size)
            .Select(c => new CommentItem
            {
                Content = c.Content,
                Time = c.Time,
                UserName = c.User.UserName
            });
        return new Page<CommentItem>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = total,
            Data = total == 0 ? new List<CommentItem>() : data.ToList(),
            Sort = pageRequest.Sort
        };
    }

    public void UpdateComment(int commentId, string newContent)
    {
        var comment = DbSet.Find(commentId);
        if (comment != null)
        {
            comment.Content = newContent;
            DbSet.Update(comment);
        }
    }

    public Page<CommentList> GetAllComments(PageRequest<Comment> pageRequest)
    {
        var rawData = DbSet
            .Include(c => c.User)
            .OrderBy(pageRequest.Sort ?? "Time Desc")
            .Skip((pageRequest.PageNumber - 1) * pageRequest.Size)
            .Take(pageRequest.Size)
            .Select(c => new CommentList
            {
                Id = c.Id,
                UserId = c.UserId,
                FilmId = c.FilmId,
                Content = c.Content,
                Time = c.Time
            });

        var total = DbSet.Count();

        return new Page<CommentList>
        {
            PageNumber = pageRequest.PageNumber,
            PageSize = pageRequest.Size,
            TotalElement = total,
            Data = rawData.ToList(),
            Sort = pageRequest.Sort
        };
    }
	public Page<CommentList> GetAllCommentsByAsc(PageRequest<Comment> pageRequest)
	{
		var rawData = DbSet
			.Include(c => c.User)
			.OrderBy(pageRequest.Sort ?? "Time Asc")
			.Skip((pageRequest.PageNumber - 1) * pageRequest.Size)
			.Take(pageRequest.Size)
			.Select(c => new CommentList
			{
				Id = c.Id,
				UserId = c.UserId,
				FilmId = c.FilmId,
				Content = c.Content,
				Time = c.Time
			});

		var total = DbSet.Count();

		return new Page<CommentList>
		{
			PageNumber = pageRequest.PageNumber,
			PageSize = pageRequest.Size,
			TotalElement = total,
			Data = rawData.ToList(),
			Sort = pageRequest.Sort
		};
	}
	public Page<CommentList> SearchComments(string searchTerm, PageRequest<Comment> pageRequest)
	{
		var rawData = DbSet
			.Include(c => c.User)
			.Where(c => c.Content.Contains(searchTerm));

		var total = rawData.Count();

		var data = rawData
			.OrderBy(pageRequest.Sort ?? "Time Desc")
			.Skip((pageRequest.PageNumber - 1) * pageRequest.Size)
			.Take(pageRequest.Size)
			.Select(c => new CommentList
			{
				Id = c.Id,
				UserId = c.UserId,
				FilmId = c.FilmId,
				Content = c.Content,
				Time = c.Time
			});

		return new Page<CommentList>
		{
			PageNumber = pageRequest.PageNumber,
			PageSize = pageRequest.Size,
			TotalElement = total,
			Data = total == 0 ? new List<CommentList>() : data.ToList(),
			Sort = pageRequest.Sort
		};
	}
	public Page<CommentList> SearchCommentsByAsc(string searchTerm, PageRequest<Comment> pageRequest)
	{
		var rawData = DbSet
			.Include(c => c.User)
			.Where(c => c.Content.Contains(searchTerm));

		var total = rawData.Count();

		var data = rawData
			.OrderBy(pageRequest.Sort ?? "Time Asc")
			.Skip((pageRequest.PageNumber - 1) * pageRequest.Size)
			.Take(pageRequest.Size)
			.Select(c => new CommentList
			{
				Id = c.Id,
				UserId = c.UserId,
				FilmId = c.FilmId,
				Content = c.Content,
				Time = c.Time
			});

		return new Page<CommentList>
		{
			PageNumber = pageRequest.PageNumber,
			PageSize = pageRequest.Size,
			TotalElement = total,
			Data = total == 0 ? new List<CommentList>() : data.ToList(),
			Sort = pageRequest.Sort
		};
	}



}