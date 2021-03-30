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

    [Header("Broadcasting on channels")]
    [SerializeField] private GameResultChannelSO _gameEndEvent;

    private PlayableDirector _activePlayableDirector;
    private bool _isPaused;
    private bool _isEndingCutscene; //flag for raising load end menu

    bool IsCutscenePlaying => _activePlayableDirector.playableGraph.GetRootPlayable(0).GetSpeed() != 0d;

    private void OnEnable()
    {
        _inputReader.advanceDialogueEvent += OnAdvance;
    }

    private void OnDisable()
    {
        _inputReader.advanceDialogueEvent -= OnAdvance;
    }

    private void Start()
    {
        if (_playCutsceneEvent != null)
            // Play cutscene when event is broadcasted on PlayableDirectorChannel
            _playCutsceneEvent.OnEventRaised += PlayCutscene;
        if (_playDialogueEvent != null)
            _playDialogueEvent.OnEventRaised += PlayDialogueFromClip;
        if (_pauseTimelineEvent != null)
            _pauseTimelineEvent.OnEventRaised += PauseTimeline;
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
            //TODO: Game Result remove player score
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
            ResumeTimeline();

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

}
