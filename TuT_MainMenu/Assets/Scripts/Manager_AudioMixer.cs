using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class Manager_AudioMixer : MonoBehaviour 
{
    public AudioClip menuMusic;					    //Assign Audioclip for menu music loop
    public AudioClip levelMusic;				    //Assign Audioclip for level music loop

    private AudioSource musicSource;				//Reference to the AudioSource which plays music
    private float resetTime = .01f;					//Very short time used to fade in near instantly without a click

    public AudioMixer mainMixer;                    //Reference to main mixer 
    public AudioMixerSnapshot volumeDown;			//Reference to Audio mixer snapshot in which the master volume of main mixer is turned down
    public AudioMixerSnapshot volumeUp;				//Reference to Audio mixer snapshot in which the master volume of main mixer is turned up

    bool isMute = false;

    #region ExposedMixerVolumes
    //Don't forget to expose the 'volume' paramater from the audio mixer and rename it
    //Don't forget to set the 'boundary' on the slider to match that of the VolMixer range

    public void SetSFXLevel(float sfxLvl)
    {
        mainMixer.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLevel(float musicLvl)
    {
        mainMixer.SetFloat("musicVol", musicLvl);
    }

    public void SetVoiceLevel(float voiceLvl)
    {
        mainMixer.SetFloat("voiceVol", voiceLvl);
    }

    public void SetMasterLevel(float masterLvl)
    {
        mainMixer.SetFloat("masterVol", masterLvl);
    }

    #endregion

    void Awake()
    {
        //Get a component reference to the AudioSource attached to the UI Canvas
        musicSource = GetComponent<AudioSource>();

        //Call the PlayLevelMusic function to start playing music
        PlayLevelMusic();
    }

    //Once the level has loaded, call PlayLevelMusic
    void OnLevelWasLoaded()
    {
        PlayLevelMusic();
    }

    /// <PlayLvelMusic Notes>
    /// Find a way to have the loaded level set the audio clip to play.
    /// Instead of using an infinate amount of cases.
    /// </PlayLvelMusic Notes>
    public void PlayLevelMusic()
    {
        //This switch looks at the last loadedLevel number using the scene index in build settings to decide which music clip to play.
        switch (Application.loadedLevel)
        {
            //If scene index is 0 (usually title scene) assign the clip menuMusic to musicSource
            case 0:
                musicSource.clip = menuMusic;
                break;

            //If scene index is 1 (usually main scene) assign the clip levelMusic to musicSource
            case 1:
                musicSource.clip = levelMusic;
                break;
        }
        //Fade up the volume very quickly, over resetTime seconds (.01 by default)
        FadeUp(resetTime);

        //Play the assigned music clip in musicSource
        musicSource.Play();
    }

    //Call this function to disable AudioListener
    public void Mute()
    {
        isMute = !isMute;

        //AudioListener is attached to mainCamera (player).
        AudioListener.volume = isMute ? 0 : 1;          
    }

    //Call this function to very quickly fade up the volume of master mixer
    public void FadeUp(float fadeTime)
    {
        //call the TransitionTo function of the audioMixerSnapshot volumeUp;
        volumeUp.TransitionTo(fadeTime);
    }

    //Call this function to fade the volume to silence over the length of fadeTime
    public void FadeDown(float fadeTime)
    {
        //call the TransitionTo function of the audioMixerSnapshot volumeDown;
        volumeDown.TransitionTo(fadeTime);
    }
}
