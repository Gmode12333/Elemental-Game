using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveLevel : MonoBehaviour
{
    public GameObject[] Gate;
    public GameObject ObjectOpen;
    public GameObject[] enemys;
    public GameObject Boss;
    public BoxCollider boxCollider;
    public GameObject interactBox;

    public int needToSpawn;
    public int xPos1;
    public int xPos2;
    public int zPos1;
    public int zPos2;
    public int yPos;
    public int enemyCount;
    public bool isFinaleState;

    private int xPos;
    private int zPos;
    public void SetObject()
    {
        foreach (GameObject go in Gate)
        {
            go.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactBox.SetActive(true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.CompareTag("Player"))
        {
            interactBox.SetActive(false);
            InGameManager.Instance.isOnState = true;
            SetObject();
            StartCoroutine(SpawnRandomEnemy());
            boxCollider.GetComponent<BoxCollider>().enabled = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        interactBox.SetActive(false);
    }
    public void FinishState()
    {
        InGameManager.Instance.isOnState = false;
        if (isFinaleState)
        {
            InGameManager.Instance.SetWin();
        }
        foreach (GameObject go in Gate)
        {
            go.SetActive(false);
            if (ObjectOpen != null)
            {
                ObjectOpen.transform.localRotation = Quaternion.Euler(-20, 102, 2);
            }
        }
    }
    IEnumerator SpawnRandomEnemy()
    {
        while(enemyCount < needToSpawn)
        {
            for (int i = 0; i < enemys.Length; i++)
            {
                xPos = Random.Range(xPos1, xPos2);
                zPos = Random.Range(zPos1, zPos2);
                yield return new WaitForSeconds(2f);
                Instantiate(enemys[i], new Vector3(xPos, yPos, zPos), Quaternion.identity);
                enemyCount++;
            }
            yield return new WaitUntil(() => EnemyAI.EnemyCount == 0);
            yield return new WaitForSeconds(1f);
            if (enemyCount == needToSpawn)
            {
                StartCoroutine(Complete());
            }
        }
    }
    IEnumerator Complete()
    {
        yield return new WaitUntil(() => EnemyAI.EnemyCount == 0);
        Instantiate(Boss, new Vector3(xPos, yPos, zPos), Quaternion.identity);
        yield return new WaitUntil(() => EnemyAI.EnemyCount == 0);
        FinishState();
    }
}
