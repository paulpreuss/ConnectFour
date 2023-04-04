using Azubi.ConnectFour.Abstracts;

namespace Azubi.ConnectFour;

public class GameBot : IGameBot
{
    private readonly Difficulty _difficulty;
    private readonly char _botSymbol;
    private readonly char _enemySymbol;
    private readonly Random _random;

    public GameBot(Difficulty difficulty, bool isPlayerOne)
    { 
        _difficulty = difficulty;
        _botSymbol = isPlayerOne ? 'X' : 'O';
        _enemySymbol = isPlayerOne ? 'O' : 'X';
        _random = new Random();
    }

    public int CalculateBotPosition(char[][] field)
    {
        if (_difficulty == Difficulty.Easy)
        {
            return CalculateEasyBotPosition();
        }
        else if (_difficulty == Difficulty.Normal)
        {
            return CalculateNormalBotPosition(field);
        }
        else
        {
            return CalculateHardBotPosition(field);
        }
    }

    private int CalculateEasyBotPosition()
    {
        return _random.Next(1, 8);
    }

    private int CalculateNormalBotPosition(char[][] field)
    {
        if (MoveToFour(field) > -1) 
            return MoveToFour(field) + 1;
        if (MoveToThree(field) > -1)
            return MoveToThree(field) + 1;
        if (MoveToTwo(field) > -1)
            return MoveToTwo(field) + 1;

        return CalculateEasyBotPosition();
    }

    private int CalculateHardBotPosition(char[][] field)
    {
        if (MoveToFour(field) > -1)
            return MoveToFour(field) + 1;
        if (EnemyMoveToFour(field) > -1)
            return EnemyMoveToFour(field) + 1;

        return CalculateNormalBotPosition(field);
    }

    private int EnemyMoveToFour(char[][] field)
    {
        for (var i = 0; i < field.Length; i++)
        {
            for (var j = 0; j < field[i].Length; j++)
            {
                if (field[i][j] != _enemySymbol)
                    continue;
                if (EnemyMoveHorizontallyToFour(field, j, i) > 0)
                    return EnemyMoveHorizontallyToFour(field, j, i);
                if (EnemyMoveVerticallyToFour(field, j, i) > 0)
                    return EnemyMoveVerticallyToFour(field, j, i);
                if (EnemyMoveDiagonallyToFour(field, j, i) > 0)
                    return EnemyMoveDiagonallyToFour(field, j, i);
            }
        }

        return -1;
    }

    private int MoveToFour(char[][] field)
    {
        for (var i = 0; i < field.Length; i++)
        {
            for (var j = 0; j < field[i].Length; j++)
            {
                if (field[i][j] != _botSymbol)
                    continue;
                if (MoveHorizontallyToFour(field, j, i) > 0)
                    return MoveHorizontallyToFour(field, j, i);
                if (MoveVerticallyToFour(field, j, i) > 0)
                    return MoveVerticallyToFour(field, j, i);
                if (MoveDiagonallyToFour(field, j, i) > 0)
                    return MoveDiagonallyToFour(field, j, i);
            }
        }

        return -1;
    }

    private int MoveToThree(char[][] field)
    {
        for (var i = 0; i < field.Length; i++)
        {
            for (var j = 0; j < field[i].Length; j++)
            {
                if (field[i][j] != _botSymbol)
                    continue;
                if (MoveHorizontallyToThree(field, j, i) > 0)
                    return MoveHorizontallyToThree(field, j, i);
                if (MoveVerticallyToThree(field, j, i) > 0)
                    return MoveVerticallyToThree(field, j, i);
                if (MoveDiagonallyToThree(field, j, i) > 0)
                    return MoveDiagonallyToThree(field, j, i);
            }
        }

        return -1;
    }

    private int MoveToTwo(char[][] field)
    {
        for (var i = 0; i < field.Length; i++)
        {
            for (var j = 0; j < field[i].Length; j++)
            {
                if (field[i][j] != _botSymbol)
                    continue;
                if (MoveHorizontallyToTwo(field, j, i) > 0)
                    return MoveHorizontallyToTwo(field, j, i);
                if (MoveVerticallyToTwo(field, j, i) > 0)
                    return MoveVerticallyToTwo(field, j, i);
                if (MoveDiagonallyToTwo(field, j, i) > 0)
                    return MoveDiagonallyToTwo(field, j, i);
            }
        }

        return -1;
    }

    private int EnemyMoveHorizontallyToFour(char[][] field, int x, int y)
    {
        if (x < 5)
        {
            if (field[y][x] == _enemySymbol && field[y][x + 1] == _enemySymbol
                && field[y][x + 2] == _enemySymbol && field[y][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 3, y))
                    return x + 3;
        }
        else
        {
            if (field[y][x] == _enemySymbol && field[y][x - 1] == _enemySymbol
                && field[y][x - 2] == _enemySymbol && field[y][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 3, y))
                    return x - 3;
        }

        return -1;
    }

    private int EnemyMoveVerticallyToFour(char[][] field, int x, int y)
    {
        if (y > 4)
        {
            if (field[y][x] == _enemySymbol && field[y - 1][x] == _enemySymbol
                && field[y - 2][x] == _enemySymbol && field[y - 3][x] == '-')
                return x;
        }

        return -1;
    }

    private int EnemyMoveDiagonallyToFour(char[][] field, int x, int y)
    {
        if (x < 5 && y < 5)
        {
            if (field[y][x] == _enemySymbol && field[y + 1][x + 1] == _enemySymbol
                && field[y + 2][x + 2] == _enemySymbol && field[y + 3][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 3, y + 3))
                    return x + 3;
        }
        else if (x > 4 && y < 5)
        {
            if (field[y][x] == _enemySymbol && field[y + 1][x - 1] == _enemySymbol
               && field[y + 2][x - 2] == _enemySymbol && field[y + 3][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 3, y + 3))
                    return x - 3;
        }
        else if (x < 5 && y > 4)
        {
            if (field[y][x] == _enemySymbol && field[y - 1][x + 1] == _enemySymbol
                && field[y - 2][x + 2] == _enemySymbol && field[y - 3][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 3, y - 3))
                    return x + 3;
        }

        else
        {
            if (field[y][x] == _enemySymbol && field[y - 1][x - 1] == _enemySymbol
                && field[y - 2][x - 2] == _enemySymbol && field[y - 3][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 3, y - 3))
                    return x - 3;
        }

        return -1;
    }

    private int MoveHorizontallyToFour(char[][] field, int x, int y)
    {
        if (x < 5)
        {
            if (field[y][x] == _botSymbol && field[y][x + 1] == _botSymbol
                && field[y][x + 2] == _botSymbol && field[y][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 3, y))
                    return x + 3;
        }
        else
        {
            if (field[y][x] == _botSymbol && field[y][x - 1] == _botSymbol
                && field[y][x - 2] == _botSymbol && field[y][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 3, y))
                    return x - 3;
        }

        return -1;
    }

    private int MoveVerticallyToFour(char[][] field, int x, int y)
    {
        if (y > 4)
        {
            if (field[y][x] == _botSymbol && field[y - 1][x] == _botSymbol
                && field[y - 2][x] == _botSymbol && field[y - 3][x] == '-')
                return x;
        }

        return -1;
    }

    private int MoveDiagonallyToFour(char[][] field, int x, int y)
    {
        if (x < 5 && y < 5)
        {
            if (field[y][x] == _botSymbol && field[y + 1][x + 1] == _botSymbol
                && field[y + 2][x + 2] == _botSymbol && field[y + 3][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 3, y + 3))
                    return x + 3;
        }
        else if (x > 4 && y < 5)
        {
            if (field[y][x] == _botSymbol && field[y + 1][x - 1] == _botSymbol
               && field[y + 2][x - 2] == _botSymbol && field[y + 3][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 3, y + 3))
                    return x - 3;
        }
        else if (x < 5 && y > 4)
        {
            if (field[y][x] == _botSymbol && field[y - 1][x + 1] == _botSymbol
                && field[y - 2][x + 2] == _botSymbol && field[y - 3][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 3, y - 3))
                    return x + 3;
        }

        else
        {
            if (field[y][x] == _botSymbol && field[y - 1][x - 1] == _botSymbol
                && field[y - 2][x - 2] == _botSymbol && field[y - 3][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 3, y - 3))
                    return x - 3;
        }

        return -1;
    }

    private int MoveHorizontallyToThree(char[][] field, int x, int y)
    {
        if (x < 5)
        {
            if (field[y][x] == _botSymbol && field[y][x + 1] == _botSymbol
                && field[y][x + 2] == '-' && field[y][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 2, y))
                    return x + 2;
        }
        else
        {
            if (field[y][x] == _botSymbol && field[y][x - 1] == _botSymbol
                && field[y][x - 2] == '-' && field[y][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 2, y))
                    return x - 2;
        }

        return -1;
    }

    private int MoveVerticallyToThree(char[][] field, int x, int y)
    {
        if (y > 4)
        {
            if (field[y][x] == _botSymbol && field[y - 1][x] == _botSymbol
                && field[y - 2][x] == '-' && field[y - 3][x] == '-')
                return x;
        }

        return -1;
    }

    private int MoveDiagonallyToThree(char[][] field, int x, int y)
    {
        if (x < 5 && y < 5)
        {
            if (field[y][x] == _botSymbol && field[y + 1][x + 1] == _botSymbol
                && field[y + 2][x + 2] == '-' && field[y + 3][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 2, y + 2))
                    return x + 2;
        }
        else if (x > 4 && y < 5)
        {
            if (field[y][x] == _botSymbol && field[y + 1][x - 1] == _botSymbol
               && field[y + 2][x - 2] == '-' && field[y + 3][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 2, y + 2))
                    return x - 2;
        }
        else if (x < 5 && y > 4)
        {
            if (field[y][x] == _botSymbol && field[y - 1][x + 1] == _botSymbol
                && field[y - 2][x + 2] == '-' && field[y - 3][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 2, y - 2))
                    return x + 2;
        }

        else
        {
            if (field[y][x] == _botSymbol && field[y - 1][x - 1] == _botSymbol
                && field[y - 2][x - 2] == '-' && field[y - 3][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 2, y - 2))
                    return x - 2;
        }

        return -1;
    }

    private int MoveHorizontallyToTwo(char[][] field, int x, int y)
    {
        if (x < 5)
        {
            if (field[y][x] == _botSymbol && field[y][x + 1] == '-'
                && field[y][x + 2] == '-' && field[y][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 1, y))
                    return x + 1;
        }
        else
        {
            if (field[y][x] == _botSymbol && field[y][x - 1] == '-'
                && field[y][x - 2] == '-' && field[y][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 1, y))
                    return x - 1;
        }

        return -1;
    }

    private int MoveVerticallyToTwo(char[][] field, int x, int y)
    {
        if (y > 4)
        {
            if (field[y][x] == _botSymbol && field[y - 1][x] == '-'
                && field[y - 2][x] == '-' && field[y - 3][x] == '-')
                return x;
        }

        return -1;
    }

    private int MoveDiagonallyToTwo(char[][] field, int x, int y)
    {
        if (x < 5 && y < 5)
        {
            if (field[y][x] == _botSymbol && field[y + 1][x + 1] == '-'
                && field[y + 2][x + 2] == '-' && field[y + 3][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 1, y + 1))
                    return x + 1;
        }
        else if (x > 4 && y < 5)
        {
            if (field[y][x] == _botSymbol && field[y + 1][x - 1] == '-'
               && field[y + 2][x - 2] == '-' && field[y + 3][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 1, y + 1))
                    return x - 1;
        }
        else if (x < 5 && y > 4)
        {
            if (field[y][x] == _botSymbol && field[y - 1][x + 1] == '-'
                && field[y - 2][x + 2] == '-' && field[y - 3][x + 3] == '-')
                if (FieldExistsUnderMovePosition(field, x + 1, y - 1))
                    return x + 1;
        }

        else
        {
            if (field[y][x] == _botSymbol && field[y - 1][x - 1] == '-'
                && field[y - 2][x - 2] == '-' && field[y - 3][x - 3] == '-')
                if (FieldExistsUnderMovePosition(field, x - 1, y - 1))
                    return x - 1;
        }

        return -1;
    }

    private bool FieldExistsUnderMovePosition(char[][] field, int x, int y)
    {
        if (y > 6) return true;
        if (field[y + 1][x] != '-') return true;

        return false;
    }
}
