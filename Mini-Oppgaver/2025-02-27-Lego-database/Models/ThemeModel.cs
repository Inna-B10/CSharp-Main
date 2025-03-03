using System.ComponentModel.DataAnnotations;

namespace _2025_02_27_Lego_database.Models;

public class ThemeModel
{
  [Required]
  public int ThemeId { get; set; }

  [Required]
  public string ThemeName { get; set; }

  public int? ParentId { get; set; }

  public ThemeModel(int themeId, string themeName, int? parentId)
  {
    ThemeId = themeId;
    ThemeName = themeName;
    ParentId = parentId;
  }
}