using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MarioChet : MonoBehaviour
{
    public float TocDoNay = 20.5f;
    public float DoNayCao = 120f;
    Vector2 ViTriChet;

    private void Update()
    {
        StartCoroutine(HMarioChet());
    }
    IEnumerator HMarioChet()
    { 
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + TocDoNay*Time.deltaTime);
            if (transform.localPosition.y >= ViTriChet.y + DoNayCao +1) break;
            yield return null;

            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - TocDoNay*Time.deltaTime);
            if (transform.localPosition.y <= -10f)
            {
                Destroy(gameObject);
                break;
            }
            yield return null;
        }
    }
}
