using UnityEngine;
using System.Collections.Generic;
using Entitas;
public class WinConditionSystem : IExecuteSystem
{
    private readonly IGroup<GameEntity> _players;

    public WinConditionSystem(Contexts contexts)
    {
        _players = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.WinCondition));
    }

    public void Execute()
    {
        foreach (var player in _players)
        {
            if (player.winCondition.padsTouchedCount == 4)
            {
                // Trigger win screen or player destruction
                DisplayWinScreen();
                player.Destroy(); // Or stop player movement
                break; // Ensure only one win condition is checked at a time
            }
        }
    }

    private void DisplayWinScreen()
    {
        Debug.Log("You Win!");
    }
}






