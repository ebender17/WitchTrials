using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Events/Playable Director Channel")]
public class PlayableDirectorChannelSO : ScriptableObject
{
    public UnityAction<PlayableDirector, bool> OnEventRaised;

    public void RaiseEvent(PlayableDirector playable, bool isEndingCutscene)
    {
        if (OnEventRaised != null)
            OnEventRaised.Invoke(playable, isEndingCutscene);
    }
}
