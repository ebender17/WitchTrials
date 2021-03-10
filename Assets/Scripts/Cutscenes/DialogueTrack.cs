using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[TrackClipType(typeof(DialogueClip))]
public class DialogueTrack : PlayableTrack
{
    [SerializeField] public DialogueLineChannelSO PlayDialogueEvent;
    [SerializeField] public VoidEventChannelSO PauseTimelineEvent;
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        foreach (TimelineClip clip in GetClips())
        {
            DialogueClip dialogueControlClip = clip.asset as DialogueClip;
            dialogueControlClip.PlayDialogueEvent = PlayDialogueEvent;
            dialogueControlClip.PauseTimelineEvent = PauseTimelineEvent;
        }

        return base.CreateTrackMixer(graph, go, inputCount);
    }
}
