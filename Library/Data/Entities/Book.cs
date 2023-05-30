﻿namespace Library.Data.Entities;

public class Book
{
    public int Id { get; set; }

    public string Author { get; set; } = string.Empty;

    public string? Description { get; set; } 

    public int Likes { get; set; }
    
    
}