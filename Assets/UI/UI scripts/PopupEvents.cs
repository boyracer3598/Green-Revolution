using UnityEngine;
using UnityEngine.UIElements;

public class PopupEvents : MonoBehaviour
{
    private Label messageLabel;
    private Button OkayButton;
    private UIDocument UIDoc;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void UpdatePopUp(string message)
    {
        messageLabel.text = message;
    }

    private void OnEnable()
    {
        UIDoc = gameObject.GetComponent<UIDocument>();
        messageLabel = UIDoc.rootVisualElement.Q<Label>("Message");
        OkayButton = UIDoc.rootVisualElement.Q<Button>("OkayButton");
        OkayButton.RegisterCallback<ClickEvent>(OnOkayButon);
       


    }

    private void OnOkayButon(ClickEvent evt)
    {
        this.gameObject.SetActive(false);
    }
}
