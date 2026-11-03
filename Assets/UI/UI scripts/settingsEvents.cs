using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class settingsEvents : MonoBehaviour
{
    private UIDocument UIDoc;
    private Button backButton;
    private Slider FXVolume;
    private Slider MusicVolume;
    public GameObject prevPanel;
    void OnEnable()
    {
        UIDoc = GetComponent<UIDocument>();
        
        //button setup
        backButton = UIDoc.rootVisualElement.Q<Button>("backButton");
        backButton.RegisterCallback<ClickEvent>(onBackClick);

        //slider setup
        MusicVolume = UIDoc.rootVisualElement.Q<Slider>("music");
        FXVolume = UIDoc.rootVisualElement.Q<Slider>("FX");
        

    }

    public void onBackClick(ClickEvent evt)
    {
        prevPanel.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void ChangeVolume(float Volume )
    {
        //sets audio
    }
    
    void Update()
    {
        
    }
}
