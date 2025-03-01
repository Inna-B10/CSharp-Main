using System.ComponentModel.DataAnnotations;

namespace _2025_02_27_Lego_database.Model;

public class ThemeModel
{
  [Required]
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  public int ParentId { get; set; }

  public ThemeModel(int id, string name, int parentId)
  {
    Id = id;
    Name = name;
    ParentId = parentId;
  }
}