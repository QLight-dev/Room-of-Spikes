using Unity.Mathematics;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField]
    private float speed;

    void Start() { }

    void Update()
    {
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }
}
