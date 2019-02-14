using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionButton : MonoBehaviour {
    public GameObject instructionPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	public void DisplayInstructionAndPauseGame()
    {
        Time.timeScale = 0;
        instructionPanel.SetActive(true);
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        instructionPanel.SetActive(false);
    }
}
