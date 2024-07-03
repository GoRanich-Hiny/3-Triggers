using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdEventManager : MonoBehaviour
{
    [SerializeField] GameObject instantiableObject;
    [SerializeField] GameObject interactiveObject;

    [SerializeField] Vector3 spawnPosition;

    [SerializeField] float delayToDestroy;

    [SerializeField] bool IsOnceSpawned = false;

    void Start()
    {
        IsOnceSpawned = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !IsOnceSpawned)
        {
            Debug.Log("Detect Player");
            interactiveObject = Instantiate(instantiableObject, spawnPosition, new Quaternion().normalized, transform);
            interactiveObject.GetComponent<ParticleSystem>().Play();
            IsOnceSpawned = true;
            Invoke("DestroyerAfterSpawn", delayToDestroy);
        }
    }

    void DestroyerAfterSpawn()
    {
        Destroy(interactiveObject);
    }
}
