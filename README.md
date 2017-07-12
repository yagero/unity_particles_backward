# Play Unity ParticleSystem backward

## Extension methods code 
See [ParticleSystemEx.cs](Assets/Scripts/ParticleSystemEx.cs)

## Usage
See [PlayBackwardTest.cs](Assets/Scripts/PlayBackwardTest.cs)

```
public class Test : MonoBehaviour
{
    public void PlayBackward()
    {
        var ps = GetComponentInChildren<ParticleSystem>();
        ps.PlayBackward(this);
        // or use StartCoroutine(ps.CoPlayBackward());
    }
}
```
