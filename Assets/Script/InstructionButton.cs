using UnityEngine;

public class InstructionButton : MonoBehaviour {
    public GameObject InstructionPanel;
	
	public void DisplayInstructionAndPauseGame()
    {
        Time.timeScale = 0;
        InstructionPanel.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        InstructionPanel.SetActive(false);
    }
}
