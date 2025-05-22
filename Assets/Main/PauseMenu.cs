using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private CanvasController canvasController;
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    public void Resume()
    {
        canvasController.HideAll();
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Pause()
    {
        canvasController.ShowPauseMenu();
        Time.timeScale = 0f;
        isPaused = true;
    }
}
