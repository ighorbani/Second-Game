using UnityEngine;

public class SceneController : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject modalPanel;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true);
        modalPanel.SetActive(true);

        // Trigger animation for pausePanel
        Animator pausePanelAnimator = pausePanel.GetComponent<Animator>();
        pausePanelAnimator.SetBool("come", true);

        // Trigger animation for modalPanel
        Animator modalPanelAnimator = modalPanel.GetComponent<Animator>();
        modalPanelAnimator.SetBool("come", true);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Debug.Log("resume");

        // Trigger animation for pausePanel
        Animator pausePanelAnimator = pausePanel.GetComponent<Animator>();
        pausePanelAnimator.SetBool("come", false);

        // Trigger animation for modalPanel
        Animator modalPanelAnimator = modalPanel.GetComponent<Animator>();
        modalPanelAnimator.SetBool("come", false);
    }
}