using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogueBehavior : PlayableBehaviour
{
    [SerializeField] string _dialogueLine = default;
    [SerializeField] ActorSO _actor = default;

    [SerializeField] private bool _pauseWhenClipEnds = default; //This won't work if the clips ends on the very last frame of Timeline

    [HideInInspector] public DialogueLineChannelSO PlayDialogueEvent;
    [HideInInspector] public VoidEventChannelSO PauseTimelineEvent;

    private bool _dialoguePlayed;

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

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        //Check on _dialoguePlayed is needed b/c OnBehaviorPause is called also at the beginning of the Timeline, so we need to make sure
        //that the Timeline has actually gone through this clip (i.e. called OnBehaviorPlay) at least once before we stop it. 
        if (Application.isPlaying && playable.GetGraph().IsPlaying()
            && !playable.GetGraph().GetRootPlayable(0).IsDone()
            && _dialoguePlayed)
        {
            if (_pauseWhenClipEnds)
            {
                if (PauseTimelineEvent != null)
                {
                    PauseTimelineEvent.OnEventRaised();
                }
            }
        }
    }
}
