using Entitas;
using UnityEngine;

public class PadCollisionSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _pads;
    private readonly IGroup<GameEntity> _players;

    public PadCollisionSystem(Contexts contexts)
    {
        _pads = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.Pad));
        _players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.PlayerMovement, GameMatcher.WinCondition));
    }

    public void Execute()
    {
        foreach (var player in _players)
        {
            // Check collision with each pad
            foreach (var pad in _pads)
            {
                if (IsPlayerTouchingPad(player.position.position, pad.position.position) && !pad.pad.touched)
                {
                    // Change pad's color to red
                    if (pad.hasView)
                    {
                        var renderer = pad.view.value.GetComponent<Renderer>();
                        if (renderer != null)
                        {
                            renderer.material.color = Color.red; // Change color to red
                        }
                    }

                    // Mark pad as touched
                    pad.ReplacePad(true, Color.red); // Update pad state to indicate it's touched

                    // Update player's win condition: increment the touched pads count
                    player.ReplaceWinCondition(player.winCondition.padsTouchedCount + 1); 

                    // Stop further processing of this pad
                    break; // Move to the next pad check after a valid touch
                }
            }
        }
    }

    // Simple AABB (Axis-Aligned Bounding Box) collision check
    private bool IsPlayerTouchingPad(Vector2 playerPos, Vector2 padPos)
    {
        // Adjust this distance for your preferred collision detection logic
        float distance = 0.5f;
        return Vector2.Distance(playerPos, padPos) < distance;
    }
}
