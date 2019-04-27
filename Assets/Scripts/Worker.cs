using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worker : MonoBehaviour
{
    [SerializeField]
    private Rigidbody[] ragdollBodies;

    [SerializeField]
    private SkinnedMeshRenderer workerRenderer;

    [SerializeField]
    private Material workerLiveMaterial;

    [SerializeField]
    private Material workerDiedMaterial;

    [SerializeField]
    private float minEnergy;

    [SerializeField]
    private float maxEnergy;

    [SerializeField]
    private float depleteEnergySpeed = 1;

    private float workerEnergy = 100;
    private bool isDead;

    private void Start()
    {
        workerRenderer.material = workerLiveMaterial;
        workerEnergy = Random.Range(minEnergy, maxEnergy);
        FreezeParts();
    }

    private void Update()
    {
        if (!isDead)
        {
            workerEnergy -= Time.deltaTime * depleteEnergySpeed;

            if (workerEnergy <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        workerRenderer.material = workerDiedMaterial;
        workerEnergy = 0;
        isDead = true;
        ReleaseParts();
    }

    private void FreezeParts()
    {
        foreach(Rigidbody rbd in ragdollBodies)
        {
            rbd.isKinematic = true;
        }
    }

    private void ReleaseParts()
    {
        foreach (Rigidbody rbd in ragdollBodies)
        {
            rbd.isKinematic = false;
            rbd.AddExplosionForce(Random.Range(100,150), transform.position, 5f);
        }
    }
}
