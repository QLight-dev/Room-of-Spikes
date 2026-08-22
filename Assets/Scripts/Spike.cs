using System.Collections;
using System.Timers;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField]
    private int speed = 50;

    void Start() { }

    // Update is called once per frame
    void Update() { }

    void Fold()
    {
        StartCoroutine(FoldCoroutine());
    }

    void Unfold()
    {
        StartCoroutine(UnfoldCoroutine());
    }

    IEnumerator FoldCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;

            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(transform.position.x, -4),
                speed * Time.deltaTime
            );
            yield return null;
        }
    }

    IEnumerator UnfoldCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            elapsed += Time.deltaTime;

            transform.position = Vector2.MoveTowards(
                transform.position,
                new Vector2(transform.position.x, -9),
                speed * Time.deltaTime
            );
            yield return null;
        }
    }
}
