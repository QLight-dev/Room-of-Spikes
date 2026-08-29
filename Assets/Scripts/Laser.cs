using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool verticalLaser = false;

    [SerializeField]
    private float laserSpeed = 2f;

    public bool onLeftSide = false;
    public bool onBottomside = false;

    void Start() { }

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

    IEnumerator ActivateCoroutine()
    {
        float elapsed = 0f;
        while (elapsed <= laserSpeed)
        {
            elapsed += Time.deltaTime;
            if (verticalLaser)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(transform.position.x, 0, transform.position.z),
                    elapsed / laserSpeed
                );
            }
            else
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    new Vector3(0, transform.position.y, transform.position.z),
                    elapsed / laserSpeed
                );
            }
            yield return null;
        }
    }

    IEnumerator UnActivateCoroutine()
    {
        float elapsed = 0f;
        while (elapsed < laserSpeed)
        {
            elapsed += Time.deltaTime;
            if (verticalLaser)
            {
                if (onBottomside == false)
                {
                    transform.position = Vector3.Lerp(
                        transform.position,
                        new Vector3(transform.position.x, -30, transform.position.z),
                        elapsed / laserSpeed
                    );
                }
                else
                {
                    transform.position = Vector3.Lerp(
                        transform.position,
                        new Vector3(transform.position.x, 30, transform.position.z),
                        elapsed / laserSpeed
                    );
                }
            }
            else
            {
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
        else if (onBottomside)
        {
            onBottomside = false;
        }
        else
        {
            onBottomside = true;
        }
    }
}
