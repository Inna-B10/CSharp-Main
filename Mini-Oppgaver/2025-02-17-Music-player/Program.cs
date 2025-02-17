//dotnet add package NAudio (install)
/* -------------------------- Code With Single File ------------------------- */
// using NAudio.Wave;
// namespace _2025_02_17_Music_player;
// class Program
// {
//     static void Main()
//     {
//         string path = "music/01. Highway To Hell.mp3";
// 
//         if (!File.Exists(path))
//         {
//             Console.WriteLine($"File {path} not found!");
//             return;
//         }
//         string songName = Path.GetFileName(path);
// 
//         using (var audioFile = new AudioFileReader(path))
//         using (var outputDevice = new WaveOutEvent())
//         {
//             audioFile.Volume = 0.5f; // Начальная громкость 50%
// 
//             outputDevice.Init(audioFile);
//             outputDevice.Play();
// 
//             bool isPaused = false; // Флаг паузы
// 
//             Console.WriteLine("🎵 Player launched!");
//             Console.WriteLine($"Playing:  {songName}");
//             Console.WriteLine("Control: ▶ [Spacebar] Pause, 🔊 [+/-] Volume,  ⏹ [Enter] Exit");
// 
//             Console.WriteLine();
// 
//             Console.WriteLine("The program will end after the track has ended.");
// 
//             // Автоматически закрываем программу после окончания трека
//             outputDevice.PlaybackStopped += (sender, args) =>
//             {
//                 Console.WriteLine("The music has ended. Exit program.");
//                 Environment.Exit(0);
//             };
// 
//             // Управление громкостью в реальном времени
//             while (true)
//             {
//                 if (Console.KeyAvailable)
//                 {
//                     var key = Console.ReadKey(true).Key;
//                     if (key == ConsoleKey.Enter) break; //exit
//                     if (key == ConsoleKey.OemPlus || key == ConsoleKey.Add)
//                     {
//                         audioFile.Volume = Math.Min(audioFile.Volume + 0.1f, 1.0f);
//                         Console.WriteLine($"🔊+ : {audioFile.Volume * 100}%");
//                     }
//                     else if (key == ConsoleKey.OemMinus || key == ConsoleKey.Subtract)
//                     {
//                         audioFile.Volume = Math.Max(audioFile.Volume - 0.1f, 0.0f);
//                         Console.WriteLine($"🔊- : {audioFile.Volume * 100}%");
//                     }
//                     else if (key == ConsoleKey.Spacebar)
//                     {
//                         if (isPaused)
//                         {
//                             outputDevice.Play();
//                             Console.WriteLine("▶  Playing");
//                         }
//                         else
//                         {
//                             outputDevice.Pause();
//                             Console.WriteLine("⏸  Paused. Press `Spacebar` to continue playing");
//                         }
//                         isPaused = !isPaused;
//                     }
//                 }
//             }
//             // Если Enter нажат — программа завершится вручную
//             outputDevice.Stop();
//         }
//     }
// }

// /* --------------------------- Code With Playlist --------------------------- */
using System;
using System.Collections.Generic;
using System.IO;
using NAudio.Wave;

namespace _2025_02_17_Music_player;
class Program
{
    static List<string> playlist = new List<string>();
    static int currentTrackIndex = 0;
    static bool isPaused = false;
    static WaveOutEvent? outputDevice;
    static AudioFileReader? audioFile;

    static void Main()
    {
        string musicFolder = /* Directory.GetCurrentDirectory() -*/ AppContext.BaseDirectory + "/music"; // Папка с музыкой
        playlist.AddRange(Directory.GetFiles(musicFolder, "*.mp3")); // Ищем все MP3

        if (playlist.Count == 0)
        {
            Console.WriteLine("Нет MP3-файлов в папке!");
            return;
        }

        outputDevice = new WaveOutEvent();
        outputDevice.PlaybackStopped += OnPlaybackStopped;

        ShowPlaylist();  // Выводим плейлист
        PlayTrack(currentTrackIndex); // Воспроизводим первый трек

        Console.WriteLine("\n🎵 Плеер запущен! Управление:");
        Console.WriteLine("▶ [Spacebar] Пауза   ⏭ [→] Следующая   ⏮ [←] Предыдущая");
        Console.WriteLine("🔊 [+/-] Громкость  ⏹ [Enter] Выход");

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Enter) break; // Выход
                if (key == ConsoleKey.Spacebar) TogglePause(); // Пауза
                if (key == ConsoleKey.RightArrow) NextTrack(); // Следующий трек
                if (key == ConsoleKey.LeftArrow) PreviousTrack(); // Предыдущий трек
                if (key == ConsoleKey.OemPlus || key == ConsoleKey.Add) ChangeVolume(0.1f); // Увеличить громкость
                if (key == ConsoleKey.OemMinus || key == ConsoleKey.Subtract) ChangeVolume(-0.1f); // Уменьшить громкость
            }
        }

        DisposeAudio(); // Освобождаем ресурсы перед выходом
    }

    // 📜 Вывод плейлиста
    static void ShowPlaylist()
    {
        Console.WriteLine("\n🎶 Плейлист:");
        for (int i = 0; i < playlist.Count; i++)
        {
            if (i == currentTrackIndex)
                Console.WriteLine($"▶ {i + 1}. {Path.GetFileName(playlist[i])}  (Текущий)");
            else
                Console.WriteLine($"  {i + 1}. {Path.GetFileName(playlist[i])}");
        }
        Console.WriteLine();
    }

    // ▶ Воспроизведение трека
    static void PlayTrack(int index)
    {
        DisposeAudio(); // Останавливаем и освобождаем предыдущее аудио

        if (index < 0 || index >= playlist.Count) return;

        if (outputDevice == null)
        {
            outputDevice = new WaveOutEvent(); // Если не был инициализирован, инициализируем
            outputDevice.PlaybackStopped += OnPlaybackStopped;
        }

        audioFile = new AudioFileReader(playlist[index]);
        outputDevice?.Init(audioFile);

        // Установка начальной громкости
        // audioFile.Volume = 0.5f;

        outputDevice?.Play();

        // Console.Clear();
        ShowPlaylist(); // Обновляем плейлист с текущей песней
        Console.WriteLine($"▶  Играет: {Path.GetFileName(playlist[index])}");
    }

    // ⏸ Пауза / продолжение
    static void TogglePause()
    {
        if (isPaused)
        {
            outputDevice?.Play();
            Console.WriteLine("▶  Продолжение...");
        }
        else
        {
            outputDevice?.Pause();
            Console.WriteLine("⏸  Пауза...");
        }
        isPaused = !isPaused;
    }

    // ⏭ Следующий трек
    static void NextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % playlist.Count;
        PlayTrack(currentTrackIndex);
    }

    // ⏮ Предыдущий трек
    static void PreviousTrack()
    {
        currentTrackIndex = (currentTrackIndex - 1 + playlist.Count) % playlist.Count;
        PlayTrack(currentTrackIndex);
    }

    // 🎵 Автоматически включить следующий трек после завершения текущего
    static void OnPlaybackStopped(object sender, StoppedEventArgs e)
    {
        NextTrack();
    }

    // 🔊 Изменение громкости
    static void ChangeVolume(float amount)
    {
        if (audioFile != null)
        {
            audioFile.Volume = Math.Clamp(audioFile.Volume + amount, 0.0f, 1.0f);
            Console.WriteLine($"🔊  Громкость: {audioFile.Volume * 100}%");
        }
    }

    // 🗑 Остановка и освобождение ресурсов
    static void DisposeAudio()
    {
        if (outputDevice != null)
        {
            outputDevice.Stop();
        }
        audioFile?.Dispose();
        audioFile = null;
    }
}


