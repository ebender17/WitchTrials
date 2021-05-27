using System;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Acts as the data for <see cref="DialogueTrack"/>
/// Instantances of this script represent runtime playables.
/// </summary>
[Serializable]
public class DialogueBehavior : PlayableBehaviour
{
    [SerializeField] private string _dialogueLine = default;
    [SerializeField] private ActorSO _actor = default;

    //TODO: Custom Editor only shows rewind time when _rewindWhenClipEnds is checked. Only allows you to check one of bools.
    [SerializeField] private bool _pauseWhenClipEnds = default; //This won't work if the clips ends on the very last frame of Timeline
    [SerializeField] private bool _rewindWhenClipEnds = default;
    [SerializeField] private float _rewindTime = 0f;
    [SerializeField] private float _advanceTime = 0f;

    [HideInInspector] public DialogueLineChannelSO PlayDialogueEvent;
    [HideInInspector] public VoidEventChannelSO PauseTimelineEvent;
    [HideInInspector] public RewindTimelineEventChannelSO RewindTimelineEvent;

    private bool _dialoguePlayed = false;

    //Called each frame when timeline is played
    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        if (_dialoguePlayed)
            return;

        if(Application.isPlaying)
        {
            //Need to ask the CutsceneManager if the cutscene is playing, since the graph is not actually stopped/paused: it is just going at speed 0. 
            
            if(playable.GetGraph().IsPlaying())
            {
                if(_dialogueLine != null && _actor != null)
                {
                    if (PlayDialogueEvent != null)
                        PlayDialogueEvent.RaiseEvent(_dialogueLine, _actor);
                    _dialoguePlayed = true;
                }
                else
                {
                    Debug.LogWarning("This clip contains no DialogueLine");
                }
            }
        }
    }

    /// <summary>
    /// OnBehaviorPause is called when clip becomes deactivated. This occurs when the timeline starts, when the clip is passed it's duration, or if the timeline is stopped.
    /// </summary>
    /// <param name="playable"></param>
    /// <param name="info"></param>
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        //Check on _dialoguePlayed is needed b/c OnBehaviorPause is called also at the beginning of the Timeline, so we need to make sure
        //that the Timeline has actually gone through this clip (i.e. called OnBehaviorPlay) at least once before we stop it. 
        if (Application.isPlaying && playable.GetGraph().IsPlaying()
            && !playable.GetGraph().GetRootPlayable(0).IsDone()
            && _dialoguePlayed)
        {
            if (_pauseWhenClipEnds && !_rewindWhenClipEnds)
            {
                if (PauseTimelineEvent != null)
                {
                    PauseTimelineEvent.RaiseEvent();
                }
            }

            else if (!_pauseWhenClipEnds && _rewindWhenClipEnds)
            {
                if (RewindTimelineEvent != null)
                {
                    RewindTimelineEvent.RaiseEvent(_rewindTime, _advanceTime);
                }
            }
        }
    }
}
