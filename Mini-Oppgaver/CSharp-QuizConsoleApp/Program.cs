using System;
using System.Collections.Generic;
using System.Text;




namespace QuizConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)

        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("Velkommen til quizen!");

            // liste med spørsmål
            List<Question> questions = new List<Question>
            {
                new Question("Hva er hovedstaden i Frankrike?", "Paris"),
                new Question("Hva er verdens største hav?", "Stillehavet"),
                new Question("Hva er hovedstaden i Italia?", "Roma"),
                new Question("Hva er hovedstaden i Tyskland?", "Berlin"),
                new Question("Hva er verdens høyeste fjell?", "Everest"),
                new Question("Hvilket årstall startet andre verdenskrig?", "1939"),
                new Question("Hva er Jordens største kontinent?", "Asia"),
                new Question("Hva er hovedstaden i Spania?", "Madrid"),
                new Question("Hva heter hovedstaden i Norge?", "Oslo"),
                new Question("Hvilken norsk by er kjent for sin brygge og fjorder?", "Bergen")
            };

            // Spille quizen
            int score = 0;
            foreach (Question question in questions)
            {
                Console.WriteLine(question.Quest);
                string userAnswer = Console.ReadLine();

                if (userAnswer.ToLower() == question.Answer.ToLower())
                {
                    Console.WriteLine("Korrekt!");
                    score++;
                }
                else
                {
                    Console.WriteLine("Feil. Riktig svar er: " + question.Answer);
                }
            }

            // Vise resultatet
            Console.WriteLine("\nQuiz avsluttet!");
            Console.WriteLine("Din score: " + score + " av " + questions.Count);
            Console.ReadKey();
        }
    }

    class Question
    {
        public string Quest { get; }
        public string Answer { get; }

        public Question(string quest, string answer)
        {
            Quest = quest;
            Answer = answer;
        }
    }
}

