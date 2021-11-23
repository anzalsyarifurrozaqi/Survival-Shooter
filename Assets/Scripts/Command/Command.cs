
public abstract class Command {
    public abstract void Execute();
    public abstract void UnExecute();
}

public class MoveCommand : Command {
    PlayerMovement PlayerMovement;
    float H, V;
    public MoveCommand(PlayerMovement playerMovement, float h, float v) {
        this.PlayerMovement = playerMovement;
        this.H = h;
        this.V = v;
    }
    public override void Execute() {
        PlayerMovement.Move(H, V);

        PlayerMovement.Animating(H, V);
    }

    public override void UnExecute() {
        PlayerMovement.Move(-H, -V);
    }
}

public class TurnCommand : Command {
    public PlayerMovement PlayerMovement;
    public TurnCommand(PlayerMovement playerMovement) {
        PlayerMovement = playerMovement;
    }
    public override void Execute() {
        PlayerMovement.Turning();
    }

    public override void UnExecute() {
        
    }
}

public class ShootCommand : Command {
    PlayerShooting PlayerShooting;
    public ShootCommand(PlayerShooting playerShooting) {
        PlayerShooting = playerShooting;
    }
    public override void Execute() {
        PlayerShooting.Shoot();
    }

    public override void UnExecute() {
        
    }
}
