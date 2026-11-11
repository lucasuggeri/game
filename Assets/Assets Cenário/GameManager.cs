using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private bool[] _puzzlesCompleted;

    public bool[] PuzzlesCompleted => _puzzlesCompleted;

    [SerializeField] private Transform[] spawnPoints;


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
            return;
        }

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
