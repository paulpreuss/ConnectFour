// See https://aka.ms/new-console-template for more information
using Azubi.ConnectFour;
using Azubi.ConnectFour.Abstracts;

IGameEngine engine = new GameEngine();

var setup = false;
var won = false;
var playerOneTurn = true;


while (!setup)
{
    Console.WriteLine("Singleplayer (1) or multiplayer (2)?");

    var input = Console.ReadLine();

    if (!Int32.TryParse(input, out var result))
    {
        Console.Clear();
        Console.WriteLine("Please enter a number.");
        continue;
    }

    if (!(result > 0 && result < 3))
    {
        Console.Clear();
        Console.WriteLine("Please enter a number between 1 and 2.");
        continue;
    }

    if (result == 1)
    {
        Singleplayer();
        setup = true;
    }
    else
    {
        Multiplayer();
        setup = true;
    }

    Console.ReadKey();
}

void Singleplayer()
{
    var setupBot = false;
    IGameBot bot = new GameBot(Difficulty.Easy, true);

    while (!setupBot)
    {
        Console.WriteLine("Easy (1), Normal (2) or Hard (3)?");

        var input = Console.ReadLine();

        if (!Int32.TryParse(input, out var result))
        {
            Console.Clear();
            Console.WriteLine("Please enter a number.");
            continue;
        }
        
        if (!(result > 0 && result < 4))
        {
            Console.Clear();
            Console.WriteLine("Please enter a number between 1 and 3.");
            continue;
        }

        if (result == 1)
        {
            setupBot = true;
        }
        else if (result == 2)
        {
            bot = new GameBot(Difficulty.Normal, true);
            setupBot = true;
        }
        else
        {
            bot = new GameBot(Difficulty.Hard, true);
            setupBot = true;
        }
    }

    PrintField(engine.Field);

    while (!won)
    {
        if (!playerOneTurn)
        {
            Console.WriteLine("Player two turn.");
        }

        try
        {
            if (!TryOnePlayerRound(bot, playerOneTurn)) continue;

            Console.Clear();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
            continue;
        }

        if (engine.PlayerOneHasWon())
        {
            Console.WriteLine("Player one won.");
            won = true;
        }

        if (engine.PlayerTwoHasWon())
        {
            Console.WriteLine("Player two won.");
            won = true;
        }

        PrintField(engine.Field);
        playerOneTurn = !playerOneTurn;
    }
}

void Multiplayer()
{
    PrintField(engine.Field);

    while (!won)
    {
        if (playerOneTurn)
            Console.WriteLine("Player one turn.");
        else
            Console.WriteLine("Player two turn.");

        try
        {
            if (!TryTwoPlayerRound(playerOneTurn)) continue;

            Console.Clear();
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine(ex.Message);
            continue;
        }

        if (engine.PlayerOneHasWon())
        {
            Console.WriteLine("Player one won.");
            won = true;
        }

        if (engine.PlayerTwoHasWon())
        {
            Console.WriteLine("Player two won.");
            won = true;
        }

        PrintField(engine.Field);
        playerOneTurn = !playerOneTurn;
    }
}

bool TryOnePlayerRound(IGameBot bot, bool playerOneTurn)
{
    if (playerOneTurn)
    {
        engine.SetPlayerOnePosition(bot.CalculateBotPosition(engine.Field));
    }
    else
    {
        var input = Console.ReadLine();

        if (!Int32.TryParse(input, out var result))
            return false;

        engine.SetPlayerTwoPosition(result);
    }

    return true;
}

bool TryTwoPlayerRound(bool playerOneTurn)
{
    var input = Console.ReadLine();

    if (!Int32.TryParse(input, out var result))
        return false;

    if (playerOneTurn)
        engine.SetPlayerOnePosition(result);
    else
        engine.SetPlayerTwoPosition(result);

    return true;
}

void PrintField(char[][] field)
{
    for (var i = 0; i < field.Length; i++)
    {
        for (var j = 0; j < field[i].Length; j++)
        {
            Console.Write($" {field[i][j]} ");
        }
        Console.Write("\n");
    }
    Console.WriteLine(" 1  2  3  4  5  6  7  8");
}