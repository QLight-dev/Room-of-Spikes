using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions controller;

    [SerializeField]
    private int speed;

    void Start()
    {
        controller = new InputSystem_Actions();
        controller.Enable();
    }

    void Update()
    {
        float horizontalInput = controller.Player.Horizontal.ReadValue<float>();
        float verticalInput = controller.Player.Vertical.ReadValue<float>();

        transform.Translate(
            horizontalInput * speed * Time.deltaTime,
            verticalInput * speed * Time.deltaTime,
            0
        );

        // side bounds
        if (transform.position.x < -15.9f)
        {
            transform.position = new Vector3(-15.9f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 15.9f)
        {
            transform.position = new Vector3(15.9f, transform.position.y, transform.position.z);
        }

        // top / down bounds
        if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(transform.position.x, -4.5f, transform.position.z);
        }
        if (transform.position.y > 4.5f)
        {
            transform.position = new Vector3(transform.position.x, 4.5f, transform.position.z);
        }
    }
}
