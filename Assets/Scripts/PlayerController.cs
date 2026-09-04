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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Lethal"))
        {
            Destroy(gameObject);
            Debug.Log("you died");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(
            "Collided with "
                + other.gameObject.name
                + " and does it have power leach tag? well, "
                + other.gameObject.CompareTag("Power Leach")
        );
        if (other.gameObject.CompareTag("Power Leach"))
        {
            other.GetComponent<PowerLeach>().DrainPower();
            Destroy(other.gameObject);
        }
    }
}
