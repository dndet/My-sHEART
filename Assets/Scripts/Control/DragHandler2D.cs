using UnityEngine;

public class DragHandler2D : MonoBehaviour
{
    private bool isDragging = false;
    private Vector2 offset;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    Collider2D collider = Physics2D.OverlapPoint(touchPosition);
                    if (collider != null && collider.gameObject == gameObject)
                    {
                        isDragging = true;
                        offset = (Vector2)transform.position - touchPosition;
                    }
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        transform.position = touchPosition + offset;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
    }
}
