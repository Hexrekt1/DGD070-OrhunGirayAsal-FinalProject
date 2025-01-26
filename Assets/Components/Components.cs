using Entitas;
using UnityEngine;

public class PlayerMovementComponent : IComponent
{
    public float speed;
    public Vector2 direction; // The direction vector (WASD input)
}

public class PositionComponent : IComponent
{
    public Vector2 position;
}


public class PadComponent : IComponent
{
    public bool touched;  // This will track if the pad has been touched
    public Color color;   // The color of the pad (used for changing color to red)
}
public class InputComponent : IComponent
{
    public bool moveUp;
    public bool moveDown;
    public bool moveLeft;
    public bool moveRight;
}

public class WinConditionComponent : IComponent
{
    public int padsTouchedCount;
}
public class ViewComponent : IComponent
{
    public GameObject value;  // Store reference to the GameObject
}

public class GameStateService
{
    private static GameStateService _instance;
    public static GameStateService Instance => _instance ?? (_instance = new GameStateService());

    public bool IsGameWon { get; private set; }

    private GameStateService() { }

    public void SetGameWon(bool won)
    {
        IsGameWon = won;
    }
}





