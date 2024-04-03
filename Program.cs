Random random = new Random();

List<Plant> plants = new List<Plant>()
{
    new Plant()
    {
        Species = "Orchid",
        LightNeeds = 1,
        AskingPrice = 99.99M,
        ZIP = 37138,
        Sold = false,
    },
    new Plant()
    {
        Species = "Blueberry Bush",
        LightNeeds = 4,
        AskingPrice = 50.00M,
        ZIP = 37205,
        Sold = true,
    },
    new Plant()
    {
        Species = "Oak Sapling",
        LightNeeds = 5,
        AskingPrice = 82.35M,
        ZIP = 37040,
        Sold = false,
    },
    new Plant()
    {
        Species = "Archaefructus",
        LightNeeds = 4,
        AskingPrice = 999.99M,
        ZIP = 11111,
        Sold = false,
    },
    new Plant()
    {
        Species = "Rose Bush",
        LightNeeds = 4,
        AskingPrice = 7.17M,
        ZIP = 39275,
        Sold = true,
    },
};

string greeting = @" Welcome to the secondhand plant store!
Bottom text!";

Console.WriteLine(greeting);

void ListPlantInventory()
{
    Console.WriteLine("Plant Inventory:");
    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species} {(plants[i].Sold ? "was sold" : "is available") } for ${plants[i].AskingPrice}");
    }
}

void PressToContinue()
{
    Console.WriteLine("Press any key to continue");
    Console.ReadKey(); // THIS DOES NOT WORK IN GIT BASH STANDALONE BUT IT WORKS IN VSCODE TERMINAL GITBASH FOR SOME REASON
    Console.Clear();
}

void GetAndAddPlant()
{
    Console.WriteLine("Enter the information for the plant:");
    Console.WriteLine("Species:");
    string NewSpecies = null;
    while (NewSpecies == null)
    {
        try
        {
        NewSpecies = Console.ReadLine().Trim();
        }
        catch (Exception ex)
        {
            Console.Write(ex);
            Console.WriteLine("Try again");
        }
    }
    Console.WriteLine("Light needs (1-5):");
    int NewLightNeeds = int.Parse(Console.ReadLine().Trim());
    Console.WriteLine("Asking price:");
    decimal NewAskingPrice = decimal.Parse(Console.ReadLine().Trim());
    Console.WriteLine("zip code:");
    int NewZIP = int.Parse(Console.ReadLine().Trim());

    Plant NewPlant = new Plant()
    {
        Species = NewSpecies,
        LightNeeds = NewLightNeeds,
        AskingPrice = NewAskingPrice,
        ZIP = NewZIP,
        Sold = false,
    };

    plants.Add(NewPlant);
}

void AdoptAPlant()
{
    ListPlantInventory();
    Console.WriteLine("Enter the number for the plant you wish to adopt!");
    int PlantToBeAdopted = int.Parse(Console.ReadLine()) - 1;
    if (plants[PlantToBeAdopted].Sold == true)
    {
        Console.WriteLine("Plant already adopted!");
    }
    else
    {
        plants[PlantToBeAdopted].Sold = true;
        Console.WriteLine("Plant successfully adopted!");
    }
}

void DelistAPlant()
{
    ListPlantInventory();
    Console.WriteLine("Enter the number for the plant to be delisted:");
    int PlantToBeDelisted = int.Parse(Console.ReadLine()) - 1;
    plants.RemoveAt(PlantToBeDelisted);
}

void PlantOfTheDay()
{
    bool RouletteResult = false;
    while (RouletteResult == false)
    {
        int randomInt = random.Next(0, plants.Count + 1);
        if (plants[randomInt].Sold == false)
        {
            RouletteResult = true;
            Console.WriteLine(@$"The plant of the day is number {randomInt}:
            {plants[randomInt].Species}
            It goes for: ${plants[randomInt].AskingPrice}
            Light need: {plants[randomInt].LightNeeds}
            Its location is {plants[randomInt].ZIP}");

        }
        else
        {}
    }
}

void SearchByLightLevel()
{
    List<Plant> FilteredPlants = new List<Plant>();
    Console.WriteLine("Enter the maximum light level:");
    int MaxLight = 0;
    MaxLight = int.Parse(Console.ReadLine());
    while (MaxLight > 5 || MaxLight < 1)
    {
        Console.WriteLine("Light level must be from 1 to 5");
        MaxLight = int.Parse(Console.ReadLine());
    }
    
    foreach (Plant plant in plants)
    {
        if (plant.LightNeeds <= MaxLight)
        {
            FilteredPlants.Add(plant);
        }
    }
    Console.WriteLine($"Plants equal or less than {MaxLight} light need:");
    for (int i = 0; i < FilteredPlants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {FilteredPlants[i].Species} {(FilteredPlants[i].Sold ? "was sold" : "is available") } for ${FilteredPlants[i].AskingPrice}");
    }
}

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Options:
                        0. Exit program
                        1. View plant inventory
                        2. Post a new plant
                        3. Adopt a plant
                        4. Delist a plant
                        5. Plant of the day
                        6. Search plants by light level");
                        choice = Console.ReadLine();
    if (choice == "0")
    {
        Console.WriteLine("Program closing");
    }
    else if (choice == "1")
    {
        ListPlantInventory();
        PressToContinue();
    }
    else if (choice == "2")
    {
        GetAndAddPlant();
        PressToContinue();
    }
    else if (choice == "3")
    {
        AdoptAPlant();
        PressToContinue();
    }
    else if (choice == "4")
    {
        DelistAPlant();
        PressToContinue();
    }
    else if (choice == "5")
    {
        PlantOfTheDay();
        PressToContinue();
    }
    else if (choice == "6")
    {
        SearchByLightLevel();
        PressToContinue();
    }
    else
    {
        Console.WriteLine("Invalid input! Please enter a number corresponding to an option.");
    }
}