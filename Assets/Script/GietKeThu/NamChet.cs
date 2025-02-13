using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamChet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NamDocBienMat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NamDocBienMat()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
