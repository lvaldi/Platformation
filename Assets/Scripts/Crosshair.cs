﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private float _startDelay = 0f;
    private IEnumerator coroutine;
    private bool canShoot;

	[SerializeField]
	private GameObject bullet;

    private GameObject _chosenPlatform;
	[Header("UI")]
    [SerializeField]
    private TextMeshProUGUI _killCountTextMesh;
    [SerializeField]
    private Image _previewPlatformImage;

    [Header("Platform Prefabs")]
    [SerializeField]
    private GameObject[] _platformPrefabs;
	[SerializeField]
    private GameObject[] _trapPrefabs;
    [SerializeField]
    private GameObject _bombPrefab;

	[SerializeField]
	GameObject splatterPrefab;

	private List<GameObject> splatterList;

    private int _killCount;

    private float _crosshairRadius;

    void Start()
	{
        _crosshairRadius = this.GetComponent<SpriteRenderer>().bounds.size.x / 2f;

		splatterList = new List<GameObject> ();

		for (int i = 0; i < 10f; ++i) {
			GameObject obj = Instantiate (splatterPrefab);
			obj.GetComponent<SpriteRenderer> ().enabled = false;

			splatterList.Add(obj);

		}
    }

    public void moveCrossHair(Vector2 input) 
	{
		float x = transform.position.x + input.x*speed;
		float y = transform.position.y + input.y*speed;
		transform.position = new Vector3(x, y, 0);
	}

	private IEnumerator DelayShot(float waitTime, Vector2 posBeforeDelay) 
	{
		yield return new WaitForSeconds(waitTime);
		Collider2D colliderHit = Physics2D.OverlapCircle (posBeforeDelay, _crosshairRadius, LayerMask.GetMask ("Player"));
		Collider2D safeAreaHit = Physics2D.OverlapCircle (posBeforeDelay, _crosshairRadius, LayerMask.GetMask ("SafeArea"));

		if (colliderHit != null && safeAreaHit == null) {
            OnHitPlayer(colliderHit);
        } 
		else 
		{
			// Nothing happens
		}

		AudioController.instance.PLAY (AUDIO.WALL_IMPACT);
		GameObject splatter = GetSplatter ();

		splatter.transform.position = new Vector3 (posBeforeDelay.x, posBeforeDelay.y, 0);
		splatter.GetComponent<SpriteRenderer> ().enabled = true;
		splatter.GetComponent<SplatterAnimation>().StartFade (this.GetComponent<SpriteRenderer>().color, this);
	}

	private GameObject GetSplatter()
	{
		GameObject obj = splatterList [0];
		splatterList.RemoveAt (0);
		return obj;
	}

	public void ReturnToPool(GameObject obj)
	{
        splatterList.Add(obj);

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
		bullet.GetComponent<BulletAnimation> ().startShot (posBeforeDelay);
		coroutine = DelayShot(_bulletTravelDelay,posBeforeDelay);
		StartCoroutine(coroutine);

	}

	public void AttemptShot() 
	{
		if(Time.time - _previousShotTime >= _shotCooldownTime) 
		{
			Shoot();
			AudioController.instance.PLAY (AUDIO.SHOT);
			_previousShotTime = Time.time;
		}
	}

	public void CreatePlatform(GameObject player) 
	{
		Vector3 spawnPosition = new Vector3 (player.transform.position.x, player.transform.position.y, 0);
		GameObject obj = Instantiate(_chosenPlatform, spawnPosition, Quaternion.identity);
        Platform platformComponent = obj.GetComponent<Platform>();
        platformComponent.Init();

		onPlayerKill ();
	}

	public void DesignatePlatformIndex(bool isBomb)
	{
        if(isBomb)
		{
            _chosenPlatform = _bombPrefab;
        }
		else
		{
			int index;
			int r = (int)Random.Range(0, 5) % 4;
			if (r == 0) {
				index = (int)Random.Range(0, _trapPrefabs.Length);
				_chosenPlatform = _trapPrefabs[index];
			}
			else 
			{
				index = (int)Random.Range(0, _platformPrefabs.Length);
				_chosenPlatform = _platformPrefabs[index];
			}
		}

        SetupPreviewUI(_chosenPlatform.GetComponent<SpriteRenderer>().sprite);
    }

	private void SetupPreviewUI(Sprite previewSprite)
	{
        _previewPlatformImage.sprite = previewSprite;
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
