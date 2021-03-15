using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// Custome timeline clip. Placed on <see cref="DialogueTrack"/>
/// </summary>
public class DialogueClip : PlayableAsset, ITimelineClipAsset
{
    [SerializeField] private DialogueBehavior _template = default;

    [HideInInspector] public DialogueLineChannelSO PlayDialogueEvent;
    [HideInInspector] public VoidEventChannelSO PauseTimelineEvent; 

    //Set ClipsCaps to None to ensure the clips cannot be blended, extrapolated, looped, etc. 
    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        //To use Behavior as playable, must be encapsulated in ScriptPlayable object
        //Clones template and encapsulates withing ScriptPlayable object
        ScriptPlayable<DialogueBehavior> playable = ScriptPlayable<DialogueBehavior>.Create(graph, _template);

        _template.PlayDialogueEvent = PlayDialogueEvent;
        _template.PauseTimelineEvent = PauseTimelineEvent;

        //Generated clone is returned and used as runtime playable
        return playable;
    }
}
