using UnityEngine;

public class CarManager : MonoBehaviour
{
    public int carNumber;          // Identificador único para cada coche
    public int currentCheckpoint; // Último checkpoint cruzado
    public int lapsCompleted;     // Vueltas completadas

    public RaceManager raceManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            Checkpoint checkpoint = other.GetComponent<Checkpoint>();

            if (checkpoint.index == (currentCheckpoint + 1) % raceManager.totalCheckpoints)
            {
                // Actualizar progreso del checkpoint
                currentCheckpoint = checkpoint.index;

                // Si es el último checkpoint, incrementar vueltas
                if (currentCheckpoint == 0)
                {
                    lapsCompleted++;
                }

                // Notificar al RaceManager
                raceManager.UpdateCarProgress(carNumber, lapsCompleted, currentCheckpoint);
            }
        }
    }
}
