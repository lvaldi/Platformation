using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Crosshair : MonoBehaviour {

	public delegate void OnPlayerKill();
	public OnPlayerKill onPlayerKill;

	public float speed = 0.5f;
	[SerializeField]
	private float _previousShotTime = 0.0f;
	[SerializeField]
	private float _shotCooldownTime = 1.0f;
	[SerializeField]
    private float _bulletTravelDelay = 3f;
    [SerializeField]
    private float _startDelay = 5.0f;
    private IEnumerator coroutine;
    private bool canShoot;

    private GameObject _chosenPlatform;
	[Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _killCountTextMesh;
    [SerializeField]
    private SpriteRenderer _previewPlatformSpriteRenderer;

    [Header("Platform Prefabs")]
    [SerializeField]
    private GameObject[] _platformPrefabs;
	[SerializeField]
    private GameObject[] _trapPrefabs;

    private int _killCount;

    public void moveCrossHair(Vector2 input) 
	{
		float x = transform.position.x + input.x*speed;
		float y = transform.position.y + input.y*speed;
		transform.position = new Vector3(x, y, 0);
	}

	private IEnumerator DelayShot(float waitTime, Vector2 posBeforeDelay) 
	{
		yield return new WaitForSeconds(waitTime);
		Collider2D colliderHit = Physics2D.OverlapCircle (posBeforeDelay, 1f, LayerMask.GetMask ("Player"));
		Collider2D safeAreaHit = Physics2D.OverlapCircle (posBeforeDelay, 1f, LayerMask.GetMask ("SafeArea"));

		if (colliderHit != null && safeAreaHit == null) {
            OnHitPlayer(colliderHit);
        } 
		else 
		{
			// Nothing happens
		}
	}

	private void OnHitPlayer(Collider2D colliderHit)
	{
		CreatePlatform (colliderHit.gameObject);
        _killCount++;

        UpdateKillCount();
    }

	public void Shoot() 
	{
		Vector2 posBeforeDelay = transform.position;
		coroutine = DelayShot(_bulletTravelDelay,posBeforeDelay);
		StartCoroutine(coroutine);
		
	}

	public void AttemptShot() 
	{
		if(!canShoot)
            return;

		_previousShotTime = Time.time - _previousShotTime ;
		if(_previousShotTime >= _shotCooldownTime) 
		{
			Shoot();
		}
	}

	public void DelayStart(){
		Invoke ("setCanShootTrue", _startDelay);
	}

	void setCanShootTrue() {
		canShoot = true;
	}

	public void CreatePlatform(GameObject player) 
	{
		Vector3 spawnPosition = new Vector3 (player.transform.position.x, player.transform.position.y, 0);
		GameObject obj = Instantiate(_chosenPlatform, spawnPosition, Quaternion.identity);
        Platform platformComponent = obj.GetComponent<Platform>();
        platformComponent.Init();

		onPlayerKill ();
	}

	public void DesignatePlatformIndex()
	{
        int index;
        int r = (int)Random.Range(0, 10) % 2;
		if (r == 0) {
			index = (int)Random.Range(0, _platformPrefabs.Length);
            _chosenPlatform = _platformPrefabs[index];
        }
		else 
		{
            index = (int)Random.Range(0, _trapPrefabs.Length);
            _chosenPlatform = _trapPrefabs[index];
        }

        SetupPreviewUI(_chosenPlatform.GetComponent<SpriteRenderer>().sprite);
    }

	private void SetupPreviewUI(Sprite previewSprite)
	{
        //print(previewSprite);
        _previewPlatformSpriteRenderer.sprite = previewSprite;
    }

	public void UpdateKillCount()
	{
		_killCountTextMesh.text = "Kills: " + _killCount;
	}

	public void ResetKillCount()
	{
        _killCount = 0;
    }
}
