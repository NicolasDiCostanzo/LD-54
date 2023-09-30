using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    public AnimationCurve curve;

    public float duration;
    public float amplitude;

    private Vector3 _startPos;

    private float progress = 0f;

    // Start is called before the first frame update
    void Start()
    {
        progress = 0f;
        _startPos = target.transform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        progress += Time.deltaTime;
        if (progress > 2f * duration) progress = 0f;
        target.transform.localPosition = _startPos + Vector3.up * amplitude * (1f - curve.Evaluate(progress / duration) * 2f);
    }
}
