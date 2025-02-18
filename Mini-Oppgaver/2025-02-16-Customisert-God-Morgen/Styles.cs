using System;
using System.Drawing;

namespace _2025_02_16_Customisert_God_Morgen;
public static class StylesClass
{
  public const string RESET_ALL = "\x1b[0m";
  public const string ITALIC = "\x1B[3m";
  public const string RESET_ITALIC = "\x1B[23m";
  public const string BOLD = "\x1B[1m";
  public const string RESET_BOLD = "\x1B[22m";
  public const string INVERSE = "\x1B[7m";
  public const string RESET_INVERSE = "\x1B[27m";
  public const string ERROR = "\x1B[38;5;196m\x1B[48;5;189m";

  // public static string GetColor(string time)
  // {
  //   return time switch
  //   {
  //     "morning" => "\x1B[38;5;46m",
  //     "day" => "\x1B[38;5;21m",
  //     "evening" => "\x1B[38;5;202m",
  //     "night" => "\x1B[38;5;55m",
  //     _ => "\x1B[38;5;16m",
  //   };
  // }

  public static readonly Dictionary<string, (string color, string message, string goodbye)> DayOptions = new()
  {
    ["morning"] = (
                "\x1B[38;5;46m",
                "Starting the day with energy?\nEnergetic music is the perfect way to start the day with vigor and motivation.",
                "Have a nice day!"
            ),
    ["day"] = (
                "\x1B[38;5;21m",
                "Boosting your day?\nWell-chosen melodies enhance productivity throughout the workday.",
                "Have a great rest of the day!"
            ),
    ["evening"] = (
                "\x1B[38;5;202m",
                "Winding down?\nRelaxing music helps create an atmosphere of comfort and harmony in the evening.",
                "Enjoy your evening!"
            ),
    ["night"] = (
                "\x1B[38;5;55m",
                "Late-night tunes?\nSoft, calming tunes prepare your mind for a good night's sleep.",
                "Sleep well!"
            )
  };
}
