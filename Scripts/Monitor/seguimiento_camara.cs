using UnityEngine;

public class seguimiento_camara : MonoBehaviour
{
    public Transform jugador; // Referencia al transform del jugador

    public float suavidadMovimiento = 0.1f; // Controla la suavidad del seguimiento

    private Vector3 offset; // Distancia entre la cámara y el jugador al inicio

    void Start()
    {
        // Calcula el offset entre la posición de la cámara y la del jugador
        offset = transform.position - jugador.position;
    }

    void LateUpdate()
    {
        if (jugador != null)
        {
            // Obtiene la posición del jugador en 2D
            Vector3 posicionJugador = jugador.position;

            // Calcula la posición a la que la cámara debe moverse para seguir al jugador
            Vector3 nuevaPosicion = posicionJugador + offset;

            // Aplica una transición suave al movimiento de la cámara
            transform.position = Vector3.Lerp(transform.position, nuevaPosicion, suavidadMovimiento);
        }
    }
}
