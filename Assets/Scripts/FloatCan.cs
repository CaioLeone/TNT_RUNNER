using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatCan : MonoBehaviour
{
    [Header("Configura��es do Movimento")]
    public float amplitude = 0.5f; // Altura do movimento para cima e para baixo
    public float speed = 1.0f; // Velocidade do movimento

    private Vector3 startPosition;

    void Start()
    {
        // Salva a posi��o inicial da moeda
        startPosition = transform.position;
    }

    void Update()
    {
        // Calcula o deslocamento vertical com base no tempo
        float offsetY = Mathf.Sin(Time.time * speed) * amplitude;

        // Atualiza a posi��o da moeda
        transform.position = new Vector3(startPosition.x, startPosition.y + offsetY, startPosition.z);
    }
}
