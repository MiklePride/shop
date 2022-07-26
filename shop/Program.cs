﻿class program
{
    static void Main(string[] args)
    {
        Seller seller = new Seller("Ал'Джавир");
        Player player = new Player("Путник");

        bool isWork = true;

        Console.WriteLine($"{player.Name} шел вдоль дороги, как вдруг на пути ему встретилась, столь желанная, лавка с едой,\n" +
            $"в которой стоял продавец по имени {seller.Name}...\n");
        Console.WriteLine("Нажмите любую кнопку ждя продолжения.");
        Console.ReadKey();

        while (isWork)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 20);
            seller.ShowInfo();

            Console.WriteLine();
            player.ShowInfo();

            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"{seller.Name}: Приветствую {player.Name}, чего изволишь?\n");

            Console.WriteLine("" +
                "1. Посмотреть товары.\n" +
                "2. Купить товар.\n" +
                "3. Заглянуть в инвентарь.\n" +
                "4. Выход.");

            string playerInput = Console.ReadLine();

            switch (playerInput)
            {
                case "1":
                    seller.ShowInfoProduct();
                    break;
                case "2":
                    seller.SellProduct(player);
                    break;
                case "3":
                    player.ShowInfoProduct();
                    break;
                case "4":
                    isWork = false;
                    break;
                default:
                    Console.WriteLine("Такой команды нет!");
                    break;
            }
            Console.WriteLine("Нажмите любую кнопку для продолжения...");
            Console.ReadKey();
        }
    }


}

class Seller : Human
{
    private List<Product> _products = new List<Product>();

    public Seller(string name) : base(name)
    {
        Random random = new Random();
        int minimumPrice = 40;
        int maximumPrice = 160;

        _products.Add(new Apple(random.Next(minimumPrice, maximumPrice)));
        _products.Add(new Pear(random.Next(minimumPrice, maximumPrice)));
        _products.Add(new Bread(random.Next(minimumPrice, maximumPrice)));
        _products.Add(new Fish(random.Next(minimumPrice, maximumPrice)));
        _products.Add(new Potato(random.Next(minimumPrice, maximumPrice)));
        _products.Add(new Meat(random.Next(minimumPrice, maximumPrice)));
        _products.Add(new Milk(random.Next(minimumPrice, maximumPrice)));
    }

    public override void ShowInfoProduct()
    {
        Console.Clear();

        if (_products.Count > 0)
        {
            foreach (var product in _products)
            {
                product.ShowInfo();
            }
        }
        else
            ErrorMessage();
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Продавец: {Name}\nВсего денег: {Money}");
    }

    public void SellProduct(Player player)
    {
        Product product;
        string playerInput;
        bool hasProductBeenFound;

        Console.WriteLine($"{player.Name}, что хочешь купить?");

        playerInput = Console.ReadLine();

        hasProductBeenFound = TryGetProduct(out product, playerInput);

        if (hasProductBeenFound == true)
        {
            if (player.Money >= product.Price)
            {
                Money += product.Price;
                _products.Remove(product);

                player.TakeProduct(product);

                Console.WriteLine("Товар куплен!");
            }
            else
                ErrorMessage("Нехватает денег для покупки!");
        }
        else
            ErrorMessage("Такого товара нет!");
    }

    private bool TryGetProduct(out Product product, string nameProduct)
    {
        product = null;

        foreach (var product1 in _products)
        {
            if (product1.Name.ToLower() == nameProduct.ToLower())
            {
                product = product1;
                return true;
            }
        }
        return false;
    }

    private void ErrorMessage(string message = "Еда закончилась!")
    {
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = color;
    }
}

class Player : Human
{
    private List<Product> _inventoryOfProduct = new List<Product>();

    public Player(string name) : base(name)
    {
    }

    public override void ShowInfoProduct()
    {
        Console.Clear();

        if (_inventoryOfProduct.Count > 0)
        {
            foreach (var product in _inventoryOfProduct)
            {
                product.ShowInfo();
            }
        }
        else
            ErrorMessage();
    }

    public void TakeProduct(Product product)
    {
        Money -= product.Price;
        _inventoryOfProduct.Add(product);
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Игрок: {Name}\nВсего денег: {Money}");
    }

    private void ErrorMessage(string message = "Инвентарь пуст!")
    {
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = color;
    }
}

abstract class Human
{
    public string Name { get; protected set; }
    public int Money { get; protected set; }

    public Human(string name)
    {
        Random random = new Random();
        int minimumMoney = 150;
        int maximumMoney = 400;

        Name = name;
        Money = random.Next(minimumMoney, maximumMoney);
    }

    public abstract void ShowInfoProduct();
}

abstract class Product
{
    public string Name { get; protected set; }
    public int Price { get; protected set; }
    public string Description { get; protected set; }

    public Product(int price)
    {
        Price = price;
    }

    public void ShowInfo()
    {
        Console.WriteLine($"{Name}\n{Description}\nЦена: {Price}\n");
    }
}
#region products

class Apple : Product
{
    public Apple(int price) : base(price)
    {
        Name = "Яблоко";
        Description = "Спелое и сладкое.";
    }
}

class Pear : Product
{
    public Pear(int price) : base(price)
    {
        Name = "Груша";
        Description = "Сочная и мягкая.";
    }
}

class Bread : Product
{
    public Bread(int price) : base(price)
    {
        Name = "Хлеб";
        Description = "Свежеиспеченный.";
    }
}

class Fish : Product
{
    public Fish(int price) : base(price)
    {
        Name = "Рыба";
        Description = "Для ухи самое то...";
    }
}

class Potato : Product
{
    public Potato(int price) : base(price)
    {
        Name = "Картофель";
        Description = "Картофель молодой.";
    }
}

class Meat : Product
{
    public Meat(int price) : base(price)
    {
        Name = "Мясо";
        Description = "Вроде бы барана, но это не точно...";
    }
}

class Milk : Product
{
    public Milk(int price) : base(price)
    {
        Name = "Молоко";
        Description = "Без сои!";
    }
}
#endregion