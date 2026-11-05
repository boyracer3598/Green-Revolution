using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class settingsEvents : MonoBehaviour
{
    private UIDocument UIDoc;
    private Button backButton;
    private SliderInt FXVolume;
    private SliderInt MusicVolume;
    private Toggle MuteSounds;
    public GameObject prevPanel;
    void OnEnable()
    {
        UIDoc = GetComponent<UIDocument>();
        
        //button setup
        backButton = UIDoc.rootVisualElement.Q<Button>("backButton");
        backButton.RegisterCallback<ClickEvent>(onBackClick);

        //slider setup
        MusicVolume = UIDoc.rootVisualElement.Q<SliderInt>("music");
        FXVolume = UIDoc.rootVisualElement.Q<SliderInt>("FX");
        FXVolume.value = (int)SoundManager.instance.GetVolume("FXVolume");
        MusicVolume.value = (int)SoundManager.instance.GetVolume("MusicVolume");
        MusicVolume.RegisterValueChangedCallback(onSliderMusicChange);
        FXVolume.RegisterValueChangedCallback(onSliderFXChange);
        
        //mute sound setup
        MuteSounds= UIDoc.rootVisualElement.Q<Toggle>("MuteSounds");
        if (SoundManager.instance.GetVolume("MasterVolume") <= -80)
        {
            MuteSounds.value = true;
        }
        else
        {
            MuteSounds.value = false;
        }
        MuteSounds.RegisterValueChangedCallback(ToggleMuteSounds);

    }
    public void onBackClick(ClickEvent evt)
    {
        prevPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    private void onSliderMusicChange(ChangeEvent<int> evt)
    {
        SoundManager.instance.SetMusicVolume(evt.newValue);
    }

    private void onSliderFXChange(ChangeEvent<int> evt)
    {
        SoundManager.instance.SetFXVolume(evt.newValue);
    }

    private void ToggleMuteSounds(ChangeEvent<bool> evt)
    {
        if (MuteSounds.value)
        {
            SoundManager.instance.SetVolumeMaster(-80);
        }
        else
        {
            SoundManager.instance.SetVolumeMaster(0);
        }
        
    }
        
}
