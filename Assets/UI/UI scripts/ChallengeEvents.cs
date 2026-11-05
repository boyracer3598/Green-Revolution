using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ChallengeEvents : MonoBehaviour
{
    private Label challengeLabel;
    private Button OkayButton;
    private UIDocument UIDoc;

    private void OnEnable()
    {
        UIDoc = gameObject.GetComponent<UIDocument>();
        challengeLabel = UIDoc.rootVisualElement.Q<Label>("Message");
        OkayButton = UIDoc.rootVisualElement.Q<Button>("OkayButton");
        OkayButton.RegisterCallback<ClickEvent>(OnOkayButon);
    }

    private void OnOkayButon(ClickEvent evt)
    {
        this.gameObject.SetActive(false);
    }

  
}
