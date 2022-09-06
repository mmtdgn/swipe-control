using System;
using UnityEngine;

namespace MD.Swipe
{
    public enum SwipeDirection { Left, Right, Up, Down };
    public class SwipeController : MonoBehaviour
    {
        public static event Action<SwipeDirection> OnSwiped;
        [SerializeField] protected float m_SwipeSens = 25f;
        protected Swipe m_Swipe;
        protected bool m_IsReady;

        private void Awake()
        {
            m_Swipe = new Swipe(this, m_SwipeSens, SwipeMode.Distance);
            m_IsReady = true;
        }

        /// <summary>
        /// swipe is not allowed when level complete.
        /// </summary>
        private void OnLevelCompleted()//Listen level completed event.
        {
            m_IsReady = false;
        }

        /// <summary>
        /// Reset on level change.
        /// </summary>
        private void OnLevelChange()//listen level change event.
        {
            m_IsReady = true;
        }

        private void Update()
        {
            if (!m_IsReady) return;
            SwipeCheck();
        }

        /// <summary>
        /// Check if it is swipeable. If so, then swipe.
        /// </summary>
        private void SwipeCheck()
        {
            if (SwipeUtilities.IsTouchBlockedbyAnyObj() || SwipeUtilities.IsPointerOverUI()) return;
            m_Swipe.OnSwipe();
        }

        public void OnSwipe(SwipeDirection swipeDirection)
        {
            OnSwiped?.Invoke(swipeDirection);
#if DEBUG
            Debug.LogWarning("Move\n " + Enum.GetName(typeof(SwipeDirection), swipeDirection));
#endif
        }
    }
}