using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float turretTurnSpeed = 10F;
    [SerializeField] private float turretTurnLimit = 45F;

    [Header("Shooting")]
    [SerializeField] private Transform shellPrefab;
    [SerializeField] private Transform[] shellSpawnTransforms;
    [SerializeField] private float turretFiringRange = 50;
    [SerializeField] private float turretShellVelocity = 700;
    [SerializeField] private float turretShellDamage = 100;
    [SerializeField] private float turretReloadtime = 7; // in seconds

    private bool canShoot = false;

    void Start()
    {
        StartCoroutine(Cooldown(turretReloadtime));
    }

    void Update()
    {
        if (GameInput.Instance.isLeftClicking() && canShoot)
        {
            StartCoroutine(Cooldown(turretReloadtime));

            foreach (Transform shellSpawnTransform in shellSpawnTransforms)
            {
                Instantiate(shellPrefab);
            }
        }
    }

    public IEnumerator Cooldown(float seconds)
    {
        canShoot = false;
        yield return new WaitForSeconds(seconds);
        canShoot = true;
    }
}
