using UnityEngine;

namespace UltimateBackgroundsCollection
{
    public class Ground : MonoBehaviour
    {
        private Transform player;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }

        private void Update()
        {
            if (player == null) return;
            transform.position = new Vector2(player.position.x, transform.position.y);
        }
    }
}

