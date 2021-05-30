using UnityEngine;
using UnityEngine.Playables; 

/// <summary>
/// Listens on channels to responds to cutscene events.
/// </summary>
public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader = default;
    [SerializeField] private DialogueManager _dialogueManager = default;

    [Header("Listening on channels")]
    [SerializeField] private PlayableDirectorChannelSO _playCutsceneEvent;
    [SerializeField] private DialogueLineChannelSO _playDialogueEvent = default;
    [SerializeField] private VoidEventChannelSO _pauseTimelineEvent = default;
    [SerializeField] private RewindTimelineEventChannelSO _rewindTimelineEvent = default;

    [Header("Broadcasting on channels")]
    [SerializeField] private GameResultChannelSO _gameEndEvent;

    private PlayableDirector _activePlayableDirector;
    private bool _isPaused = false;
    private bool _stopLooping = false; //flag to indicate when advanceDialogueEvent has been pressed
    private uint _loopingCounter = 0;
    private float _advanceTime = 0;
    private bool _isEndingCutscene; //flag for raising load end menu

    bool IsCutscenePlaying => _activePlayableDirector.playableGraph.GetRootPlayable(0).GetSpeed() != 0d;

    private void OnEnable()
    {
        _inputReader.advanceDialogueEvent += OnAdvance;

        if (_playCutsceneEvent != null)
        {
            // Play cutscene when event is broadcasted on PlayableDirectorChannel
            _playCutsceneEvent.OnEventRaised += PlayCutscene;
        }

        if (_playDialogueEvent != null)
        {
            _playDialogueEvent.OnEventRaised += PlayDialogueFromClip;
        }

        if (_pauseTimelineEvent != null)
        {
            _pauseTimelineEvent.OnEventRaised += PauseTimeline;
        }

        if (_rewindTimelineEvent != null)
        {
            _rewindTimelineEvent.OnEventRaised += RewindTimeline;
        }
    }

    private void OnDisable()
    {
        _inputReader.advanceDialogueEvent -= OnAdvance;


        if (_playCutsceneEvent != null)
        {
            // Play cutscene when event is broadcasted on PlayableDirectorChannel
            _playCutsceneEvent.OnEventRaised -= PlayCutscene;
        }

        if (_playDialogueEvent != null)
        {
            _playDialogueEvent.OnEventRaised -= PlayDialogueFromClip;
        }

        if (_pauseTimelineEvent != null)
        {
            _pauseTimelineEvent.OnEventRaised -= PauseTimeline;
        }

        if (_rewindTimelineEvent != null)
        {
            _rewindTimelineEvent.OnEventRaised -= RewindTimeline;
        }
    }


    void PlayCutscene(PlayableDirector activePlayableDirector, bool isEndingCutscene)
    {
        _inputReader.EnableDialogueInput();

        _activePlayableDirector = activePlayableDirector;

        _isPaused = false;
        _isEndingCutscene = isEndingCutscene;
        _activePlayableDirector.Play();
        //When cutscene ends
        _activePlayableDirector.stopped += HandleDirectorStopped;
    }

    void CutsceneEnded()
    {
        if (_activePlayableDirector != null)
            _activePlayableDirector.stopped -= HandleDirectorStopped;

        if(_isEndingCutscene)
        {
            _gameEndEvent.RaiseEvent(true, "0");
        }
        else
        {
            _inputReader.EnableGameplayInput();
            _dialogueManager.DialogueEndedAndCloseDialogueUI();
        }
    }

    private void HandleDirectorStopped(PlayableDirector director) => CutsceneEnded();

    private void PlayDialogueFromClip(string dialogueLine, ActorSO actor)
    {
        _dialogueManager.DisplayDialogueLine(dialogueLine, actor);

    }

    private void OnAdvance()
    {
        if (_isPaused)
        {
           ResumeTimeline();
        }
        else if(_loopingCounter > 1)
        {
            
            AdvanceTimeline(_advanceTime);
        }
    }

    private void PauseTimeline()
    {
        _isPaused = true;
        _activePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    private void ResumeTimeline()
    {
        _isPaused = false;
        _activePlayableDirector.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    private void AdvanceTimeline(float time)
    {
        Debug.Log("Advance time + " + time);

        _activePlayableDirector.playableGraph.GetRootPlayable(0).SetTime(time);
        _stopLooping = true;
        _loopingCounter = 0;
        
    }

    /// <summary>
    /// Triggered by OnBehaviorPause in <see cref="DialgueBehavior"/>. 
    /// </summary>
    /// <param name="rewindTime"></param>
    /// <param name="advanceTime"></param>
    private void RewindTimeline(float rewindTime, float advanceTime)
    {
        //RewindTimeline will be called after AdvanceTimeline b/c AdvanceTimeline causes clip to deactivate and OnBehaviorPause to be called.
        //Therefore, we set _stopLooping to false after RewindTimeline has been called once (right after AdvanceTimeline).
        if(_loopingCounter == 1)
        {
            _stopLooping = false;
        }

        if(!_stopLooping)
        {
            Debug.Log("Inside Rewind Timeline");

            _advanceTime = advanceTime;
            _activePlayableDirector.playableGraph.GetRootPlayable(0).SetTime(rewindTime);
        }
        _loopingCounter++;
    }

}
