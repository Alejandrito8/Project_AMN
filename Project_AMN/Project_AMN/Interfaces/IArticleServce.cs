using System.Collections.Generic;
using System.Threading.Tasks;
using Project_AMN.Models;

namespace Project_AMN.Interfaces;

public interface IArticleService
{
    Task<IEnumerable<Article>> GetAllArticlesAsync();
    Task<Article?> GetArticleByIdAsync(int id);
    Task AddArticleAsync(Article article);
    Task UpdateArticleAsync(Article article);
    Task DeleteArticleAsync(int id);
}