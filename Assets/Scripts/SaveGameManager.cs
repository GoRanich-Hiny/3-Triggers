using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using static UnityEngine.ParticleSystem;

public class SaveGameManager : MonoBehaviour
{
    public Transform player;
    public Transform startingPosition;

    public GameObject menuPrefab;
    public GameObject playerPrefab;

    public FirstEventManager firstEvent;
    public SecondEventManager secondEvent;

    void Start()
    {
        firstEvent = FindAnyObjectByType<FirstEventManager>();
        secondEvent = FindAnyObjectByType<SecondEventManager>();
        LoadGame();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            menuPrefab.SetActive(!menuPrefab.activeInHierarchy);
            playerPrefab.SetActive(!menuPrefab.activeInHierarchy);
        }
        if (menuPrefab.activeInHierarchy == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(menuPrefab.activeInHierarchy == false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerPositionX", player.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", player.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", player.position.z);

        PlayerPrefs.SetInt("FirstEventOccurredStatus", firstEvent.IsOnceSpawnedInt);
        PlayerPrefs.SetInt("SecondEventOccurredStatus", secondEvent.IsOnceSpawnedInt);

        
        PlayerPrefs.Save();
        Debug.Log("Game Saved!");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("PlayerPositionX"))
        {
            float x = PlayerPrefs.GetFloat("PlayerPositionX");
            float y = PlayerPrefs.GetFloat("PlayerPositionY");
            float z = PlayerPrefs.GetFloat("PlayerPositionZ");

            int first = PlayerPrefs.GetInt("FirstEventOccurredStatus");
            int second = PlayerPrefs.GetInt("SecondEventOccurredStatus");

            firstEvent.IsOnceSpawnedInt = first;
            secondEvent.IsOnceSpawnedInt = second;
            player.position = new Vector3(x, y, z);
            Debug.Log("Game Loaded!");

            menuPrefab.SetActive(false);
            playerPrefab.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Debug.Log("No save data found!");
        }
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        ResetGameProgress();

        SceneManager.LoadScene("SampleScene");
        menuPrefab.SetActive(false);
        playerPrefab.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void ResetGameProgress()
    {
        player.position = startingPosition.position;
        firstEvent.IsOnceSpawnedInt = 0;
        secondEvent.IsOnceSpawnedInt = 0;
    }
}