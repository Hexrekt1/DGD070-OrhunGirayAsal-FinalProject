using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject playerPrefab;  // Drag your player prefab here in the inspector
    public GameObject padPrefab;     // Drag your pad prefab here in the inspector

    private InputService _inputService;
    private PlayerMovementSystem _playerMovementSystem;
    private PadCollisionSystem _padCollisionSystem;
    private WinConditionSystem _winConditionSystem;

    private void Start()
    {
        _inputService = new InputService();

        var contexts = Contexts.sharedInstance;
        _playerMovementSystem = new PlayerMovementSystem(contexts, _inputService);
        _padCollisionSystem = new PadCollisionSystem(contexts);
        _winConditionSystem = new WinConditionSystem(contexts);

        // Create player and pads
        CreatePlayer(contexts);
        CreatePads(contexts);
    }

    private void Update()
{
    _inputService.UpdateInput();  // Ensure input is updated here

    _playerMovementSystem.Execute();
    _padCollisionSystem.Execute();
    _winConditionSystem.Execute();
}



 private void CreatePlayer(Contexts contexts)
{
    var playerEntity = contexts.game.CreateEntity();
    
    // Add PositionComponent and other relevant components
    playerEntity.AddPosition(new Vector2(0, 0)); // Initial position at the center
    playerEntity.AddPlayerMovement(5.0f, Vector2.zero);  // Speed and initial direction
    playerEntity.AddInput(false, false, false, false);
    playerEntity.AddWinCondition(0); // No pads touched at the start

    // Create the visual representation (GameObject)
    var player = Instantiate(playerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    player.name = "Player";
    playerEntity.AddView(player); // Associate this visual GameObject with the player entity
}






    private void CreatePads(Contexts contexts)
{
    // Set minimum distance between pads
    float minDistance = 2.0f;

    for (int i = 0; i < 4; i++)
    {
        var padEntity = contexts.game.CreateEntity();

        Vector2 padPosition;
        bool positionIsValid;
        do
        {
            positionIsValid = true;
            // Generate random positions for pads, ensuring they stay within screen bounds
            padPosition = new Vector2(
                Random.Range(0.1f, 0.9f), // X between 10% and 90% of screen width
                Random.Range(0.1f, 0.9f)  // Y between 10% and 90% of screen height
            );

            // Convert pad position to world space (from screen space)
            padPosition = Camera.main.ViewportToWorldPoint(new Vector3(padPosition.x, padPosition.y, 0));

            // Check the distance between the new pad and already placed pads
            foreach (var existingPad in contexts.game.GetGroup(GameMatcher.Position))
            {
                if (Vector2.Distance(padPosition, existingPad.position.position) < minDistance)
                {
                    positionIsValid = false;
                    break;  // Stop checking if the position is too close
                }
            }

        } while (!positionIsValid);  // Retry until valid position is found

        // Add the pad's position to the entity
        padEntity.AddPosition(padPosition);
        padEntity.AddPad(false, Color.white); // Initially, not touched

        // Create the visual representation (GameObject)
        var pad = Instantiate(padPrefab, padPosition, Quaternion.identity);
        pad.name = "Pad_" + i;
        padEntity.AddView(pad); // Associate this visual GameObject with the pad entity
    }
}

}
