using UnityEngine;

namespace MD.Swipe
{
    public enum SwipeMode { Distance, InputCut }
    public class Swipe
    {
        protected SwipeController m_SwipeController;
        protected Vector2 m_MouseDownPos, m_MouseUpPos, m_DeltaPos;
        protected SwipeMode m_SwipeMode;
        protected float m_SwerveSens = 25f;
        protected bool m_IsInvoked;

        /// <summary>
        /// Constructor, Initialize swiper.
        /// </summary>
        /// <param name="swipeController">Event sender reference</param>
        /// <param name="swerveSens">How much distance is required to invoke the swipe?</param>
        /// <param name="swipeMode">swipe Mode : SwipeMode.InputCut, SwipeMode.Distance -> Default : SwipeMode.InputCut  </param>
        public Swipe(SwipeController swipeController, float swerveSens
        , SwipeMode swipeMode = SwipeMode.InputCut)
        {
            m_SwerveSens = swerveSens;
            m_SwipeController = swipeController;
            m_SwipeMode = swipeMode;
        }
        /// <summary>
        /// Constructor, Initialize swiper.
        /// </summary>
        /// <param name="swipeController">Event sender reference</param>
        public Swipe(SwipeController swipeController)
        {
            m_SwipeController = swipeController;
        }

        public void OnSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_MouseDownPos = (Vector2)Input.mousePosition;
                m_IsInvoked = false;
            }
            switch (m_SwipeMode)
            {
                case SwipeMode.Distance: OnDistanceEnoughToSwipe(); break;
                case SwipeMode.InputCut: OnInputCut(); break;
                default: break;
            }
        }

        private void OnDistanceEnoughToSwipe()
        {
            if (Input.GetMouseButton(0) && !m_IsInvoked)
            {
                SwipeControl();
            }
        }

        private void OnInputCut()
        {
            if (Input.GetMouseButtonUp(0) && !m_IsInvoked)
            {
                SwipeControl();
            }
        }

        private void SwipeControl()
        {
            m_MouseUpPos = (Vector2)Input.mousePosition;
            m_DeltaPos = m_MouseUpPos - m_MouseDownPos;

            if (Mathf.Abs(m_DeltaPos.x) > Mathf.Abs(m_DeltaPos.y))
            {
                if (Mathf.Abs(m_DeltaPos.x) < m_SwerveSens) return;

                if (Mathf.Sign(m_DeltaPos.x) == 1)
                    InvokeSwipe(SwipeDirection.Right, m_SwipeMode);
                else if (Mathf.Sign(m_DeltaPos.x) == -1)
                    InvokeSwipe(SwipeDirection.Left, m_SwipeMode);
            }
            else
            {
                if (Mathf.Abs(m_DeltaPos.y) < m_SwerveSens) return;

                if (Mathf.Sign(m_DeltaPos.y) == 1)
                    InvokeSwipe(SwipeDirection.Up, m_SwipeMode);
                else if (Mathf.Sign(m_DeltaPos.y) == -1)
                    InvokeSwipe(SwipeDirection.Down, m_SwipeMode);
            }
        }

        private void InvokeSwipe(SwipeDirection direction, SwipeMode swipeMode)
        {
            m_SwipeController.OnSwipe(direction);

            if (swipeMode == SwipeMode.Distance)
            {
                m_MouseDownPos = (Vector2)Input.mousePosition;
                m_DeltaPos = Vector2.zero;
                m_IsInvoked = true;
                return;
            }
        }
    }
}
