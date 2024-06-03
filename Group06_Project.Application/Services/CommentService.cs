using System;
using Group06_Project.Domain.Interfaces.Services;
using Group06_Project.Domain.Models;
using Group06_Project.Domain.Interfaces.Repositories;
using Group06_Project.Domain.Entities;

namespace Group06_Project.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IFilmRepository _filmRepository;

    public CommentService(ICommentRepository commentRepository, IFilmRepository filmRepository)
    {
        _commentRepository = commentRepository;
        _filmRepository = filmRepository;
    }

    public void AddCommentToFilm(int filmId, string commentText, string userId)
    {
        var film = _filmRepository.GetById(filmId);
        if(film == null) 
        {
            throw new Exception("Film not found");
        }
        var comment = new Comment
        {
            FilmId = filmId,
            Content = commentText,
            UserId = userId,
            Time = DateTime.UtcNow
        };
    }

    public void RemoveComment(int commentId)
    {
        var comment = _commentRepository.GetById(commentId);
        if(comment == null) 
        {
            throw new Exception("Comment not found");
        }
        _commentRepository.Remove(comment);
    }
}