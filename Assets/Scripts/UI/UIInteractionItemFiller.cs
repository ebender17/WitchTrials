using UnityEngine;
using TMPro; 

public class UIInteractionItemFiller : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _interactionName = default;
    [SerializeField] private TextMeshProUGUI _interactionKeyButton = default;

    public void FillInteractionPanel(InteractionSO interactionItem)
    {
        _interactionName.text = interactionItem.InteractionName;
        _interactionKeyButton.text = KeyCode.Q.ToString(); //TODO: Change later for different schemes, gamepad, keyboard, etc. 
    }
}
