using Azubi.ConnectFour.Abstracts;

namespace Azubi.ConnectFour;

public class GameEngine : IGameEngine
{
    private const char PlayerOneSymbol = 'X';
    private const char PlayerTwoSymbol = 'O';
    public char[][] Field { get; init; }

    public GameEngine()
    {
        Field = GenerateEmptyField();
    }

    public void SetPlayerOnePosition(int position)
    {
        SetPosition(position, PlayerOneSymbol);
    }
    public void SetPlayerTwoPosition(int position)
    {
        SetPosition(position, PlayerTwoSymbol);
    }

    public bool PlayerOneHasWon()
    {
        return PlayerHasWon(PlayerOneSymbol);
    }

    public bool PlayerTwoHasWon()
    {
        return PlayerHasWon(PlayerTwoSymbol);
    }

    private void SetPosition(int position, char symbol)
    {
        var index = position - 1;
        var height = GetHeight(index);

        Field[height][index] = symbol;
    }

    private int GetHeight(int index)
    {
        if (index < 0 || index > 7)
            throw new InvalidOperationException("Please give a number between 1 and 8.");

        for (var i = 0; i < Field.Length; i++)
        {
            for (var j = 0;  j < Field[i].Length; j++)
            {
                if (FieldIsInBounds(index, i, j)) return i;
            }
        }

        throw new InvalidOperationException("Please give a number between 1 and 8.");
    }

    private bool FieldIsInBounds(int index, int i, int j)
    {
        if (i < 7)
        {
            if (j == index)
            {
                if (i == 0)
                    if (Field[i][j] != '-')
                        throw new InvalidOperationException("No space in this column.");
                if (Field[i + 1][j] == '-')
                    return false;
                else
                    return true;
            }
        }
        else
        {
            return true;
        }

        return false;
    }

    private bool PlayerHasWon(char symbol)
    {
        bool won = false;

        for (var i = 0; i < Field.Length; i++)
        {
            for (var j = 0; j < Field[i].Length; j++)
            { 
                if (PlayerHasWonHorizontally(symbol, j, i)) won = true;
                if (PlayerHasWonVertically(symbol, j, i)) won = true;
                if (PlayerHasWonDiagonally(symbol, j, i)) won = true;
            }
        }

        return won;
    }

    private bool PlayerHasWonHorizontally(char symbol, int x, int y)
    {
        if (x < 5)
        {
            if (Field[y][x] == symbol && Field[y][x + 1] == symbol 
                && Field[y][x + 2] == symbol && Field[y][x + 3] == symbol)
                return true;
        }
        else
        {
            if (Field[y][x] == symbol && Field[y][x - 1] == symbol
                && Field[y][x - 2] == symbol && Field[y][x - 3] == symbol)
                return true;
        }

        return false;
    }

    private bool PlayerHasWonVertically(char symbol, int x, int y)
    {
        if (y < 5)
        {
            if (Field[y][x] == symbol && Field[y + 1][x] == symbol
                && Field[y + 2][x] == symbol && Field[y + 3][x] == symbol)
                return true;
        }
        else
        {
            if (Field[y][x] == symbol && Field[y - 1][x] == symbol
                && Field[y - 2][x] == symbol && Field[y - 3][x] == symbol)
                return true;
        }

        return false;
    }

    private bool PlayerHasWonDiagonally(char symbol, int x, int y)
    {
        if (x < 5 && y < 5)
        {
            if (Field[y][x] == symbol && Field[y + 1][x + 1] == symbol
                && Field[y + 2][x + 2] == symbol && Field[y + 3][x + 3] == symbol)
                return true;
        }
        else if (x > 4 && y < 5)
        {
            if (Field[y][x] == symbol && Field[y + 1][x - 1] == symbol
               && Field[y + 2][x - 2] == symbol && Field[y + 3][x - 3] == symbol)
                return true;
        }
        else if (x < 5 && y > 4)
        {
            if (Field[y][x] == symbol && Field[y - 1][x + 1] == symbol
                && Field[y - 2][x + 2] == symbol && Field[y - 3][x + 3] == symbol)
                return true;
        }

        else
        {
            if (Field[y][x] == symbol && Field[y - 1][x - 1] == symbol
                && Field[y - 2][x - 2] == symbol && Field[y - 3][x - 3] == symbol)
                return true;
        }

        return false;
    }

    private char[][] GenerateEmptyField()
    {
        var field = new char[8][];
        for (var i = 0; i < field.Length; i++)
        {
            field[i] = new char[8];
            for (var j = 0; j < field[i].Length; j++)
            {
                field[i][j] = '-';
            }
        }

        return field;
    }
}
