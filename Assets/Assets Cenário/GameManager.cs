using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool[] _puzzlesCompleted;

    public bool[] PuzzlesCompleted => _puzzlesCompleted;

  
    

    private GameObject player;

    private Dictionary<string, int> sceneSpawnPoints = new Dictionary<string, int>();
    private Dictionary<string, int> sceneExitPoints = new Dictionary<string, int>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;
        int exitIndex = GetExitPoint(sceneName);

        LevelManager levelManager = FindAnyObjectByType<LevelManager>();
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (levelManager == null || player == null) return;

        player.transform.position = levelManager.GetSpawnPosition(exitIndex);
    }
    public void SaveExitPoint(string sceneName, int exitIndex)
    {
        sceneExitPoints[sceneName] = exitIndex;
    }

    public int GetExitPoint(string sceneName)
    {
        return sceneExitPoints.TryGetValue(sceneName, out int index) ? index : 0;
    }
    public void SetSpawnPoint(string sceneName, int spawnIndex)
    {
        sceneSpawnPoints[sceneName] = spawnIndex;
    }

    public int GetSpawnPoint(string sceneName)
    {
        if (sceneSpawnPoints.ContainsKey(sceneName))
        {
            return sceneSpawnPoints[sceneName];
        }

        return 0;
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    public void MarkPuzzleAsCompleted( int puzzleNumber)
    {
        if(puzzleNumber < 0 || puzzleNumber > _puzzlesCompleted.Length)
        {
            Debug.Log("Numero de puzzle inválido");
            return;
        }

        _puzzlesCompleted[puzzleNumber] = true;
    }

}
