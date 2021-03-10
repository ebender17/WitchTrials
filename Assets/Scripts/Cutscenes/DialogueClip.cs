using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogueClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] private DialogueBehavior _template = default;

    [HideInInspector] public DialogueLineChannelSO PlayDialogueEvent;
    [HideInInspector] public VoidEventChannelSO PauseTimelineEvent; 
    //Having ClipCaps set to None makes sure the clips cannot be blended, extrapolated, looped, etc. 
    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        ScriptPlayable<DialogueBehavior> playable = ScriptPlayable<DialogueBehavior>.Create(graph, _template);

        _template.PlayDialogueEvent = PlayDialogueEvent;
        _template.PauseTimelineEvent = PauseTimelineEvent;

        return playable;
    }
}
