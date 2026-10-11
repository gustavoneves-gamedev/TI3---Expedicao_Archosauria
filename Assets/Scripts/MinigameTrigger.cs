using System.Collections;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject rock;

    private bool wasTriggered;

    public int counter;

    private void SpawnObstacle()
    {
        StartCoroutine(SpawnRocks());
    }

    public void EndMinigame()
    {
        StopAllCoroutines();        
    }

    private IEnumerator SpawnRocks()
    {
        int x = Random.Range(0, spawnPoints.Length);
        
        if (counter < 10)
        {
            Instantiate(rock, spawnPoints[x].position, Quaternion.identity);
            counter++;
        }

        yield return new WaitForSeconds(1f);

        SpawnObstacle();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !wasTriggered)
        {
            //GameController.gameController.SaveGame(transform.position);
            SpawnObstacle();
            wasTriggered = true;
        }
        if (other.CompareTag("SphereTrap"))
        {
            Destroy(other.gameObject);
            counter--;
        }

    }
}
