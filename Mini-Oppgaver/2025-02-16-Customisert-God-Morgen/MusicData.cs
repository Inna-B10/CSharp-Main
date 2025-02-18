namespace _2025_02_16_Customisert_God_Morgen;

public static class MusicData
{
  public static readonly Dictionary<string, Dictionary<string, string>> MusicDictionary = new(){
  { "morning", new() { { "rock", "Gorky_Park--Welcome_to_the_Gorky_Park" }, { "pop", "Sean_Paul--Temperture" },
                              { "instrumental", "Piano_Duel" }, { "soundtracks", "Hey_Mister" } } },

        { "day", new() { { "rock", "Kipelov--Power_of_Fire" }, { "pop", "Gnarls_Barkley--Crazy" },
                         { "instrumental", "Piano_Duet" }, { "soundtracks", "Dream_in_Bali" } } },

        { "evening", new() { { "rock", "Nate_Smith--Rather_Be_Lonely" }, { "pop", "Brad_Paisley--The_world" },
                             { "instrumental", "Secret" }, { "soundtracks", "Boiling" } } },

        { "night", new() { { "rock", "Scorpions--The_Winds_of_Change" }, { "pop", "Tanita_Tikaram--Twist_in_my_Sobriety" },
                           { "instrumental", "Dancing_with_Father" }, { "soundtracks", "You_Let_Me_Go_With_a_Smile" } } }
};
}