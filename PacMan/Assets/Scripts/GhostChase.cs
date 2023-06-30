using UnityEngine;

public class GhostChase : GhostBehaviour
{
    private void OnDisable() 
    {
        this.ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Node node = other.GetComponent<Node>();

        if (GameManager.instance.IsGameStarted)
        {
            if (node != null && this.enabled && !this.ghost.frightened.enabled)
            {
                Vector2 direction = Vector2.zero;
                float minDistance = float.MaxValue;

                foreach (Vector2 availableDirecion in node.availableDirections)
                {
                    Vector3 newPosition = this.transform.position + new Vector3(availableDirecion.x, availableDirecion.y, 0.0f);
                    float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                    if (distance < minDistance)
                    {
                        direction = availableDirecion;
                        minDistance = distance;
                    }
                }

                this.ghost.movement.SetDirection(direction);
            }
        }
    }
}
