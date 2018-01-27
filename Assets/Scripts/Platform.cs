using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformType
{
	// TODO:
}

// 
public abstract class Platform : MonoBehaviour, IPlatform {

	[SerializeField]
    private PlatformType _platformType;

    private Sprite _sprite;

    private SpriteRenderer _spriteRenderer;

    public virtual void Init(Sprite sprite)
	{
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _sprite = sprite;

        if(_spriteRenderer == null)
		{
            Debug.LogError("There is no sprite renderer on the Platform GameObject.");
        }
		if(_sprite == null)
		{
            Debug.LogError("The sprite is null.");
        }

        _spriteRenderer.sprite = _sprite;
        this.gameObject.AddComponent<PolygonCollider2D>();
    }

    public virtual void Death()
	{
		// might not need
	}

    
}
