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
    }
}
