using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class XuNay : MonoBehaviour
{
    public Text txt;
    GameObject Mario;

    // Start is called before the first frame update
    void Start()
    {
        txt = GameObject.Find("txtScore").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Mario.GetComponent<MarioScript>().score += 1;
            Destroy(gameObject);
            txt.text = "Score : " + Mario.GetComponent<MarioScript>().score.ToString();
        }
    }
}
