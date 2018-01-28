



public abstract class Trap : Platform
{

    protected void KillPlayer()
    {
        GameController.instance.endRound();
    }

    protected void Ice() {
        GameController.instance.slowPlayer();
    }
}