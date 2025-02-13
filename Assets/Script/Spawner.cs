using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monster;

    private GameObject spaw;
    [SerializeField]
    private Transform leftPos, rightPos;

    private int randomIndex;
    private int randomSide;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnFrog());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnFrog()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            randomIndex = Random.Range(0, monster.Length);
            spaw = Instantiate(monster[randomIndex]);
            spaw.transform.position = leftPos.position;
        }
    }
}
