using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour, IPlatform {

    protected SpriteRenderer _spriteRenderer;
    protected PolygonCollider2D _collider;

    public virtual void Init()
	{
        _spriteRenderer = GetComponent<SpriteRenderer>();

        

        _collider = this.gameObject.AddComponent<PolygonCollider2D>();

    }

    public virtual void Death()
	{
		// might not need
	}

    
}
