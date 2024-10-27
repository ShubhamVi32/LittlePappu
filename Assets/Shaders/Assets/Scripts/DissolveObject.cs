using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class DissolveObject : MonoBehaviour
{
    [SerializeField] private float noiseStrength = 0.25f;
    [SerializeField] private float objectHeight = 1.0f;

    private Material material;
    public bool isDissolve;

    private void Awake()
    {
        material = GetComponent<Renderer>().material;
        isDissolve = true;
    }

    private void Update()
    {
        if (isDissolve)
        {
            var time = Time.time * Mathf.PI * 0.25f;

            float height = transform.position.y;
            height += Mathf.Sin(time) * (objectHeight / 2.0f);
            SetHeight(height);
            if (height > 1.3f)
            {
                isDissolve = false;
            }
        }
    }

    private void SetHeight(float height)
    {
        material.SetFloat("_CutoffHeight", height);
        material.SetFloat("_NoiseStrength", noiseStrength);
    }
}
