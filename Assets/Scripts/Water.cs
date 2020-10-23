using UnityEngine;

namespace Eternity
{
    public class Water : MonoBehaviour
    {
        public int initTimer;
        public int timerStep;
        public float moveSpeed;

        private int currentTimerMax;
        private int currentTimerCountdown;
        private float frameTick = 1;
        private Vector3 startPosition;
        private Vector3 endPosition = new Vector3(315, 0, 0);
        private bool crashing;
        private Vector3 movement = new Vector3(1, 0, 0);

        public int TimerDisplay
        {
            get
            {
                return currentTimerCountdown;
            }
        }

        void Start()
        {
            startPosition = transform.position;
            currentTimerMax = initTimer;
            currentTimerCountdown = currentTimerMax;
        }

        void Update()
        {
            if (!crashing)
            {
                frameTick -= Time.deltaTime;
                if (frameTick <= 0)
                {
                    frameTick = 1;
                    currentTimerCountdown -= 1;
                    Debug.Log(currentTimerCountdown);
                }

                if (currentTimerCountdown <= 0)
                {
                    crashing = true;
                }
                return;
            }

            if (crashing && Vector3.Distance(transform.position, endPosition) < 1.0f)
            {
                crashing = false;
                transform.position = startPosition;
                currentTimerMax += timerStep;
                currentTimerCountdown = currentTimerMax;
                return;
            }

            transform.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }
}