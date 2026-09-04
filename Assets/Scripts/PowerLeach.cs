using System.Collections;
using UnityEngine;

public class PowerLeach : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }

    void Update() { }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        yield return null;
    }

    public void DrainPower()
    {
        Debug.Log("executed!");
        GameObject.Find("GameManager").GetComponent<GameManager>().ChangePowerReserve(-10);
    }
}
