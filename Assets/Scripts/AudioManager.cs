using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   
    public static AudioManager Instance;
    public AudioList[] audiolist;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

       

      

        // Keep AudioManager alive between scenes
        DontDestroyOnLoad(gameObject);

        // Initialize audio sources for each sound
        foreach (AudioList a in audiolist)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.pitch = a.pitch;
            a.source.loop = a.loop;
            a.source.outputAudioMixerGroup = a.mixerGroup; 
        }


    }

    private void Start()
    {
        playSound("BackGroundMusic");
    }

    public void playSound(string name)
    {
       AudioList s = Array.Find(audiolist, AudioList => AudioList.name == name);
        s.source.Play();
    }
    public void StopSound(string name)
    {
        AudioList s = Array.Find(audiolist, AudioList => AudioList.name == name);
        s.source.Stop();
    }


    

    
}
