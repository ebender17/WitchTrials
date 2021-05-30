using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

/// <summary>
/// Custom timeline track for dialogue. Contains <see cref="DialogueClip"/>
/// No MixerBehavior for dialogue track as we do not want to blend clips. 
/// </summary>
[TrackColor(55f/255f, 205/255f, 225f/255f)]
[TrackClipType(typeof(DialogueClip))]
public class DialogueTrack : PlayableTrack
{
    [SerializeField] public DialogueLineChannelSO PlayDialogueEvent;
    [SerializeField] public VoidEventChannelSO PauseTimelineEvent;
    [SerializeField] public RewindTimelineEventChannelSO RewindTimelineEvent;
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (TimelineClip clip in GetClips())
        {
            DialogueClip dialogueControlClip = clip.asset as DialogueClip;
            dialogueControlClip.PlayDialogueEvent = PlayDialogueEvent;
            dialogueControlClip.PauseTimelineEvent = PauseTimelineEvent;
            dialogueControlClip.RewindTimelineEvent = RewindTimelineEvent;

        }

        return base.CreateTrackMixer(graph, go, inputCount);
    }
}
