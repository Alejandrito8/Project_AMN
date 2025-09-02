using System;
using Project_AMN.Data;
using Project_AMN.Models;
using Microsoft.EntityFrameworkCore;
namespace Project_AMN.Models;

public class Article
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SKU { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string Location { get; set; } = string.Empty;
}