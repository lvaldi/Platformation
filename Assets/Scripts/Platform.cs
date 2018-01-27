using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour, IPlatform {

    private SpriteRenderer _spriteRenderer;

    public virtual void Init()
	{
        _spriteRenderer = GetComponent<SpriteRenderer>();

        

        this.gameObject.AddComponent<PolygonCollider2D>();
    }

    public virtual void Death()
	{
		// might not need
	}

    
}
