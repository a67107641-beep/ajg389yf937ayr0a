using UnityEngine;

public class Block : MonoBehaviour
{
    public ElementData element;

    private void Start()
    {
        if (element == null)
        {
            Debug.LogError("ElementData Ç™ê›íËÇ≥ÇÍÇƒÇ¢Ç‹ÇπÇÒ", this);
            return;
        }

        var sr = GetComponent<SpriteRenderer>();
        sr.color = element.elementColor;
    }

    //public void RegisterToBoard()
    //{
    //    int x = Mathf.RoundToInt(transform.position.x);
    //    int y = Mathf.RoundToInt(transform.position.y);

    //    BoardManager.Instance.PlaceBlock(this, x, y);
    //}

}
