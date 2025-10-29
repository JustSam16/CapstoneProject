using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated) return;

        if (collision.CompareTag("Player"))
        {
            activated = true;
            PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();

            if (playerRespawn != null)
            {
                playerRespawn.UpdateCheckpoint(transform.position);
            }

            
            UIMessageManager ui = FindObjectOfType<UIMessageManager>();
            if (ui != null)
                ui.ShowCheckpointMessage();

            Debug.Log("✅ Checkpoint raggiunto!");
        }
    }
}
