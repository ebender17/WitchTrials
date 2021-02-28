using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Triggers cutscenes. Attach to colliders for OnTriggerEnter.
/// </summary>
public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector = default;
    [SerializeField] private bool _playOnStart = default;
    [SerializeField] private bool _playOnce = default;

    [SerializeField] private PlayableDirectorChannelSO _playCutsceneEvent; 

    // Start is called before the first frame update
    void Start()
    {
        if (_playOnStart)
            if (_playCutsceneEvent != null)
                _playCutsceneEvent.RaiseEvent(_playableDirector);

        //Removes this trigger cutscene script
        if (_playOnce)
            Destroy(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_playCutsceneEvent != null)
            _playCutsceneEvent.RaiseEvent(_playableDirector);

        //Removes this trigger custscene script
        if (_playOnce)
            Destroy(this);
    }
}
