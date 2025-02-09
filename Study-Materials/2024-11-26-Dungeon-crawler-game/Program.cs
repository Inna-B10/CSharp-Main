using System;
using System.Collections.Generic;
					
public class Program
{
	public static void Main() // initializes the game
	{
		var ui = new UserInterface();
		var newGame = new Game(ui);
		newGame.WelcomePlayer();
		newGame.RunGame();
	}
	class DungeonRoom(string description, string obstacleDesc, int diff, Treasure loot, bool cleared = false)
	// initializes the dungeon room, and populates it with a description, a description of the obstacle in the room,
	// the difficulty in the room, a boolean to check if the room has been cleared or not and a treasure.
	{
		public string Description = description;
		public string ObstacleDescription = obstacleDesc;
		public int Difficulty = diff;
		public bool Cleared = cleared;
		public Treasure Loot = loot;
	}
	class Treasure(string desc, int mod)
	// Creates the treasure class and populates it with a description and a modifier
	{
		public string Description = desc;
		public int Modifier = mod;
	}
	class Player(string name)
	// Creates the player class, and populates it with a name, gives it access to the dice roller and a list of 
	// modifiers. At the end we create the modifier sum, which is the sum of all the modifiers the player has.
	{
		public string Name = name;
		public DiceRoller diceRoller = new(new Random());
		public List<int> Modifiers = new();
		public int ModifierSum()
		{
			int sum = 0;
			foreach (int number in Modifiers)
			{
				sum += number;	
			}
			return sum;
		}
	}
	class DiceRoller(Random random)
	// Creates a simple dice roller that takes in two numbers and returns a random number between the high and low
	{
		private Random _random = random;
		public int Roll(int low, int high)
		{
			return _random.Next(low, high);
		}	
	}
	class Game(UserInterface ui)
	// Creates the game class, and populates it with a user interface, a player and a list of rooms. 
	// the list of rooms can be easily modified to expand the game.
	{
		public Player Player;
		private readonly UserInterface _ui = ui;
		public List<DungeonRoom> Dungeon = new(){
			new(
				description: "Hi and welcome to THE DUNGEON.\nTo move on further, you will have to clear each obstacle in a room.\nAlong the way you will get some loot that can either help or hinder you.\nYou stand in a dank tavern.\n",
				obstacleDesc: "\nA drunkard sits regailing you of hidden treasure in the cellar below.\n It seems you can convince him to tell it all if you give it a try.",
				diff: 2,
				loot: new Treasure(
					desc: "\nMagical Wine",
					mod: 1
					)
				),
				new(
					description: "\n\nA dark wine cellar meets you as you descend below the tavern. \nThe drunkards told of great treasure hiding behind one of the large vats of wine",
					obstacleDesc: "\nThe vat is large and heavy, it will require effort to move.",
					diff: 3,
					loot: new Treasure(
						desc: "\nA small crystal, hidden in the floor below the vat.",
						mod: 2
					)
				),
				new(
					description: "\n\nYou find a trapdoor behind the vat of wine. \nYou climb through the opening and emerge in a narrow tunnel that smells of dirt. \nThere is a faint light flickering at the end.",
					obstacleDesc: "\nIn front of you is a table with a locked box on it. Lockpicking it will be difficult.",
					diff: 4,
					loot: new Treasure(
						desc: "\nA heavy and rusted key.",
						mod: 3
					)
				),
				new(
					description: "\n\nAt the end of the tunnel is a large cavern-like room. \nIt's obvious that the room has been used as a secret hideout at some point. \nThere is old furniture scattered across the room, and on a desk you see an open book.",
					obstacleDesc: "\nThe book is written in an unfamiliar language, but there is a translator on the table. \nDecoding it will take time and lots of concentration.",
					diff: 6,
					loot: new Treasure(
						desc: "\nA number code that seems to unlock something important.",
						mod: 4
					)
				),
				new(
					description: "\n\nYou continue through the large cavern, checking the cupboards and desks for useful items, \nbut everything seems to have been emptied in a rush. There are papers and scrolls scattered across the ground. \nIn a corner however, you spot a small chest with a lock.",
					obstacleDesc: "\nThe lock requires you to remember the code you found in the book earlier.",
					diff: 2,
					loot: new Treasure(
						desc: "\nA rusted dagger",
						mod: 2
					)
				)
		};
		public void WelcomePlayer()
		// This is a method that welcomes the Player and asks them for a name. The name input here
		// is the name the player will be referenced by throughout the game.
		{
			string name = _ui.Request<string>("What is your name, traveler?");
			Player = new Player(name);
		}
		public void RunGame()
		// this method runs the game. It starts by welcoming the player, then it enters a loop that
		// says that as long as there are more rooms, and the boolean "cleared" is still false, it will 
		// continue to ask the player for an action, and as long as the number on the dice roll
		// is higher than the assigned difficulty of the room, it will give us the next room in the public List<DungeonRoom> Dungeon list.
		// When all the rooms have been cleared, the player will recieve a notification that they have won.

		{
			int roomIndex = 0;
			while (roomIndex < Dungeon.Count)
			{
				while (Dungeon[roomIndex].Cleared == false)
				{
					_ui.Send(Dungeon[roomIndex].Description);
					_ui.Send(Dungeon[roomIndex].ObstacleDescription);
					var input = _ui.Request<string>("\nWhat is your action");
					switch(input)
					{
						case "help":
								break;
						case "roll":
								var result = Player.diceRoller.Roll(1, (6 + Player.ModifierSum()));
								_ui.Send($"\nYou rolled: {result}");
								if (result >= Dungeon[roomIndex].Difficulty) 
								{
									Dungeon[roomIndex].Cleared = true;
									_ui.Send($"You get: {Dungeon[roomIndex].Loot.Description}");
									Player.Modifiers.Add(Dungeon[roomIndex].Loot.Modifier);
									roomIndex++;
								}
								break;
						default:
								_ui.Send("\n\nPlease input an action, help or roll.");
								break;
					}
					if (roomIndex >= Dungeon.Count) 
					{
						_ui.Send($"\n\nCongratulations of completing the dungeon, {Player.Name}");
						return;
					}
				}
			}
		}
		
	}
	
	

	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	//An interface is a contract a blueprint (class, struct and so on) must fullfill.
	//Several classes can adhere to an interface, but implement the methods differently.
	interface IUserInterface
	{
		public void Send(string message);
		public T Request<T>(string message);
	}
	class UserInterface : IUserInterface
	{
		private readonly Dictionary<Type, Func<string, object>> _parsers;
		/// <summary>
		/// Object enabling sending requests and receiving responses to users of the program.
		/// <example>
		/// For Example:
		/// <code>
		/// //Initializing the user interface.
		/// var ui = new UserInterface();
		/// //Sending a request to the user requesting their age:
		/// var userAge = ui.Request{T}();
		/// //Sending a message telling the user program received their response.
		/// ui.Send($"Your age is {userAge}.");
		/// </code>
		/// </example>
		/// </summary>
		public UserInterface()
		{
			_parsers = new Dictionary<Type, Func<string, object>>
			{
				{typeof(int), input=>ParseInt(input)},
				{typeof(string),input => input},
				{typeof(float), input=>ParseFloat(input)},
				{typeof(double), input=>ParseDouble(input)},
				{typeof(decimal), input=>ParseDecimal(input)},
				{typeof(bool), input=>ParseBool(input)}
			};
		}
		/// <summary>
		/// A function requesting a strict typed input from the users.
		/// The following types are supported by the interface:
		/// <list type="bullet">
		/// <item>
		/// <term>string</term>
		/// <description>a datatype representing a string of some characters, usually represents a word, or words.</description>
		/// </item>
		/// <item>
		/// <term>bool</term>
		/// <description>a datatype representing some affirmation, can have the value true or false.</description>
		/// </item>
		/// <item>
		/// <term>decimal</term>
		/// <description>a C# spesific datatype representing an accurate decimal number. more taxing on the system than a float and double, but considered accurate for financial work.</description>
		/// </item>
		/// <item>
		/// <term>float</term>
		/// <description>a datatype representing some decimal number, generally considered inaccurate, but lightweight compared to other decimal datatypes. Ideal for simple decimal operations.</description>
		/// </item>
		/// <item>
		/// <term>double</term>
		/// <description>a datatype representing some decimal number. Has twice the available data accessible for storing values than floats, and are considered somewhat accurate.</description>
		/// </item>
		/// <item>
		/// <term>int</term>
		/// <description>a datatype representing a whole number. max size is (+ -) 2^31</description>
		/// </item>
		/// </list>
		/// </summary>
		/// <param name="message">The message representing the request</param>
		/// <typeparam name="T">The type of data requested</typeparam>
		/// <returns>some value of type T requested from the user</returns>
		public T Request<T>(string message)
		{
			try
			{
				var input = RequestInput(message);
				var success = _parsers.TryGetValue(typeof(T), out var parseFunc);
				if (!success || parseFunc is null) throw new NotImplementedException($"Input of type {typeof(T)} is not yet supported.");
				return (T)parseFunc(input);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("Please try again.");
				return Request<T>(message);
			}
		}

		private static bool ParseBool(string input)
		{
			var success = bool.TryParse(input, out var output);
			if (!success) throw new FormatException($"Input {input} is not a valid boolean value.");
			return output;
		}

		private static int ParseInt(string input)
		{
			var success = int.TryParse(input, out var output);
			if (!success) throw new FormatException($"Input {input} could not be parsed, expected type of {typeof(int)}.");
			return output;
		}
		private static float ParseFloat(string input)
		{
			var success = float.TryParse(input, out var output);
			if (!success) throw new FormatException($"Input {input} could not be parsed, expected type of {typeof(float)}.");
			return output;
		}
		private static double ParseDouble(string input)
		{
			var success = double.TryParse(input, out var output);
			if (!success) throw new FormatException($"Input {input} could not be parsed, expected type of {typeof(double)}.");
			return output;
		}
		private static decimal ParseDecimal(string input)
		{
			var success = decimal.TryParse(input, out var output);
			if (!success) throw new FormatException($"Input {input} could not be parsed, expected type of {typeof(decimal)}.");
			return output;
		}
		/// <summary>
		/// A method to add a new parser to the parser dictionary. 
		/// </summary>
		/// <param name="parser">The parser function parsing a string to the parsed type T</param>
		/// <typeparam name="T">The type T representing the desired parsed datatype</typeparam>
		/// <exception cref="ArgumentException">If the parser allready exists in the parsing dictionary, throws an ArgumentException error.</exception>
		public void AddParser<T>(Func<string, object> parser)
		{
			if (_parsers.ContainsKey(typeof(T))) throw new ArgumentException($"Parser for type {typeof(T)} already exists.");
			_parsers[typeof(T)] = parser;
		}
		/// <summary>
		/// Method to Send a response to users of the program. 
		/// </summary>
		/// <param name="message">A string representing the message being sent to the user.</param>
		public void Send(string message)
		{
			Console.WriteLine(message);
		}

		private static string RequestInput(string message)
		{
			string? input = null;
			while (input is null)
			{
				Console.WriteLine(message);
				input = Console.ReadLine();
			}
			return input;
		}
	}
}