
using _2025_02_27_Lego_database.Models;

namespace _2025_02_27_Lego_database.Services;

public class SetService
{
  public static SetModel ParseSets(string csvLine)
  {
    var csvData = csvLine.Split(",");

    return new SetModel(
        csvData[0],
        csvData[1],
        int.TryParse(csvData[2], out int year) ? year : 0,
        int.TryParse(csvData[3], out int themeId) ? themeId : 0,
        int.TryParse(csvData[4], out int numParts) ? numParts : 0,
    csvData[5]);
  }
}