using WebApplicat.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


public class BlogPost
{
    public BlogPost()
    {
        this.Comments = new HashSet<Comment>();
    }
    public int Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTimeOffset? Updated { get; set; }

    [Required]
    public string Title { get; set; }
    [Required]
    [AllowHtml]
    public string Content { get; set; }
    public string Slug { get; set; }
    public string Body { get; set; }

    public string MediaURL { get; set; }
    public bool Published { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
 }
