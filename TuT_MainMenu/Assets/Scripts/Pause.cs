using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {


	private ShowPanels showPanels;						//Reference to the ShowPanels script used to hide and show UI panels
    private Manager_MainMenu mainMenu;					//Reference to the Main Menu Manager script
	private bool isPaused;								//Boolean to check if the game is paused or not
	
	//Awake is called before Start()
	void Awake()
	{
		//Get a component reference to ShowPanels attached to this object, store in showPanels variable
		showPanels = GetComponent<ShowPanels> ();
		//Get a component reference attached to this object, store in mainMenu variable
        mainMenu = GetComponent<Manager_MainMenu>();
	}

	// Update is called once per frame
	void Update () 
    {
		//Check if the Cancel button in Input Manager is down this frame (default is Escape key) and that game is not paused, and that we're not in main menu
		if (Input.GetButtonDown ("Cancel") && !isPaused && !mainMenu.inMainMenu) 
		{
			//Call the Paused function to pause the game
			Paused();
		} 
		//If the button is pressed and the game is paused and not in main menu
		else if (Input.GetButtonDown ("Cancel") && isPaused && !mainMenu.inMainMenu) 
		{
			//Call the UnPause function to unpause the game
			UnPause ();
		}
	}

	public void Paused()
	{
		//Set isPaused to true
		isPaused = true;

		//Set time.timescale to 0, this will cause animations and physics to stop updating
		Time.timeScale = 0;

		//call the ShowPausePanel function of the ShowPanels script
		showPanels.ShowPausePanel ();
	}

	public void UnPause()
	{
		//Set isPaused to false
		isPaused = false;

		//Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
		Time.timeScale = 1;

		//call the HidePausePanel function of the ShowPanels script
		showPanels.HidePausePanel ();
	}

    public void ReturnToMenu()
    {
        UnPause();

        //Return to the Main Menu
        Application.LoadLevel(0);
        mainMenu.inMainMenu = true;

        //Reset the Animator for Fade between loading
        mainMenu.animColorFade.SetBool("fade", false);

        //Show the Main Menu Panel
        showPanels.ShowMenu();
    }


}
