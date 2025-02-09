using System;
using System.Reflection.Metadata;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

public class Program  // Contains the programs code, initializes the program we
// are about to write
{
    public static void Main() // defines the entry point of the program,
                              // allows you to execute without an object and is accessible globally (throughout the entire code)
                              // it is the first point of execution in a program
    {
        string myName = "John"; // initializes the variable text string "myName"
        float myAge = 32.5f; // initializes the variable float "myAge". The number type needs to be declared in the variable as well

        Console.WriteLine("My name is " + myName + " and I'm " + myAge + " years old."); // prints the variable to the console

        Cup myCup = new( //creates a new instance of the cup class
            handle: false, // sets the handle property to false
            circ: 20.5d, // sets the circ property to 20.5
            depth: 50.4d // sets the depth property to 50.4
        );
        Console.WriteLine(myCup.HasHandle);
        // prints to the console whether the cup has a handle or not

        // Initializing different books and giving them parameters
        Book book1 = new(
       tittel: "Lord of the Rings",
       forfatter: "J.R.R. Tolkien",
       sideAntall: 1084
       );
        Book book2 = new(
            tittel: "Harry Potter and the C# mystery",
            forfatter: "J.K. Rowling",
            sideAntall: 183
        );
        Book book3 = new(
            tittel: "Chronicles of Narnia",
            forfatter: "C Sharp Lewis",
            sideAntall: 568
        );

        // Assigns the books to the library's book collection
        Book[] bookArray = [book1, book2, book3];

        // Assigns information to the library
        Bibliotek mittBibliotek = new(
            navn: "John's bibliotek",
            adresse: "Thormølensgate 47",
            bøker: bookArray
        );

        // Gets the mittBibliotek object and calls the method 
        // FindBook with the argument "Hey". The string will search the library for 
        // a book that contains the word "hey" in the title, and if it can't find 
        // any, it will print the default message assigned in the method.

        mittBibliotek.FindBook("Hey");

        // Initializes the diceRoller
        DiceRoller diceRoller = new DiceRoller(new Random());

        // Rolls the dice based on the numbers given in the constructor and prints them to the console
        //Console.WriteLine(diceRoller.Roll(1, 21));

        for (int i = 0; i < 3; i++)
        {
            Console.WriteLine(diceRoller.Roll(1, 21));
        }


        Imdb myImdb = new(); // Creates a new instance of the Imdb class
        myImdb.ShowFilms();
        Film newFilm = new( // creates a new film
            title: "Rush Hour",
            director: "Brett Ratner",
        length: 1.38f,
        actors: new string[] { "Jackie Chan", "Chris Tucker" },
        rating: 7.0f,
        country: "USA",
        language: "English",
        genre: "Comedy"
        );
        myImdb.Films.Add(newFilm); // adds the new film to the IMDB list
        myImdb.ShowFilms(); // show the list of films



    }
}
class Cup // inizialises the class Cup
{
    public bool HasHandle; // declares the property HasHandle, lets you tell the program 
                           // if the cup has a handle or not (boolean)
    public double Circumference; // declares the  property Circumfrence, lets you tell the program
                                 // what the circumference of the cup is
    public double Depth; // declares the depth property, lets you
                         // tell the program how deep the cup is
    public Cup(bool handle, double circ, double depth) // Declares what properties the cup should contain,
                                                       // and what type of data they should be
    {
        HasHandle = handle;
        Circumference = circ;
        Depth = depth;
    }

}

class Book // initializes the class Book
{
    public string Tittel;
    public string Forfatter;
    public int SideAntall;
    public Book(
        string tittel,
        string forfatter,
        int sideAntall
    )
    {
        Tittel = tittel;
        Forfatter = forfatter;
        SideAntall = sideAntall;
    }

}

class Bibliotek // initializes the class Bibliotek
{
    public string Navn;
    public string Adresse;
    public Book[] Bøker;
    public Bibliotek(
        string navn,
        string adresse,
        Book[] bøker
    )
    {
        Navn = navn;
        Adresse = adresse;
        Bøker = bøker;

    }

    public void ShowBooks() // creates the method ShowBooks, which is 
                            //  used to show the books in the library
    {
        for (int i = 0; i < Bøker.Length; i++) // loops through the list of books
        {
            Console.WriteLine(Bøker[i].Tittel); // for each book, it prints the title
            Console.WriteLine(Bøker[i].Forfatter); // for each book it prints the writer
        }
    }

    public void AddBook(Book nyBook) //creates a new book
    {
        Bøker[2] = nyBook;
    }

    public void FindBook(string inputparam) // creates the method FindBook, which
                                            // will search for a book in the library. The inputparam is just a name for the string
                                            // we ask the program to search for
    {
        Book foundBook = Bøker.Where(book => book.Tittel.Contains(inputparam)).FirstOrDefault();
        // Checks the book list for books where the title contains the inputparam string, and will display
        // the first instance of a book, or the default value of it
        if (foundBook != null) // if a book is found (it is not null, meaning it has a value)
        {
            Console.WriteLine("Book found: " + foundBook.Tittel); // The program logs the book to the console
        }
        else // if no book is found
        {
            Console.WriteLine("Book not found"); // it logs to the console that there is no book with that name
        }
    }
};


class DiceRoller // initializes the class Diceroller
{
    private Random _random; // creates a random number generator
    public int Roll(int lower, int higher) // Initializes the roll function, 
    // which takes in two numbers and prints a number between the lowest and highest number
    {
        return _random.Next(lower, higher);
    }
    public DiceRoller(Random random) // Assigns the passed Random object to the _random field

    {
        _random = random;
    }
}


class Film // Initializes the Film class and sets the different attributes
// the class should consist of
(
        string title,
        string director,
        float length,
        string[] actors,
        float rating,
        string country,
        string language,
        string genre
    )
{
    public string Title = title;
    public string Director = director;
    public float Length = length;
    public string[] Actors = actors;
    public float Rating = rating;
    public string Country = country;
    public string Language = language;
    public string Genre = genre;

}
class Imdb // initializes the library of films
{
    public List<Film> Films = new() { }; // creates a list of films
    public void ShowFilms()
    {
        for (int i = 0; i < Films.Count; i++)
        {
            Console.WriteLine(Films[i].Title);
            Console.WriteLine(Films[i].Director);
        }
    }
}

