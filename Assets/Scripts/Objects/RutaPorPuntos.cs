using UnityEngine;

public class RutaPorPuntos : MonoBehaviour
{
    [Header("Movimiento por puntos")]
    public Transform[] puntosRuta;
    public float velocidad = 1f;
    public bool rotarHaciaDestino = true;
    public bool loop = true;

    [Header("Rotación sobre ejes")]
    public bool rotarEnX = false;
    public bool rotarEnY = false;
    public bool rotarEnZ = false;
    public float velocidadRotacionX = 30f;
    public float velocidadRotacionY = 30f;
    public float velocidadRotacionZ = 30f;

    private int puntoActual = 0;

    void Update()
    {
        // Movimiento por ruta
        if (puntosRuta.Length > 0)
        {
            Transform destino = puntosRuta[puntoActual];
            Vector3 direccion = (destino.position - transform.position).normalized;

            transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);

            if (rotarHaciaDestino)
            {
                Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 2f);
            }

            if (Vector3.Distance(transform.position, destino.position) < 0.1f)
            {
                puntoActual++;
                if (puntoActual >= puntosRuta.Length)
                {
                    puntoActual = loop ? 0 : puntosRuta.Length - 1;
                }
            }
        }

        // Rotación adicional sobre ejes
        Vector3 rotacionExtra = Vector3.zero;
        if (rotarEnX) rotacionExtra.x = velocidadRotacionX * Time.deltaTime;
        if (rotarEnY) rotacionExtra.y = velocidadRotacionY * Time.deltaTime;
        if (rotarEnZ) rotacionExtra.z = velocidadRotacionZ * Time.deltaTime;

        transform.Rotate(rotacionExtra, Space.Self);
    }

    void OnDrawGizmos()
    {
        if (puntosRuta == null || puntosRuta.Length < 2) return;

        Gizmos.color = Color.cyan;
        for (int i = 0; i < puntosRuta.Length - 1; i++)
        {
            Gizmos.DrawLine(puntosRuta[i].position, puntosRuta[i + 1].position);
        }

        if (loop)
        {
            Gizmos.DrawLine(puntosRuta[puntosRuta.Length - 1].position, puntosRuta[0].position);
        }
    }
}
