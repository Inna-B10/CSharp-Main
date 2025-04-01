using System.Collections;
using Core.Classes;

namespace Tests;

public class AnimalTest
{

    /* Vi kan definere en Fact, en enkel test, 
    som skal kunne Asserte en Fact om programmet vårt. 
    La oss først definere en test som garanterer for at vi kan skape en Lion,
    som er en subclass av Animal. */
    [Fact]
    public void TestCreateLion()
    {
        var lion = new Lion("Olsen", "Blonde", 18, Feed.Carnivore);
        Assert.NotNull(lion); //Not null lar oss Asserte at en referansetype, som f.eks vår class, ikke er null på dette tidspunktet.
        Assert.Equal(Feed.Carnivore, lion.Feed); //Her kan vi Asserte at på dette tidspunket, skal to verdier være lik.
        Assert.IsAssignableFrom<Animal>(lion); //Her kan vi asserte at vår referansetype enten er en spesifikk type, eller derivert fra en annen type. 
        //Legg merke til Test results tabben som dukker opp i terminalen, slik at vi kan se på resultatet fra testen.
    }

    /* Vi kan teste med flere parametere samtidig, via Theory.
    Her kan vi definere en IEnumerable som vi kan mate som parameter til vår testfunksjon. */
    [Theory]
    [MemberData(nameof(TestData.GetEnumerator), MemberType = typeof(TestData))]
    public void TestCreateSeveralElephants(string name, double truncLength, int age, Feed feed)
    {
        var elephant = new Elephant(name, truncLength, age, feed);
        Assert.NotNull(elephant);
        Assert.Equal(age, elephant.Age);
        Assert.Equal(feed, elephant.Feed);
        Assert.Equal(truncLength, elephant.TruncLength);
    }

}
public class TestData
{


    /// <summary>
    /// Vi kan bruke samme yield nøkkelord her også, for å gi testen vår et bookmark
    /// på hvor den skal hente neste parameter. 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<object[]> GetEnumerator()
    {
        yield return new object[]{"Oscar", 1.3, 18, Feed.Herbivore};
        yield return new object[]{"Arne", 2.4, 26, Feed.Herbivore};
        yield return new object[]{"Terje", 0.8, 10, Feed.Herbivore};
    }

}

