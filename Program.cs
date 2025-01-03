int windowWidth = Console.WindowWidth;
int windowHeight = Console.WindowHeight;

string[] playerStatesArray = ["('-')", "(^-^)", "(X_X)"];
string[] foodArray = ["@@@@@", "$$$$$", "#####"];

int currentPlayerPositionX = 0;
int currentPlayerPositionY = 0;

int currentFoodPositionX = 0;
int currentFoodPositionY = 0;

string currentPlayer = playerStatesArray[0];
int currentFoodIndex = 0;

int consumedFoodCount = 0;

Random random = new Random();

DisplayRandomFoodInRandomLocations();

while(true){
    IsWindowResized();
    MovePlayer();
}

void IsWindowResized(){

    if(Console.WindowWidth!=windowWidth){
        windowWidth = Console.WindowWidth;

        Console.WriteLine("\nWindow width has changed");
        Console.WriteLine("Press 'R' to restart the game");
        Console.WriteLine("Press 'E' to exit the game");

        string? v = Console.ReadLine();
        if(v?.ToLower()=="R"){
            SetUpGame();
        }else if(v?.ToLower()=="E"){
            Environment.Exit(0);
        }

        Console.Clear();
    }
}

void DisplayRandomFoodInRandomLocations(){

    currentFoodIndex = random.Next(0, foodArray.Length);

    currentFoodPositionX = random.Next(0, windowWidth - currentPlayer.Length);
    currentFoodPositionY = random.Next(0, windowHeight - 1);

    Console.SetCursorPosition(currentFoodPositionX, currentFoodPositionY);
    Console.Write(foodArray[currentFoodIndex]);
}

void ChangePlayerAppearanceToFoodConsumed(){

    if(consumedFoodCount < 5){
        currentPlayer = playerStatesArray[0];
    }else if(consumedFoodCount >= 5 && consumedFoodCount < 10){
        currentPlayer = playerStatesArray[1];
    }else{
        currentPlayer = playerStatesArray[2];
    }

}

void FreezePlayer(){

    System.Threading.Thread.Sleep(1000);
}

void MovePlayer(){

    ConsoleKeyInfo keyInfo = Console.ReadKey();

    if(keyInfo.Key == ConsoleKey.UpArrow){
        currentPlayerPositionY--;
    }else if(keyInfo.Key == ConsoleKey.DownArrow){
        currentPlayerPositionY++;
    }else if(keyInfo.Key == ConsoleKey.LeftArrow){
        currentPlayerPositionX--;
    }else if(keyInfo.Key == ConsoleKey.RightArrow){
        currentPlayerPositionX++;
    }else if(keyInfo.Key == ConsoleKey.F){
        FreezePlayer();
    }

    if(currentPlayerPositionX == currentFoodPositionX && currentPlayerPositionY == currentFoodPositionY){
        consumedFoodCount++;
        DisplayRandomFoodInRandomLocations();
        ChangePlayerAppearanceToFoodConsumed();
    }

    Console.SetCursorPosition(currentPlayerPositionX, currentPlayerPositionY);
    Console.Write(currentPlayer);
}

void SetUpGame(){

    currentPlayerPositionX = 0;
    currentPlayerPositionY = 0;

    currentFoodPositionX = 0;
    currentFoodPositionY = 0;

    currentPlayer = playerStatesArray[0];
    currentFoodIndex = 0;

    consumedFoodCount = 0;

    Console.Clear();
    DisplayRandomFoodInRandomLocations();
}
