using NAudio.Wave;
namespace _2025_02_16_Customisert_God_Morgen;
class MusicPlayer
{
  public static void Player(string path)
  {
    // string path = "/music/01. Highway To Hell.mp3";
    // Console.WriteLine();

    if (!File.Exists(path))
    {
      Console.WriteLine($"\nFile {path} not found!");
      return;
    }
    string songName = Path.GetFileName(path);

    using (var audioFile = new AudioFileReader(path))
    using (var outputDevice = new WaveOutEvent())
    {
      audioFile.Volume = 0.5f; // Начальная громкость 50%

      outputDevice.Init(audioFile);
      outputDevice.Play();

      bool isPaused = false; // Флаг паузы

      Console.WriteLine("🎵 Player launched!");
      Console.WriteLine($"Playing:  {songName}");
      Console.WriteLine("Control: ▶ [Spacebar] Pause, 🔊 [+/-] Volume,  ⏹ [Enter] Exit");

      Console.WriteLine();

      Console.WriteLine("The program will end after the track has ended.");

      // Автоматически закрываем программу после окончания трека
      outputDevice.PlaybackStopped += (sender, args) =>
      {
        Console.WriteLine("The music has ended. Exit program.");
        Environment.Exit(0);
      };

      // Управление громкостью в реальном времени
      while (true)
      {
        if (Console.KeyAvailable)
        {
          var key = Console.ReadKey(true).Key;
          if (key == ConsoleKey.Enter) break; //exit
          if (key == ConsoleKey.OemPlus || key == ConsoleKey.Add)
          {
            audioFile.Volume = Math.Min(audioFile.Volume + 0.1f, 1.0f);
            Console.WriteLine($"🔊+ : {audioFile.Volume * 100}%");
          }
          else if (key == ConsoleKey.OemMinus || key == ConsoleKey.Subtract)
          {
            audioFile.Volume = Math.Max(audioFile.Volume - 0.1f, 0.0f);
            Console.WriteLine($"🔊- : {audioFile.Volume * 100}%");
          }
          else if (key == ConsoleKey.Spacebar)
          {
            if (isPaused)
            {
              outputDevice.Play();
              Console.WriteLine("▶  Playing");
            }
            else
            {
              outputDevice.Pause();
              Console.WriteLine("⏸  Paused. Press `Spacebar` to continue playing");
            }
            isPaused = !isPaused;
          }
        }
      }
      // Если Enter нажат — программа завершится вручную
      outputDevice.Stop();
    }
  }
}