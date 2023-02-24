

using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] Transform player; // Transform del jugador
    [SerializeField] float sensitivityX = 1f; // Sensibilidad al rotar horizontalmente
    [SerializeField] float smoothSpeed = 0.125f; // Velocidad de suavizado del movimiento
    [SerializeField] float distance = 10f; // Distancia entre la camara y el jugador
    [SerializeField] float height = 3f; // Altura de la camara sobre el jugador

    float yaw; // Rotaci�n en eje X

    void LateUpdate()
    {
        // Obtener entrada del mouse
        float mouseX = Input.GetAxisRaw("Mouse X");
        // Calcular rotaci�n en eje X
        yaw += mouseX * sensitivityX;

        // Calcular posici�n deseada de la c�mara
        Vector3 desiredPosition = player.position - player.forward * distance + Vector3.up * height;

        // Suavizar movimiento de la c�mara
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Hacer que la c�mara mire hacia la espalda del jugador
        transform.LookAt(player.position + player.forward * -1);

        // Rotar el jugador en el mismo �ngulo en eje X que se est� rotando la c�mara
        player.rotation = Quaternion.Euler(0, yaw, 0);
    }
}



