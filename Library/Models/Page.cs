﻿namespace Library.Models;

public class Page<T>
{
    public int CurrentPage { get; set; }

    public int PageSize { get; set; }

    public int TotalItems { get; set; }

    public int TotalPage => (int)Math.Ceiling((decimal)TotalItems / PageSize);

    public IList<T> Content { get; set; } = new List<T>();
}