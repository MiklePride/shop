class program
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
                    seller.ShowProduct();
                    break;
                case "2":
                    seller.SellProduct(player);
                    break;
                case "3":
                    player.ShowProduct();
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
    public Seller(string name) : base(name)
    {
        Random random = new Random();
        int minimumPrice = 40;
        int maximumPrice = 160;

        Products.Add(new Apple(random.Next(minimumPrice, maximumPrice)));
        Products.Add(new Pear(random.Next(minimumPrice, maximumPrice)));
        Products.Add(new Bread(random.Next(minimumPrice, maximumPrice)));
        Products.Add(new Fish(random.Next(minimumPrice, maximumPrice)));
        Products.Add(new Potato(random.Next(minimumPrice, maximumPrice)));
        Products.Add(new Meat(random.Next(minimumPrice, maximumPrice)));
        Products.Add(new Milk(random.Next(minimumPrice, maximumPrice)));
    }

    public override void ShowProduct()
    {
        Console.Clear();

        if (Products.Count == 0)
            ShowErrorMessage();

        base.ShowProduct();
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
                Products.Remove(product);

                player.TakeProduct(product);

                Console.WriteLine("Товар куплен!");
            }
            else
            {
                ShowErrorMessage("Нехватает денег для покупки!");
            }
        }
        else
        {
            ShowErrorMessage("Такого товара нет!");
        }
    }

    private bool TryGetProduct(out Product product, string nameProduct)
    {
        product = null;

        foreach (var product1 in Products)
        {
            if (product1.Name.ToLower() == nameProduct.ToLower())
            {
                product = product1;
                return true;
            }
        }
        return false;
    }

    private void ShowErrorMessage(string message = "Еда закончилась!")
    {
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = color;
    }
}

class Player : Human
{
    public Player(string name) : base(name)
    {
    }

    public override void ShowProduct()
    {
        Console.Clear();

        if (Products.Count == 0)
            ShowErrorMessage();

        base.ShowProduct();
    }

    public void TakeProduct(Product product)
    {
        Money -= product.Price;
        Products.Add(product);
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Игрок: {Name}\nВсего денег: {Money}");
    }

    private void ShowErrorMessage(string message = "Инвентарь пуст!")
    {
        ConsoleColor color = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = color;
    }
}

abstract class Human
{
    protected List<Product> Products = new List<Product>();
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

    public virtual void ShowProduct()
    {
        if (Products.Count > 0)
        {
            foreach (var product in Products)
            {
                product.ShowInfo();
            }
        }
    }
}

class Product
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