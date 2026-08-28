using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool verticalLaser = false;

    [SerializeField]
    private float laserSpeed = 2f;

    private bool onLeftSide = false;
    private bool onBottomside = false;

    void Start()
    {
        StartCoroutine(gameLoop());
    }

    // Update is called once per frame
    void Update() { }

    public void Activate()
    {
        StartCoroutine(ActivateCoroutine());
    }

    public void UnActivate()
    {
        StartCoroutine(UnActivateCoroutine());
    }

    IEnumerator gameLoop()
    {
        while (true)
        {
            Activate();
            yield return new WaitForSeconds(2f);
            UnActivate();
            yield return new WaitForSeconds(4f);
        }
    }

    IEnumerator ActivateCoroutine()
    {
        float elapsed = 0f;
        while (elapsed <= laserSpeed)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(
                transform.position,
                new Vector3(0, transform.position.y, transform.position.z),
                elapsed / laserSpeed
            );
            yield return null;
        }
    }

    IEnumerator UnActivateCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < laserSpeed)
        {
            elapsed += Time.deltaTime;
            if (onLeftSide == false)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(-40, transform.position.y, transform.position.z),
                    elapsed / laserSpeed
                );
            }
            else
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(40, transform.position.y, transform.position.z),
                    elapsed / laserSpeed
                );
            }
            yield return null;
        }

        if (!verticalLaser && onLeftSide)
        {
            onLeftSide = false;
        }
        else if (!verticalLaser && !onLeftSide)
        {
            onLeftSide = true;
        }
    }
}
