using System;
using System.Collections.Generic;

/// <summary>
/// Manage game items using a Dictionary
/// </summary>
public class ItemShop
{
    private Dictionary<int, Item> _items = new Dictionary<int, Item>();

    public ItemShop()
    {
        // Initialize Dictionary with items
        _items = new Dictionary<int, Item>
        {
            {1, new Item("Sword", 100)},
            {2, new Item("Shield", 80)},
            {3, new Item("Health Potion", 20)},
        };
    }

public class Item
{
    public string Name { get; }
    public int Price { get; }

    public Item(string name, int price)
    {
        Name = name;
        Price = price;
    }
}

    public void AddItem(int itemId, Item newItem)
    {
        if (!_items.ContainsKey(itemId))
        {
            _items.Add(itemId, newItem);
            Console.WriteLine($"Added {itemId} to shop");
        }
        else
        {
            Console.WriteLine($"Item {itemId} has already been added");
        }
    }

    public void FindItem(int itemId)
    {
        if (_items.TryGetValue(itemId, out var item))
        {
            Console.WriteLine($"Found {item.Name} in shop. price: {item.Price}");
        }
        else
        {
            Console.WriteLine($"Item {itemId} not found");
        }
    }

    public void RemoveItem(int itemId)
    {
              if (_items.Remove(itemId))
        {
            Console.WriteLine($"Removed item {itemId} from shop");
        }
        else
        {
            Console.WriteLine($"Item {itemId} not found, cannot remove");
        }
    }
    
    public void ShowAllItems()
    {
        Console.WriteLine($"Shop Items:");
        
        foreach (var item in _items.Values) //  for ()
        {
            Console.WriteLine($"Name: {item.Name} - Price: {item.Price}");
        }
    }
}
  
