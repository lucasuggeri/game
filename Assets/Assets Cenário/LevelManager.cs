using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        PlayerController player = FindAnyObjectByType<PlayerController>();

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager.instance está NULL!");
            return;
        }

        if (player == null)
        {
            Debug.LogError("PlayerController não encontrado na cena!");
            return;
        }

        int spawnIndex = GameManager.instance.GetSpawnPoint( SceneManager.GetActiveScene().name );

        if (spawnIndex >= 0 && spawnIndex < _spawnPoints.Length)
        {
            player.transform.position = _spawnPoints[spawnIndex].position;
        }
        else
        {
            Debug.LogWarning("Índice de spawn inválido!");
        }
    }

    public void TransitionToScene(string sceneName, int destinationSpawn, int returnSpawn)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // Quando voltar para a cena atual, nasce no ponto por onde saiu
        GameManager.instance.SetSpawnPoint(currentScene, returnSpawn);

        // Na cena de destino, nasce no ponto indicado
        GameManager.instance.SetSpawnPoint(sceneName, destinationSpawn);

        GameManager.instance.ChangeScene(sceneName);
    }

    public Vector3 GetSpawnPosition(int index)
    {
        if (index < 0 || index >= _spawnPoints.Length)
        {
            Debug.LogWarning($"Spawn index {index} inválido nesta cena");
            return _spawnPoints.Length > 0 ? _spawnPoints[0].position : Vector3.zero;
        }

        return _spawnPoints[index].position;
    }
}