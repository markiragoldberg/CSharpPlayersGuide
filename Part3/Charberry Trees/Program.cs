using CharberryTrees;

CharberryTree tree = new CharberryTree();
Notifier notifier = new Notifier(tree);
Harvester harvester = new Harvester(tree);
int fruitCollected = 0;
harvester.HarvesterFull += CollectFruit;
while (fruitCollected < 41)
    tree.MaybeGrow();
Console.WriteLine($"{fruitCollected} fruit have been collected!");
Console.WriteLine("That's enough for today.");

void CollectFruit(Harvester? harvester)
{
    if (harvester != null)
    {
        int fruit = harvester.Empty();
        Console.WriteLine($"{fruit} fruit are collected from the harvester.");
        fruitCollected += fruit;
    }
}

// ----------------------------------

namespace CharberryTrees
{
    public class Notifier
    {
        private CharberryTree _tree;

        public Notifier(CharberryTree tree)
        {
            _tree = tree;
            _tree.Ripened += RipeNotification;
        }
        public void RipeNotification()
        {
            Console.WriteLine("A charberry fruit has ripened!");
        }
    }

    public class Harvester
    {
        public int Fruit { get; private set; } = 0;
        public int MaxFruit { get; init; } = 20;
        public event Action<Harvester>? HarvesterFull;
        private CharberryTree _tree;

        public Harvester(CharberryTree tree)
        {
            _tree = tree;
            _tree.Ripened += Harvest;
        }
        public void Harvest()
        {
            Console.WriteLine("The harvester harvests the charberry fruit.");
            _tree.Ripe = false;
            Fruit += 1;
            Console.WriteLine($"It now has {Fruit} fruit in storage.");
            if (Fruit >= MaxFruit)
            {
                Console.WriteLine("The harvester is full.");
                HarvesterFull?.Invoke(this);
            }
        }
        public int Empty()
        {
            int fruit = Fruit;
            Fruit = 0;
            return fruit;
        }
    }
    public class CharberryTree
    {
        private Random _random = new Random();
        public bool Ripe { get; set; }
        public event Action? Ripened;

        public void MaybeGrow()
        {
            // Only a tiny chance of ripening each time, but we try a lot!
            if (_random.NextDouble() < 0.00000001 && !Ripe)
            {
                Ripe = true;
                Ripened?.Invoke();
            }
        }
    }

    /*
     * Add a Ripened event to the CharberryTree class that is raised when the tree ripens.
    • Make a Notifier class that knows about the tree (Hint: perhaps pass it in as a constructor
    parameter) and subscribes to its Ripened event. Attach a handler that displays something like “A
    charberry fruit has ripened!” to the console window.
    • Make a Harvester class that knows about the tree (Hint: like the notifier, this could be passed as
    a constructor parameter) and subscribes to its Ripened event. Attach a handler that sets the tree’s
    Ripe property back to false.
    • Update your main method to create a tree, notifier, and harvester, and get them to work together
    to grow, notify, and harvest forever.
     */
}