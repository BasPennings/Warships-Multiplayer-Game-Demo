using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCollisionManager : MonoBehaviour
{
    public static WaveCollisionManager instance;

    [SerializeField] private float amplitude = 1F;
    [SerializeField] private float length = 2F;
    [SerializeField] private float speed = 1F;
    [SerializeField] private float offset = 0;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this)
        {
            Debug.LogError("There can only be one wave manager instance! Access it using WaveManager.instance");
        }
    }

    private void Update()
    {
        offset += Time.deltaTime * speed;
    }

    public float GetWaveHeight(float x)
    {
        return amplitude * Mathf.Sin(x / length + offset);
    }
}
