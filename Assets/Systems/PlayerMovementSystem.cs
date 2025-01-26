using Entitas;
using UnityEngine;


public class PlayerMovementSystem : IExecuteSystem
{
    private readonly Contexts _contexts;
    private readonly InputService _inputService;

    private float _minX, _maxX, _minY, _maxY;

    public PlayerMovementSystem(Contexts contexts, InputService inputService)
    {
        _contexts = contexts;
        _inputService = inputService;

        // Define the screen boundaries
        _minX = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).x;
        _maxX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
        _minY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0)).y;
        _maxY = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
    }

    public void Execute()
    {
        // Find the player entity
        foreach (var player in _contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.PlayerMovement, GameMatcher.View)))
        {
            // Ensure the position component is valid
            if (player.hasPosition && player.hasView)
            {
                Vector2 currentPosition = player.position.position; // Access position directly

                // Move player based on input
                if (_inputService.moveUp)
                {
                    currentPosition += new Vector2(0, 1) * player.playerMovement.speed * Time.deltaTime;
                }
                if (_inputService.moveDown)
                {
                    currentPosition += new Vector2(0, -1) * player.playerMovement.speed * Time.deltaTime;
                }
                if (_inputService.moveLeft)
                {
                    currentPosition += new Vector2(-1, 0) * player.playerMovement.speed * Time.deltaTime;
                }
                if (_inputService.moveRight)
                {
                    currentPosition += new Vector2(1, 0) * player.playerMovement.speed * Time.deltaTime;
                }

                // Clamp the player's position to the screen bounds
                currentPosition.x = Mathf.Clamp(currentPosition.x, _minX, _maxX);
                currentPosition.y = Mathf.Clamp(currentPosition.y, _minY, _maxY);

                // Apply the new position
                player.ReplacePosition(currentPosition);

                // Update the visual GameObject's position (world space)
                player.view.value.transform.position = new Vector3(currentPosition.x, currentPosition.y, 0);

                // Debug log to check if position is being updated
                // Debug.Log($"Player Position Updated: {currentPosition}");
            }
        }
    }
}
















