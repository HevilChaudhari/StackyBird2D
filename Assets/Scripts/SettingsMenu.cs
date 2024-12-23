using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Slider masterVol, musicVol, sfxVol;
    [SerializeField] private AudioMixer audioMixer;


    private void Awake()
    {
        masterVol.value = PlayerPrefs.GetFloat("MasterVol", 0);
        musicVol.value = PlayerPrefs.GetFloat("MusicVol", 0);
        sfxVol.value = PlayerPrefs.GetFloat("SFXVol", 0);
    }

    private void Start()
    {
        ChangeMasterVolume();
        ChangeMusicVolume();
        ChangeSFXVolume();
    }

    public void ChangeMasterVolume()
    {
        audioMixer.SetFloat("MasterVol", masterVol.value);
        PlayerPrefs.SetFloat("MasterVol", masterVol.value);
    }

    public void ChangeMusicVolume()
    {
        audioMixer.SetFloat("MusicVol", musicVol.value);
        PlayerPrefs.SetFloat("MusicVol", musicVol.value);
    }

    public void ChangeSFXVolume()
    {
        audioMixer.SetFloat("SFXVol", sfxVol.value);
        PlayerPrefs.SetFloat("SFXVol", sfxVol.value);
    }
}
