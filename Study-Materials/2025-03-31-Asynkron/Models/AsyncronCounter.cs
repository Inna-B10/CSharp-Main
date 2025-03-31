namespace Asynkron.Models;


public static class AsyncronCounter
{
    /// <summary>
    /// Denne metoden viser hvordan vi kan sette opp flere Threads, som alle gjør hver sin operasjon
    /// Så bruker vi Join for å joine threadene på slutten av operasjonen. 
    /// aka, vi venter til operasjonen på worker threadene våre er ferdig,
    /// før vi fletter de inn i main threaden igjen.
    /// </summary>
    public static void CountTwoThreads()
    {
        //Vi setter først opp en counter, og en thread som skal jobbe med denne counteren.
        Console.WriteLine("Couting with regular Threads...");
        var counter1 = new CounterModel("Counter 1", 5, 250);
        //Vi initialiserer en ny thread, som skal gjøre operasjone ThreadCounter.
        var thread1 = new Thread(ThreadCounter);
        //Vi setter counter1 inn i thread1 som starting state. 
        thread1.Start(counter1);

        //Vi kan nå sette opp en ny counter
        var counter2 = new CounterModel("Counter 2", 3, 500);
        //Sette av en ny thread som skal jobbe med counter2 via ThreadCounter.
        var thread2 = new Thread(ThreadCounter);
        //Så mater vi counter2 som starterstate til thread2.
        thread2.Start(counter2);

        //Hvis vi kjører denne metoden uten å Joine threadene,
        //vil vi aldri få resultatet av kjøringen. 
        //For på dette tidspunktet er scopet til CountWithThreads() ferdig,
        //og scopet til CountWithThreads avsluttes før thread1 og thread2 er ferdig. 
        //For å se et eksempel på dette, kommenter ut linje 38 og 39, og kjør programmet.

        //Vi kan tvinge scopet på å vente på 
        //thread 1 og thread 2 ved å Joine de inn i hovedthreaden igjen.

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Both Threads have finished operating!");
    }

    /// <summary>
    /// Legg merke til at vi kan ikke passe en typed referense til
    /// CounterModellen vår inn i Threaden.
    /// Isteden for kan vår Thread.Start method ta in et state object.
    /// En litt hacky workaround er å caste staten til en CounterModel.
    /// </summary>
    /// <param name="model"></param>
    private static void ThreadCounter(object? state)
    {
        var counter = (CounterModel)state!;
        Console.WriteLine($"Thread {counter.Name}: Started...");

        for (int i = 0; i <= counter.MaxCount; i++)
        {
            Thread.Sleep(counter.DelayMs); //Vi simulerer litt tungt arbeid ved å blokkere threaden i x antall ms.
            Console.WriteLine($"{counter.Name} has counted {i} of {counter.MaxCount}...");
        }
        Console.WriteLine($"Thread {counter.Name}: Finished");
    }

    /// <summary>
    /// Vi kan ta ibruk den innebygde Threadpoolen i .NET via Task nøkkelordet,
    /// der har vi tilgjengelig et ferdigdefinert set med subthreads som er klar til bruk.
    /// Vi må bare mate en action inn i queuen dems. 
    /// 
    /// Vi må da sette opp en del hjelpemetoder, blandannet en Awaiter som kan vente på,
    /// og resume når en task er fullført. 
    /// 
    /// Hvis vi setter opp denne awaiteren, kan vi mate awaiteren til threadpool scheduleren
    /// slik at awaiteren kan bli kjørt på ledige threads i threadpoolen som er tilgjengelig.
    /// 
    /// Vi trenger å sette opp følgende:
    /// 
    /// En awaiter som kan jobbe med vår count, i.e. printe ut counten på et tidspunkt,
    /// pause execution i et sett delay,
    /// passe current state til neste iteration av count.
    /// 
    /// CountWithTaskCompletionSource er hovedscopet som tar imot og håndterer signaler
    /// fra taskene vi setter opp. 
    /// </summary>
    /// <returns></returns>
    public static Task CountTwoTasks()
    {
        Console.WriteLine("Starting Counters on ThreadPool with TaskCompletionSources and Signals...");

        //Vi setter opp en "master task completion source, som er signalet vi sender ut om alle tasks er ferdig.
        var masterTsc = new TaskCompletionSource<bool>();

        //Vi setter opp counterene våre.
        var counter1 = new CounterModel("Counter Task 1", 4, 300);
        var counter2 = new CounterModel("Counter Task 2", 8, 200);


        //Vi setter opp tasks som skal jobbe på våre countere.
        var task1 = TscCounter(counter1);
        var task2 = TscCounter(counter2);


        //Vi kan så bruke WhenAll og ContinueWith for å utføre arbeid når tasksene er ferdig.

        Task.WhenAll(task1, task2).ContinueWith(allTasks => 
        {
            //Vi må først skjekke om det er en feil i en av taskene våre.
            if (allTasks.IsFaulted)
            {
                masterTsc.SetException(allTasks.Exception);
            }
            else
            {
                Console.WriteLine("All Tasks completed!");
                masterTsc.SetResult(true);
            }
        });
        return masterTsc.Task;
    }

    /// <summary>
    /// Vi kan nå sette opp en task om tar i bruk vår Task awaiter,
    /// og mater den inn i threadpool scheduleren.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private static Task TscCounter(CounterModel model)
    {

        //Vi setter opp en TaskCompletionSource som representerer når Counteren er ferdig å opperere.
        var tsc = new TaskCompletionSource();

        //Vi setter opp en action som vi kan levere til vår ThreadPool.
        Task.Run(()=>
        {
            try
            {
                Console.WriteLine($"Task {model.Name} is starting the counting process!");
                CountWithTaskAwaiter(model, tsc);
            }
            catch (Exception ex)
            {
                tsc.SetException(ex);
            }
        });

        //Vi returnerer Tasken som tilhører vår TaskCompletionSource. 
        return tsc.Task;
    }

    /// <summary>
    /// Legg merke til at vi kan typesafe vår counter her.
    /// Vi trenger ikke på noe som helst tidspunkt caste parameteret vårt, som representerer starting state,
    /// til en CounterModel, vi kan passe en referanse direkte.
    /// 
    /// Det vi setter opp her er en statemachine, som skal
    /// kunne gjøre arbeidet som trengs for å incremente counteren vår.
    /// 
    /// Vi kan sette opp på egen form for iterator for å
    /// representere vår statemachine, aka vi setter opp en count, og bruker en sub task
    /// som inkrementer den.
    /// </summary>
    /// <param name="model"></param>
    /// <param name="tsc"></param>
    private static void CountWithTaskAwaiter(CounterModel model, TaskCompletionSource tsc)
    {
        int currentCount = 0; //vi starter på posisjon 0.


        void CountContinuation()
        {
            try
            {
                if (currentCount <= model.MaxCount)
                {
                    //vi setter opp en task som skal pause execution. 
                    var delayTask = Task.Delay(model.DelayMs);
                    Console.WriteLine($"Task {model.Name} has counted {currentCount} / {model.MaxCount}...");
                    
                    //Vi kan så hente ut, og vente på at delayet er ferdig. Før vi setter opp en ny action som skal kjøres når delayTask er ferdig.
                    var awaiter = delayTask.GetAwaiter();
                    awaiter.OnCompleted(() =>
                    {
                        Console.WriteLine($"Task {model.Name} resuming after delay...");
                        //Vi incrementer currentCount
                        currentCount++;
                        
                        //og triggrer countContinuation på nytt via recrusjon.
                        CountContinuation();
                    });
                }
                else
                {
                    //Loopen er ferdig, og vi kan raportere dette til brukeren og til taskcompletionsource.
                    Console.WriteLine($"Task {model.Name} has completed the count...");
                    tsc.SetResult();
                }
            }
            catch (Exception ex)
            {
                //Hvis noe går galt, kan vi passe en exeption til vår taskcompletionsource som vil avslutte tasken, og propigere exceptionen ut av task.
                Console.WriteLine($"Task {model.Name} encountered");
                tsc.SetException(ex);
            }
        }

        //Vi starter prosessen.
        CountContinuation();
    }

    //Heldigvis for oss, det å sette opp Threadpools, Awaiters osv er ganske mye boilerplate.
    //På samme måte som yield lot oss unngå masse boilerplate for å sette opp en Incrementor.
    //Kan vi bruke nøkkelordene async await for å sette opp en Awaiter for oss.

    //La oss lage samme eksemplet som over, bare med async await.

    /// <summary>
    /// Legg merke til async nøkkelordet på tasken vår.
    /// Dette forteller compileren vår at den skal forvente å måtte
    /// bygge awaiter boilerplate kode for oss basert på
    /// når nøkkelordet await er funnet.
    /// </summary>
    /// <returns></returns>
    public static async Task CountWithAsyncAwait()
    {
        Console.WriteLine("Starting counter on Async Task...");


        //Som ovenfor setter vi opp counters og tasks for begge counterene.
        var counter1 = new CounterModel("Async Counter 1", 5, 500);
        var counter2 = new CounterModel("Async Counter 2", 6, 300);

        var task1 = AsyncCounter(counter1);
        var task2 = AsyncCounter(counter2);

        //Her ber vi compileren lage boilerplate awaiter kode for begge metodene våre.
        await Task.WhenAll(task1, task2);
        
        Console.WriteLine("Async Tasks completed!");
    }
    
    /// <summary>
    /// Den private metoden som skal arbeide mot Counteren vår.
    /// 
    /// Legg merke til hvor lik den er standard blocking code. 
    /// 
    /// utenom await nøkkelordet, som igjen vi kan tenke representerer
    /// en task.GetAwaiter().OnCompletion chain som sett i metoden over.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    private static async Task AsyncCounter(CounterModel model)
    {
        Console.WriteLine($"Async Task {model.Name} starting..");
        for (int i = 0; i <= model.MaxCount; i++)
        {
            await Task.Delay(model.DelayMs);
            Console.WriteLine($"Async Task {model.Name} has counted {i} / {model.MaxCount}...");
        }

        Console.WriteLine($"Async Task {model.Name} has completed the count...");
    }
}