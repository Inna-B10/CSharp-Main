using System;
using System.Collections.Generic;

public class ShoppingCart
{
    private List<string> items;

    public ShoppingCart()
    {
        items = new List<string>();
    }

    public void AddItem(string? item)
    {
        // adds "items" to the item list
        items.Add(item);
    }

    public void RemoveItem(string? item)
    {
        // removes "items" from the item list
        items.Remove(item);
    }

    public void DisplayAllItems()
    {
        Console.WriteLine("Current items in the shopping cart:");
        foreach (string item in items)
        {
            Console.WriteLine(item);
        }
    }
}