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
        AvailableUntil = new DateTime(2024, 6, 23)
    },
    new Plant()
    {
        Species = "Blueberry Bush",
        LightNeeds = 4,
        AskingPrice = 50.00M,
        ZIP = 37205,
        Sold = true,
        AvailableUntil = new DateTime(2024, 4, 15)
    },
    new Plant()
    {
        Species = "Oak Sapling",
        LightNeeds = 5,
        AskingPrice = 82.35M,
        ZIP = 37040,
        Sold = false,
        AvailableUntil = new DateTime(2024, 4, 1)
    },
    new Plant()
    {
        Species = "Archaefructus",
        LightNeeds = 4,
        AskingPrice = 999.98M,
        ZIP = 11111,
        Sold = false,
        AvailableUntil = new DateTime(2025, 12, 20)
    },
    new Plant()
    {
        Species = "Rose Bush",
        LightNeeds = 4,
        AskingPrice = 7.17M,
        ZIP = 39275,
        Sold = true,
        AvailableUntil = new DateTime(2024, 5, 5)
    },
};

string greeting = @" Welcome to the secondhand plant store!
Bottom text!";

Console.WriteLine(greeting);

void ListPlantInventory()
{
    Console.WriteLine("Plant Inventory:");
    int i = 0;
    foreach (Plant plant in plants)
    {
        string EachPlantDetails = PlantDetails(plant);
        Console.WriteLine($"{i + 1}. {EachPlantDetails}");
        i++;
    }
}

void PressToContinue()
{
    Console.WriteLine("Press any key to continue");
    Console.ReadKey(); // THIS DOES NOT WORK IN GIT BASH STANDALONE BUT IT WORKS IN VSCODE TERMINAL GITBASH FOR SOME REASON
    Console.Clear();
}

DateTime GetDateForExpiry()
{
    Console.WriteLine("Enter expiration date, starting with year:");
    int ExpYear = int.Parse(Console.ReadLine());
    Console.WriteLine("Month:");
    int ExpMonth = int.Parse(Console.ReadLine());
    Console.WriteLine("Day:");
    int ExpDay = int.Parse(Console.ReadLine());

    DateTime bubkis = new DateTime(1970, 1, 1); //this is a hack - I have to have a return for the function to work, even though it exits.
    try
    {
        DateTime ExpiryDate = new DateTime(ExpYear, ExpMonth, ExpDay);
        return ExpiryDate;
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine("Invalid date");
        return bubkis;
    }
    
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

    DateTime ExpiryDate = GetDateForExpiry();

    Plant NewPlant = new Plant()
    {
        Species = NewSpecies,
        LightNeeds = NewLightNeeds,
        AskingPrice = NewAskingPrice,
        ZIP = NewZIP,
        Sold = false,
        AvailableUntil = ExpiryDate
    };

    plants.Add(NewPlant);
}

void AdoptAPlant()
{
    List<Plant> FilteredPlants = new List<Plant>();
    foreach (Plant plant in plants)
    {
        int DateComparison = DateTime.Compare(plant.AvailableUntil, DateTime.Now);
        if (plant.Sold == false && DateComparison >= 0)
        {
            FilteredPlants.Add(plant);
        }
    }

    Console.WriteLine("Plant Inventory:");
    for (int i = 0; i < FilteredPlants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {FilteredPlants[i].Species} {(FilteredPlants[i].Sold ? "was sold" : "is available") } for ${FilteredPlants[i].AskingPrice}");
    }

    Console.WriteLine("Enter the number for the plant you wish to adopt!");
    int PlantToBeAdopted = int.Parse(Console.ReadLine()) - 1;
    if (FilteredPlants[PlantToBeAdopted].Sold == true)
    {
        Console.WriteLine("Plant already adopted!");
    }
    else
    {
        FilteredPlants[PlantToBeAdopted].Sold = true;
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

void StatsForPlants()
{
    decimal LowestPlant = 999.99M;
    string LowestPlantName = "";
    foreach (Plant plant in plants)
    {
        if (plant.AskingPrice < LowestPlant)
        {
            LowestPlant = plant.AskingPrice;
            LowestPlantName = plant.Species;
        }
    }

    List<Plant> AvailablePlants = new List<Plant>();
    foreach (Plant plant in plants)
    {
        int DateComparison = DateTime.Compare(plant.AvailableUntil, DateTime.Now);
        if (plant.Sold == false && DateComparison >= 0)
        {
            AvailablePlants.Add(plant);
        }
    }
    int AvailablePlantCount = AvailablePlants.Count;

    int HighestPlantLight = -1;
    string HighestLightPlantName = "";
    foreach (Plant plant in plants)
    {
        if (plant.LightNeeds > HighestPlantLight)
        {
            HighestPlantLight = plant.LightNeeds;
            HighestLightPlantName = plant.Species;
        }
    }
    int LightCount = 0;
    foreach (Plant plant in plants)
    {
        LightCount += plant.LightNeeds;
    }
    double LightCountFixed = (double)LightCount;
    double AverageLight = LightCountFixed / plants.Count;

    double SoldPlants = 0;
    foreach (Plant plant in plants)
    {
        if (plant.Sold == true)
        {
            SoldPlants ++;
            Console.WriteLine("QUACK");
        }
    }
    Console.WriteLine(SoldPlants / plants.Count);
    double PercentageSold = (SoldPlants / plants.Count) * 100;

    Console.WriteLine(@$" Plant inventory stats:
                        Lowest price plant: {LowestPlantName}
                        Number of plants available: {AvailablePlantCount}
                        Highest light need: {HighestLightPlantName}
                        Average light need: {AverageLight}
                        Percent of plants adopted: {PercentageSold}");
}

string PlantDetails(Plant plant)
{
    string plantString = $"{plant.Species} {(plant.Sold ? "was sold" : "is available") } for ${plant.AskingPrice}";
    return plantString;
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
                        6. Search plants by light level
                        7. Plant stats!");
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
    else if (choice == "7")
    {
        StatsForPlants();
        PressToContinue();
    }
    else
    {
        Console.WriteLine("Invalid input! Please enter a number corresponding to an option.");
    }
}