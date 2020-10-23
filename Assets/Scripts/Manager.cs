using UnityEngine;

namespace Eternity
{
    public class Manager : MonoBehaviour
    {
        public GameObject player;
        public ObjectPool enemyPool;
        public ObjectPool digsitePool;
        public Water water;

        void Awake()
        {
            player.transform.position = new Vector3(0,0,0);
        }

        void Update()
        {

        }

        public enum GameState
        {
            InRound,
            Crashing,
            Crashed
        }
    }
}