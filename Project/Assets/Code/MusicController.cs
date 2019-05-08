using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 // Load all sprites in atlas


public class MusicController : MonoBehaviour {
    private Dictionary<SoundsEnum.soundEffect, AudioClip> soundEffects;
    private Dictionary<SoundsEnum.soundTrack, AudioClip> soundTracks;

    public AudioSource sourceEffects, sourceTracks;

    private void Awake() {
        sourceTracks.loop = true;

        soundEffects = new Dictionary<SoundsEnum.soundEffect, AudioClip>();
        soundTracks = new Dictionary<SoundsEnum.soundTrack, AudioClip>(); 

        addSoundEffectsToDictionary();       
        //addSoundTracksToDictionary(); TODO
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
    void playSoundTrack(SoundsEnum.soundTrack sound) {
        //sourceTracks.PlayOneShot(soundTracks[sound]); TODO
    }
}
