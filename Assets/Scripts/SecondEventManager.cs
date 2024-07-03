using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondEventManager : MonoBehaviour
{
    [SerializeField] SaveGameManager saveManager;
    [SerializeField] GameObject interactiveObject;

    [SerializeField] Vector3 targetPosition;

    [SerializeField] float moveSpeed;
    [SerializeField] float delayToDestroy;

    [SerializeField] bool IsUsed = false;
    [SerializeField] bool IsOnceUsed = false;
    [SerializeField] public int IsOnceSpawnedInt = 0;

    void Start()
    {   
        saveManager = FindAnyObjectByType<SaveGameManager>();
        if (IsOnceSpawnedInt == 0)
        {
            IsOnceUsed = false;
        }
        else if (IsOnceSpawnedInt == 1)
        {
            IsOnceUsed = true;
        }
    }

    void Update()
    {

        if (!IsOnceUsed && IsUsed)
        {
            if (interactiveObject.transform.position != targetPosition)
            {
                MoveObjectLerp(interactiveObject, targetPosition);
            }
            else
            {
                interactiveObject.gameObject.GetComponent<Rigidbody>().useGravity = true;
                IsOnceUsed = true;
                IsOnceSpawnedInt = 1;
                saveManager.SaveGame();
            }
        }
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !IsOnceUsed)
        {
            Debug.Log("Detect Player");
            IsUsed = true;
        }
    }

    void MoveObjectLerp(GameObject obj, Vector3 targetPosition)
    {
        obj.transform.position = Vector3.Lerp(obj.transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
