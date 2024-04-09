using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target; // Le transform du joueur que la caméra doit suivre
    public float followSpeed = 5f; // Vitesse de suivi de la caméra

    private Vector3 offset; // Distance entre la caméra et le joueur

    private void Start()
    {
        // Calcul de l'offset initial
        offset = transform.position - target.position;
    }
    
    private void Update()
    {
        Vector3 targetPosition = target.position + offset;
        Vector3 posLerp = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.position = posLerp;

        // Regarder toujours vers le joueur
        transform.LookAt(target);
    }
}
