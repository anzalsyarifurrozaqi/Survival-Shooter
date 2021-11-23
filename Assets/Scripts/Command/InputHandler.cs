using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {
    public PlayerMovement PlayerMovement;
    public PlayerShooting PlayerShooting;

    Queue<Command> commands = new Queue<Command>();

    private void FixedUpdate() {
        Command moveCommand = InputMovementHandling();
        if (moveCommand != null) {
            commands.Enqueue(moveCommand);

            moveCommand.Execute();
        }

        Command turnCommand = new TurnCommand(PlayerMovement);
        turnCommand.Execute();
    }

    private void Update() {
        Command shootCommand = InputShootHandling();

        if (shootCommand != null) {
            shootCommand.Execute();
        }
    }

    Command InputMovementHandling() {
        if (Input.GetKey(KeyCode.D)) {
            return new MoveCommand(PlayerMovement, 1, 0);
        }
        else if (Input.GetKey(KeyCode.A)) {
            return new MoveCommand(PlayerMovement, -1, 0);
        }
        else if (Input.GetKey(KeyCode.W)) {
            return new MoveCommand(PlayerMovement, 0, 1);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            return new MoveCommand(PlayerMovement, 0, -1);
        }
        else if (Input.GetKey(KeyCode.Z)) {
            return Undo();
        }
        else {
            return new MoveCommand(PlayerMovement, 0, 0);
        }
    }

    Command Undo() {
        if (commands.Count > 0) {
            Command undoCommand = commands.Dequeue();
            undoCommand.UnExecute();
        }

        return null;
    }

    Command InputShootHandling() {
        if (Input.GetButtonDown("Fire1")) {
            return new ShootCommand(PlayerShooting);
        }
        else {
            return null;
        }
    }    
}
