using UnityEngine;

public class InputService
{
    public bool moveUp { get; private set; }
    public bool moveDown { get; private set; }
    public bool moveLeft { get; private set; }
    public bool moveRight { get; private set; }

    public void UpdateInput()
    {
        moveUp = Input.GetKey(KeyCode.W);
        moveDown = Input.GetKey(KeyCode.S);
        moveLeft = Input.GetKey(KeyCode.A);
        moveRight = Input.GetKey(KeyCode.D);

        //Debug.Log($"Input Update: Up={moveUp}, Down={moveDown}, Left={moveLeft}, Right={moveRight}");
    }
}





