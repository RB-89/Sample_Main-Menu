using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Manager_MainMenu : MonoBehaviour
{
    public int sceneToStart;										        //Index number in build settings of scene to load if changeScenes is true
    public bool changeScenes;											    //If true, load a new scene when Start is pressed, if false, fade out UI and continue in single scene
    public bool changeMusicOnStart;										    //Choose whether to continue playing menu music or start a new music clip

    //assign to gameObject before hiding from inspector
    [HideInInspector]    public bool inMainMenu = true;					    //If true, pause button disabled in main menu (Cancel in input manager, default escape key)
    [HideInInspector]    public Animator animColorFade; 					//Reference to animator which will fade to and from black when starting game.
    [HideInInspector]    public AnimationClip colorFadeAnimationClip;		//Animation clip fading to color (black default) when changing scenes

    private float fastFadeIn = .01f;									    //Very short fade time (10 milliseconds) to start playing music immediately without a click/glitch
    private Manager_AudioMixer audioMixer;									//Reference to AudioMixer Manger script
    private ShowPanels showPanels;										    //Reference to ShowPanels script on UI GameObject, to show and hide panels

    void Awake()
    {
        //Get a reference to ShowPanels attached to UI object
        showPanels = GetComponent<ShowPanels>();

        //Get a reference to PlayMusic attached to UI object
        audioMixer = GetComponent<Manager_AudioMixer>();
    }

    public void StartButtonClicked()
    {
        //If changeMusicOnStart is true, fade out volume of music group of AudioMixer by calling FadeDown function of PlayMusic, using length of colorFadeAnimationClip as time. 
        //To change fade time, change length of animation "FadeToColor"
        if (changeMusicOnStart)
        {
            audioMixer.FadeDown(colorFadeAnimationClip.length);
        }

        //If changeScenes is true, start fading and change scenes halfway through animation when screen is blocked by FadeImage
        if (changeScenes)
        {
            //Use invoke to delay calling of LoadDelayed by half the length of colorFadeAnimationClip
            Invoke("LoadDelay", colorFadeAnimationClip.length * .5f);

            //Start the Animator for animColorFade to transition to the FadeToOpaque state.
            animColorFade.SetBool("fade", true);
        }
    }

    public void LoadDelay()
    {
        //Pause button now works if escape is pressed since we are no longer in Main menu.
        inMainMenu = false;

        //Hide the main menu UI element
        showPanels.HideMenu();

        //Load the selected scene, by scene index number in build settings
        Application.LoadLevel(sceneToStart);
    }

    public void PlayNewMusic()
    {
        //Fade up music nearly instantly without a click 
        audioMixer.FadeUp(fastFadeIn);
        //Play music clip assigned to mainMusic in PlayMusic script
        audioMixer.PlayLevelMusic();
    }
}
