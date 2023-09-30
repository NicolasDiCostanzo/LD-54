using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShadowAnimation : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;

    public float period = .5f;
    private float progress = 0f;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        progress += Time.deltaTime;
        if (progress > period)
        {
            progress = 0f;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
            _spriteRenderer.flipY = CoinFlip();
        }
    }

    private bool CoinFlip()
    {
        return Random.value < .5f;
    }
}
