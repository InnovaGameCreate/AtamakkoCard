using UnityEngine;
using UnityEngine.Playables;

public class DestroyEffect : MonoBehaviour
{
    void Start()
    {
        GameObject o;
        var effect = (o = gameObject).GetComponent<PlayableDirector>();
        Destroy(o, (float)effect.duration);
    }
}
