



public abstract class Trap : Platform
{

    protected void KillPlayer()
    {
        GameController.instance.endRound();
    }
}