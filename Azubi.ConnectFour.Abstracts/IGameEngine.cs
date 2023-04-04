namespace Azubi.ConnectFour.Abstracts;

public interface IGameEngine
{
    char[][] Field { get; init; }
    void SetPlayerOnePosition(int position);
    void SetPlayerTwoPosition(int position);
    bool PlayerOneHasWon();
    bool PlayerTwoHasWon();
}