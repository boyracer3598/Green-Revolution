using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ChallengeEvents : MonoBehaviour
{
    private Label challengeLabel;
    private Button OkayButton;
    private UIDocument UIDoc;
    [SerializeField] WorldVariables worldVariables;


    private void OnEnable()
    {
        UIDoc = gameObject.GetComponent<UIDocument>();
        challengeLabel = UIDoc.rootVisualElement.Q<Label>("Message");
        OkayButton = UIDoc.rootVisualElement.Q<Button>("OkayButton");
        OkayButton.RegisterCallback<ClickEvent>(OnOkayButon);
        if(Time.frameCount > 10 ) UpdateChallengeMessage();


    }

    private void OnOkayButon(ClickEvent evt)
    {
        this.gameObject.SetActive(false);
    }

    private void UpdateChallengeMessage()
    {

        challengeLabel.text = worldVariables.ChallengeToString();
    }

  
}
