using UnityEngine;

public class CarManager : MonoBehaviour
{
    public int carNumber;          // Identificador único para cada coche
    public int currentCheckpoint; // Último checkpoint cruzado
    public int lapsCompleted;     // Vueltas completadas

    public int position;

    public RaceManager raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CP"))
        {
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();

            // Verificar que se cruza el siguiente checkpoint esperado
            if (checkpoint.index == (currentCheckpoint + 1) % raceManager.totalCheckpoints)
            {
                currentCheckpoint = checkpoint.index;

                // Incrementar vueltas al cruzar el último checkpoint
                if (currentCheckpoint == raceManager.totalCheckpoints - 1)
                {
                    lapsCompleted++;
                    currentCheckpoint = 0; // Reiniciar checkpoints para la nueva vuelta
                }

                // Notificar al RaceManager sobre el progreso actualizado
                raceManager.UpdateCarProgress(carNumber, lapsCompleted, currentCheckpoint);
            }
        }
    }
}