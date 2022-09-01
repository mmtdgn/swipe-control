<!--
  Copyright (c) 2022 Mehmet

  Written by Mehmet Doğan <mmt.dgn.6634@gmail.com>, august 2022
-->
# swipe-control
Swipe controller for games.

<details>
<summary><b>Screenshot</b></summary>
  
<img src="/.github/screenshots/swipe.png">
</details>

### Usage
1. Add `SwipeController` script to your scene.  
  1.Change `swipe sens` field as you desire for change the sensitivity.
2. Register to `OnSwiped` event from `SwipeController' as where you need it.

### Code Documentation
| Type                     | Description                                        |
| ------------------------ | -------------------------------------------------- |
| `Action<SwipeDirection>` | Invokes after swipe with swipe direction           |
| `Swipe Direction`        | Define of swipe direction. Left, Right, Up or Down |
| `SwipeMode`              | Distance mode, Input cut mode                     |
  
### SwipeMode
  `Distance Mode` : `OnSwipe` event invokes after reached to desired swipe distance.  
  `InputCut Mode` : `OnSwipe` event waits for the finger&mouse up to invoke.
  
  ### Construct swipe
```c
    public class SwipeController : MonoBehaviour
    {
        public static event Action<SwipeDirection> OnSwiped;
        [SerializeField] protected float m_SwipeSens = 25f;
        protected Swipe m_Swipe;
        protected bool m_IsReady;

        private void Awake()
        {
            m_Swipe = new Swipe(this, m_SwipeSens , SwipeMode.Distance);
            m_IsReady = true;
        }
    }
```
  
### Register Example
```c
public class Test : MonoBehaviour
{
    private void Awake()
    {
        SwipeController.OnSwiped += OnSwiped;
    }
  
    private void OnDestroy()
    {
        SwipeController.OnSwiped -= OnSwiped;
    }

    private void OnSwiped(SwipeDirection direction)
    {
#if DEBUG
        Debug.LogWarning("Move\n" + Enum.GetName(typeof(SwipeDirection), direction));
#endif
    }
}
```
