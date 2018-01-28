using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour {
	
	public float speed = 0.5f;
	[SerializeField]
	private float _previousShotTime = 0.0f;
	[SerializeField]
	private float _shotCooldownTime = 1.0f;
	[SerializeField]
    private float _bulletTravelDelay = 0.8f;
    [SerializeField]
    private float _startDelay = 5.0f;
    private IEnumerator coroutine;
    private bool canShoot;

    [SerializeField]
    private GameObject[] _platformPrefabs;



    public void moveCrossHair(Vector2 input) 
	{
		float x = transform.position.x + input.x*speed;
		float y = transform.position.y + input.y*speed;
		transform.position = new Vector3(x, y, 0);
	}

	private IEnumerator DelayShot(float waitTime) 
	{
		yield return new WaitForSeconds(waitTime);
	}

	public void Shoot() 
	{
		coroutine = DelayShot(_bulletTravelDelay);
		StartCoroutine(coroutine);
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.forward, out hit)) 
		{
			if (hit.collider.tag == "Player") 
			{
				CreatePlatform();
			}
		}
	}

	public void AttemptShot() 
	{
		if(!canShoot)
            return;

        _previousShotTime = _previousShotTime - Time.time;
		if(_previousShotTime >= _shotCooldownTime) 
		{
			Shoot();
		}
	}

	private void DelayStart() 
	{
		IEnumerator startDelay = DelayShot(_startDelay);
		StartCoroutine(startDelay);
        canShoot = true;
    }

	public void CreatePlatform() 
	{
		GameObject obj = Instantiate(_platformPrefabs[0], this.transform);
        Platform platformComponent = obj.GetComponent<Platform>();
        platformComponent.Init();
	}

}
