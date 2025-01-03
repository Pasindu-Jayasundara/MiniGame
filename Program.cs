int windowWidth = Console.WindowWidth;
int windowHeight = Console.WindowHeight;

string[] playerStatesArray = ["('-')", "(^-^)", "(X_X)"];
string[] foodArray = ["@@@@@", "$$$$$", "#####"];

int currentPlayerPositionX = 20;
int currentPlayerPositionY = 20;

int currentFoodPositionX = 0;
int currentFoodPositionY = 0;

string currentPlayer = playerStatesArray[0];
int currentFoodIndex = 0;

int consumedFoodCount = 0;

Random random = new Random();

SetUpGame();
IsWindowResized();


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

        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Consumed Food Count: "+consumedFoodCount);

    }
    IsWindowResized();
}

void DisplayRandomFoodInRandomLocations(){

    currentFoodIndex = random.Next(0, foodArray.Length);

    currentFoodPositionX = random.Next(0, windowWidth - currentPlayer.Length);
    currentFoodPositionY = random.Next(0, windowHeight - 1);

    if(currentFoodPositionX > 0 && currentFoodPositionX < windowWidth && currentFoodPositionY > 2 && currentFoodPositionY < windowHeight){
        Console.SetCursorPosition(currentFoodPositionX, currentFoodPositionY);
        Console.Write(foodArray[currentFoodIndex]);
    }else{
        DisplayRandomFoodInRandomLocations();
    }
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

    int playerLastX = currentPlayerPositionX;
    int playerLastY = currentPlayerPositionY;

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
    }else{
        MovePlayer();
    }

    if(currentPlayerPositionX < 0 && currentPlayerPositionX >= windowWidth && currentPlayerPositionY > 2 && currentPlayerPositionY < windowHeight){
        currentPlayerPositionX = playerLastX;
        currentPlayerPositionY = playerLastY;
    }

    Console.SetCursorPosition(playerLastX, playerLastY);
    for(int i=0; i<currentPlayer.Length; i++){
        Console.Write(" ");
    }

    if(currentPlayerPositionX == currentFoodPositionX && currentPlayerPositionY == currentFoodPositionY){
        consumedFoodCount++;
        DisplayRandomFoodInRandomLocations();
        ChangePlayerAppearanceToFoodConsumed();

        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Consumed Food Count: "+consumedFoodCount);
    }

    Console.SetCursorPosition(currentPlayerPositionX, currentPlayerPositionY);
    Console.Write(currentPlayer);
    Console.SetCursorPosition(currentPlayerPositionX-currentPlayer.Length+4, currentPlayerPositionY);


    MovePlayer();

}

void SetUpGame(){

    currentPlayerPositionX = 20;
    currentPlayerPositionY = 20;

    currentFoodPositionX = 0;
    currentFoodPositionY = 0;

    currentPlayer = playerStatesArray[0];
    currentFoodIndex = 0;

    consumedFoodCount = 0;

    Console.Clear();
    DisplayRandomFoodInRandomLocations();

    Console.SetCursorPosition(0, 0);
    Console.WriteLine("Consumed Food Count: "+consumedFoodCount);

    if(currentPlayerPositionX >= 0 && currentPlayerPositionX < Console.WindowWidth && currentPlayerPositionY >= 2 && currentPlayerPositionY < Console.WindowHeight){
        Console.SetCursorPosition(currentPlayerPositionX, currentPlayerPositionY);
        Console.Write(currentPlayer);
        Console.SetCursorPosition(currentPlayerPositionX-currentPlayer.Length+4, currentPlayerPositionY);
    }


    MovePlayer();

}
