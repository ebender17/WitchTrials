using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Triggers cutscenes. Attach to colliders for OnTriggerEnter.
/// </summary>
public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector _playableDirector = default;
    [SerializeField] private bool isEndingCutscene = default;
    [SerializeField] private bool _playOnStart = default;
    [SerializeField] private bool _playOnce = default;

    [SerializeField] private PlayableDirectorChannelSO _playCutsceneEvent; 

    // Start is called before the first frame update
    void Start()
    {
        if (_playOnStart)
            if (_playCutsceneEvent != null)
                _playCutsceneEvent.RaiseEvent(_playableDirector, isEndingCutscene);

        //Removes this trigger cutscene script
        if (_playOnce)
        {
            //Destroy(this);
        }
            
    }

    //Trigger if attached to game object with collision and IsTrigger checked
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Cutscene trigger entered!");
        if (_playCutsceneEvent != null)
            _playCutsceneEvent.RaiseEvent(_playableDirector, isEndingCutscene);

        //Removes this trigger cutscene script
        if (_playOnce)
        {
            //Destroy(this);
        }

    }
}
