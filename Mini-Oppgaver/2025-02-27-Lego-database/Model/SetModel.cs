
using System.ComponentModel.DataAnnotations;

namespace _2025_02_27_Lego_database.Model;

public class SetModel
{
  [Required]
  public string SetNum { get; set; }

  [Required]
  public string Name { get; set; }

  [Required]
  public int Year { get; set; }

  [Required]
  public int ThemeId { get; set; }
  [Required]
  public int NumParts { get; set; } = 0;

  [Required]
  public string ImgUrl { get; set; }

  public SetModel(string csvLine)
  {
    var csvData = csvLine.Split(",");
    SetNum = csvData[0];
    Name = csvData[1];
    if (int.TryParse(csvData[2], out int part2))
    {
      Year = part2;
    }
    if (int.TryParse(csvData[3], out int part3))
    {
      ThemeId = part3;
    }
    if (int.TryParse(csvData[4], out int part4))
    {
      NumParts = part4;
    }
    ImgUrl = csvData[5];
  }
}