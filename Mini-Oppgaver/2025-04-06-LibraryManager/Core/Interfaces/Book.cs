using System;

namespace Core.Interfaces;

public interface IBook
{
  int Id { get; set; }
  string Title { get; set; }
  string Author { get; set; }
  bool IsBorrowed { get; set; }
  DateTime? DueDate { get; set; }

  void MarkAsBorrowed();
  void MarkAsReturned();
}
