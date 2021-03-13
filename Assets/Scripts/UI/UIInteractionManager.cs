using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractionManager : MonoBehaviour
{
    [SerializeField] private List<InteractionSO> _listInteractions = default;

    [SerializeField] private UIInteractionItemFiller _interactionItem = default; 

    public void FillInteractionPanel(InteractionType interactionType)
    {
        if((_listInteractions != null) && (_interactionItem != null))
        {
            //Check if interactionType exists in list of interactions
            if(_listInteractions.Exists(o => o.InteractionType == interactionType))
            {
                //Fill interaction panel if interactionType is present
                _interactionItem.FillInteractionPanel(_listInteractions.Find(o => o.InteractionType == interactionType));
            }
        }
    }
}
