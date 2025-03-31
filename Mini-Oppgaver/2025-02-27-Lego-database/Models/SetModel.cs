
using System.ComponentModel.DataAnnotations;

namespace _2025_02_27_Lego_database.Models;

public class SetModel
{
  [Required]
  public string SetNum { get; set; }

  [Required]
  public string SetName { get; set; }

  [Required]
  public int Year { get; set; }

  [Required]
  public int ThemeId { get; set; }
  [Required]
  public int NumParts { get; set; } = 0;

  [Required]
  public string ImgUrl { get; set; }

  public SetModel(string setNum, string setName, int year, int themeId, int numParts, string imgUrl)
  {
    SetNum = setNum;
    SetName = setName;
    Year = year;
    ThemeId = themeId;
    NumParts = numParts;
    ImgUrl = imgUrl;
  }
}