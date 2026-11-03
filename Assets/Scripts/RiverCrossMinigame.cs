using System.Collections;
using UnityEngine;

public class RiverCrossMinigame : MonoBehaviour
{
    [SerializeField] private GameObject floatingRock;
    [SerializeField] private Transform[] spawnPoints;
    private bool wasTriggered;
    private int counter;

    private void SpawnObstacle()
    {
        StopAllCoroutines();
        StartCoroutine(SpawnRocks());
    }

    public void EndMinigame()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnRocks()
    {
        int x = Random.Range(0, spawnPoints.Length);
        float y = Random.Range(0.5f, 1.8f);

        
            GameObject rock = Instantiate(floatingRock, spawnPoints[x].position, Quaternion.identity);
            rock.GetComponent<FloatingRock>().speedMultiplier = y;
            rock.SetActive(true);
            counter++;
        

        yield return new WaitForSeconds(1f);

        SpawnObstacle();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !wasTriggered)
        {
            SpawnObstacle();
            wasTriggered = true;
        }


    }
}
