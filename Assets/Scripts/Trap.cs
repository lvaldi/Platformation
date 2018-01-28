



public abstract class Trap : Platform
{

	public override void Init()
	{
		base.Init();

		_collider.isTrigger = true;
	}

    protected void KillPlayer()
    {
        GameController.instance.endRound();
    }
}