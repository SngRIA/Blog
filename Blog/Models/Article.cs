using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
    public class Article
    {
        [ScaffoldColumn(false), HiddenInput(DisplayValue = false), Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, Display(Name = "Заголовок статьи")]
        public string Title { get; set; }
        [Required, Display(Name = "Описание статьи")]
        public string Description { get; set; }
        [Required, Display(Name = "Текст статьи")]
        public string Text { get; set; }
        [ScaffoldColumn(false), HiddenInput(DisplayValue = false)]
        public int Views { get; set; }
        [ScaffoldColumn(false), Required]
        public DateTime Date { get; set; }
    }

    public class PageInfo
    {
        public int CurrentPage { get; set; }
        public int CountOfArticles { get; set; }
        public int MaxArticlesOnPage { get; set; }
        public int NumberOfPages {
            get => (int)Math.Ceiling((decimal)CountOfArticles / MaxArticlesOnPage);
                }
    }

    public class ArticlesView
    {
        public IEnumerable<Article> Articles { get; set; }
        public PageInfo Page { get; set; }
    }
}