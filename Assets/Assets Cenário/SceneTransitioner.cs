using System.Collections;
using UnityEngine;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField] private string nextSceneName;


    private Collider2D collider2D;
    private bool canTransition;
    [SerializeField] private float transitionCooldown = 1.5f;
    [Header("Retorno")]
    [Tooltip("Spawn point DESTA cena onde o jogador reaparece quando voltar")]
    [SerializeField] private int sceneSpawnIndex;

    [SerializeField] private int destinationSpawnIndex;

    private LevelManager levelManager;

    private void OnEnable()
    {
        StartCoroutine(DelayTransition());
    }
    private void Start()
    {
        levelManager = FindAnyObjectByType<LevelManager>();
        collider2D = GetComponent<Collider2D>();
       
    }

    private IEnumerator DelayTransition()
    {
        canTransition = false;
        yield return new WaitForSeconds(transitionCooldown);
        canTransition = true;
    }
    public void TransitionScene()
    {
        if (!canTransition) return;
        levelManager.TransitionToScene(nextSceneName, destinationSpawnIndex, sceneSpawnIndex);
    }
   
}