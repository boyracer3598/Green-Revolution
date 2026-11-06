using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource SoundObject;
    [SerializeField] private AudioMixer audioMixer;
    public static SoundManager instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void PlaySoundClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        AudioSource audioSource = Instantiate(SoundObject, spawnTransform.position, Quaternion.identity);
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length);
    }

    public void SetVolumeMaster(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetFXVolume(float volume) {
        audioMixer.SetFloat("FXVolume", volume);
    }
    
    public float GetVolume(string name)
    {
        if (audioMixer.GetFloat(name, out float volume))
        {
            return volume;
        }
        return -80f;
    }
    

    public void SetMusicVolume(float volume) {
        audioMixer.SetFloat("MusicVolume", volume);
    }
}


