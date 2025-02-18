// using System;
// using System.IO;
// 
// namespace _2025_02_16_Customisert_God_Morgen;
// 
// class Program
// {
//   static void Main(string[] args)
//   {
//     RunMusicSelection();
//   }
// 
//   static void RunMusicSelection()
//   {
//     string userName = GetUserName();
//     Console.WriteLine($"Welcome {userName}!");
//     string timeOfDay = GetCurrentTimeOfDay();
//     string genre = GetMusicGenre();
// 
//     if (genre == "e") return;
// 
//     string songName = MusicData.MusicDictionary[timeOfDay][genre];
//     Console.WriteLine($"`{songName}` - is a good choice!");
// 
//     if (!UserWantsToPlayMusic())
//     {
//       Console.WriteLine(GetEndMessage(timeOfDay));
//       return;
//     }
// 
//     PlayMusic(songName);
//   }
// 
//   static string GetUserName()
//   {
//     Console.WriteLine("Please, enter your name");
//     return GetValidInput("name");
//   }
// 
//   static string GetCurrentTimeOfDay()
//   {
//     DateTime date = DateTime.Now;
//     return GetTimesOfDay(date.Hour);
//   }
// 
//   static string GetMusicGenre()
//   {
//     Console.WriteLine("What genre do you prefer?");
//     Console.WriteLine("Rock, pop, instrumental, soundtracks");
//     Console.WriteLine("Enter r for `rock`, p for `pop`, i for `instrumental`, s for `soundtracks` or e to exit the program");
// 
//     return GetValidInput("genre");
//   }
// 
//   static bool UserWantsToPlayMusic()
//   {
//     Console.WriteLine("Would you like to play it now? y/n");
//     return GetValidInput("player") == "y";
//   }
// 
//   static void PlayMusic(string songName)
//   {
//     Console.Clear();
//     Console.WriteLine("Enjoy!");
// 
//     string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "music");
//     string filePath = FindFile(folderPath, songName + ".mp3");
// 
//     if (string.IsNullOrEmpty(filePath))
//     {
//       Console.WriteLine("Error: Music file not found!");
//       return;
//     }
// 
//     MusicPlayer.Player(filePath);
//   }
// 
//   static string GetTimesOfDay(int hour)
//   {
//     return hour switch
//     {
//       > 17 => "evening",
//       > 12 => "day",
//       > 5 => "morning",
//       _ => "night",
//     };
//   }
// 
//   static string GetValidInput(string type)
//   {
//     string? input;
//     do
//     {
//       input = Console.ReadLine()?.Trim();
//       if (!IsValidInput(input, type))
//       {
//         Console.WriteLine(GetErrorMessage(type));
//         input = null;
//       }
//     } while (input == null);
// 
//     return NormalizeInput(input, type);
//   }
// 
//   static bool IsValidInput(string? input, string type)
//   {
//     return type switch
//     {
//       "player" => !string.IsNullOrEmpty(input) && "yn".Contains(input.ToLower()),
//       "name" => !string.IsNullOrEmpty(input),
//       "genre" => !string.IsNullOrEmpty(input) && "rpise".Contains(input.ToLower()),
//       _ => false
//     };
//   }
// 
//   static string GetErrorMessage(string type)
//   {
//     return type switch
//     {
//       "player" => "Invalid input! Please enter `y` for yes, `n` for no",
//       "name" => "Invalid input! Please, enter your name",
//       "genre" => "Invalid input! Enter r for `rock`, p for `pop`, i for `instrumental`, s for `soundtracks` or e to exit the program",
//       _ => "Unknown error"
//     };
//   }
// 
//   static string NormalizeInput(string input, string type)
//   {
//     return type switch
//     {
//       "genre" => input.ToLower() switch
//       {
//         "r" => "rock",
//         "p" => "pop",
//         "i" => "instrumental",
//         "s" => "soundtracks",
//         _ => "e"
//       },
//       _ => input.ToLower()
//     };
//   }
// 
//   static string GetEndMessage(string now)
//   {
//     return now switch
//     {
//       "morning" => "Have a nice day!",
//       "day" => "Have a great rest of the day!",
//       "evening" => "Enjoy your evening!",
//       "night" => "Sleep well!",
//       _ => "Bye bye!"
//     };
//   }
// 
//   static string FindFile(string folderPath, string fileName)
//   {
//     if (!Directory.Exists(folderPath)) return "";
// 
//     string[] files = Directory.GetFiles(folderPath, fileName, SearchOption.TopDirectoryOnly);
//     return files.Length > 0 ? files[0] : "";
//   }
// }
