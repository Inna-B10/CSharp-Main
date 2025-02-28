using System.ComponentModel.DataAnnotations;

namespace _2025_02_27_Lego_database.Model;

public class ThemeModel
{
  [Required]
  public int Id { get; set; }

  [Required]
  public string Name { get; set; }

  public int ParentId { get; set; }

  public ThemeModel(string csvLine)
  {
    var csvData = csvLine.Split(",");
    if (int.TryParse(csvData[0], out int part0))
    {
      Id = part0;
    }
    Name = csvData[1];
    if (int.TryParse(csvData[2], out int part2))
    {
      ParentId = part2;
    }
  }
}