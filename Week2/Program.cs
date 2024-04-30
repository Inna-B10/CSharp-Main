namespace CsharpKH
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Circles newCircle = new Circles(0.5);
            Console.WriteLine(newCircle.CalculateCircleArea());
            Console.WriteLine(newCircle.CalculatePerimeter());

            ShoppingCart shoppingCart = new ShoppingCart();
            shoppingCart.AddItem("Milk");
            shoppingCart.AddItem("Bananas");
            shoppingCart.AddItem("Bread");
            shoppingCart.AddItem("Steak");
            Console.WriteLine("Before removing an item from the list!\n");
            shoppingCart.DisplayAllItems();

            shoppingCart.RemoveItem("Milk");
            Console.WriteLine("\nAfter removing an item from the list!\n");
            shoppingCart.DisplayAllItems();

            Console.WriteLine("\n Calling the NumberList class and checking the output!\n");
            NumberList numberList = new NumberList();
            numberList.AppendNumber(1);
            numberList.AppendNumber(2);
            numberList.AppendNumber(3);

            numberList.DisplayNumbers();
        }
    }
}
