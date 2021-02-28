using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables; 

public class CutsceneManager : MonoBehaviour
{
    //Currently no way of retreiving without going into player gameobject which I do not like
    //[SerializeField] private PlayerInputHandler _inputHandler;
    [SerializeField] private PlayableDirectorChannelSO _playCutsceneEvent;

    private PlayableDirector _activePlayableDirector;

    // Start is called before the first frame update
    private void Start()
    {
        if (_playCutsceneEvent != null)
            // Play cutscene when event is broadcasted on PlayableDirectorChannel
            _playCutsceneEvent.OnEventRaised += PlayCutscene;
    }

    void PlayCutscene(PlayableDirector activePlayableDirector)
    {
        _activePlayableDirector = activePlayableDirector;

        _activePlayableDirector.Play();
        //When cutscene ends
        _activePlayableDirector.stopped += HandleDirectorStopped;
    }

    void CutsceneEnded()
    {
        if (_activePlayableDirector != null)
            _activePlayableDirector.stopped -= HandleDirectorStopped;

        // Input is currently tightly coupled with player
        // TODO: look into decoupling
        //_inputHandler.EnableGameplayInput();
    }

    private void HandleDirectorStopped(PlayableDirector director) => CutsceneEnded();

}
