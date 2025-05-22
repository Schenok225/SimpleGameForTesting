using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private CanvasController canvasController;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvasController.ShowWinMenu();
        }
    }
}
