using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MusicController : MonoBehaviour {

    public AudioSource sourceEffects, sourceTracks, mainSong;
    public static MusicController instance = null;
    private Dictionary<SoundsEnum.soundEffect, AudioClip> soundEffects;
    private Dictionary<SoundsEnum.soundTrack, AudioClip> soundTracks;

    

    private void Awake() {

        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        sourceTracks.loop = true;

        soundEffects = new Dictionary<SoundsEnum.soundEffect, AudioClip>();
        soundTracks = new Dictionary<SoundsEnum.soundTrack, AudioClip>(); 

        addSoundEffectsToDictionary();       
        addSoundTracksToDictionary();
    }

    private void addSoundEffectsToDictionary() {
        string key = "Sounds/";

        foreach (SoundsEnum.soundEffect sound in System.Enum.GetValues(typeof(SoundsEnum.soundEffect))) {
            soundEffects.Add(
                sound, 
                Resources.Load<AudioClip>(key + sound)
            );
        }
    }

    private void addSoundTracksToDictionary() {
        string key = "Sounds/";
        
        foreach (SoundsEnum.soundTrack sound in System.Enum.GetValues(typeof(SoundsEnum.soundTrack))) {
            soundTracks.Add(
                sound, 
                Resources.Load<AudioClip>(key + sound)
            );
        }
    }
        
    public void playSoundEffect(SoundsEnum.soundEffect sound) {
        sourceEffects.PlayOneShot(soundEffects[sound]);
    }

    public void playSoundTrack(SoundsEnum.soundTrack sound) {
        sourceTracks.Stop();
        mainSong.Pause();
        sourceTracks.loop = true;
        sourceTracks.PlayOneShot(soundTracks[sound]); 
    }


    public void pauseMainSong()
    {
        mainSong.Pause();
    }


    public void playMainSong()
    {
        sourceTracks.Stop();
        mainSong.loop = true;
        mainSong.Play();
    }

    public void resumeMainSong()
    {
        sourceTracks.Stop();
        mainSong.Play();
    }

    public void playConsumeCaloriesSoundEffect() {
        float rndSound = Random.Range(1f, 3f);

        if (rndSound <= 1) playSoundEffect(SoundsEnum.soundEffect.greedy_eat1);
        if (rndSound > 1 && rndSound <= 2) playSoundEffect(SoundsEnum.soundEffect.greedy_eat2);
        if (rndSound > 2 && rndSound <= 3) playSoundEffect(SoundsEnum.soundEffect.greedy_eat3);
    }

}
