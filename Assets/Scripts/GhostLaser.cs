using System.Collections;
using UnityEngine;

public class GhostLaser : MonoBehaviour
{
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(Blink());
    }

    void Update() { }

    IEnumerator Blink()
    {
        while (true)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.33f);
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.33f);
        }
    }
}
