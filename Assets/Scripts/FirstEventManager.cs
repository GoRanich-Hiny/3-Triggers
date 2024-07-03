using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FirstEventManager : MonoBehaviour
{
    [SerializeField] GameObject instantiableObject;
    [SerializeField] GameObject interactiveObject;

    [SerializeField] Vector3 targetPosition;

    [SerializeField] float moveSpeed;
    [SerializeField] float delayToDestroy;

    [SerializeField] bool IsSpawned = false;
    [SerializeField] bool IsOnceSpawned = false;
    [SerializeField] public int IsOnceSpawnedInt = 0;

    void Update()
    {
        if(IsSpawned)
        {
            MoveObjectLerp(interactiveObject, targetPosition);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (IsOnceSpawnedInt == 0)
        {
            IsOnceSpawned = false;
        }
        else if (IsOnceSpawnedInt == 1)
        {
            IsOnceSpawned = true;
        }
        if (other.tag == "Player" && !IsSpawned && !IsOnceSpawned)
        {
            Debug.Log("Detect Player");
            interactiveObject = Instantiate(instantiableObject, transform.position, new Quaternion().normalized, transform);
            IsSpawned = true;
            IsOnceSpawned = true;
            IsOnceSpawnedInt = 1;

            Invoke("DestroyerAfterSpawn", delayToDestroy);
        }
    }

    void MoveObjectLerp(GameObject obj, Vector3 targetPosition)
    {
        obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void DestroyerAfterSpawn()
    {
        Destroy(interactiveObject);
        IsSpawned = false;
    }
}
