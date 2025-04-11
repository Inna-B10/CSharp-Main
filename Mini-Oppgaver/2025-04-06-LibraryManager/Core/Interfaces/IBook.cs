namespace Core.Interfaces;

public interface IBook
{
  int Id { get; init; }
  string Title { get; set; }
  string Author { get; set; }
  bool IsBorrowed { get; set; }
  DateTime? DueDate { get; set; }
  string Section { get; set; }
  string Shelf { get; set; }
}
