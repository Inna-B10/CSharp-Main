using System;
using Core.Interfaces;

namespace Core.Classes.Models;

public class Book(int id, string title, string author) : IBook
{
  public int Id { get; init; } = id;
  public string Title { get; set; } = title;
  public string Author { get; set; } = author;
  public bool IsBorrowed { get; set; }
  public DateTime? DueDate { get; set; }


  public void MarkAsBorrowed(DateTime dueDate)
  {
    IsBorrowed = true;
    DueDate = dueDate;
  }

  public void MarkAsReturned()
  {
    IsBorrowed = false;
    DueDate = null;
  }

}