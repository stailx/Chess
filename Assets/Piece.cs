using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    // Start is called before the first frame update
    public enum Validity { Yes, Last ,No};
    public enum Color { White,Black};
    public Color color;
    protected TerrainManager grid;
    public virtual void Start()
    {
        grid = FindObjectOfType<TerrainManager>();
    }

    public abstract List<Vector2> GetPossibleMoove();
    public virtual bool Moove(Vector2 GridPos)
    {
        foreach (Vector2 pos in GetPossibleMoove())
        {
            if (GridPos == pos)
            {
                grid.Moove(this, GridPos);
                this.gameObject.transform.localPosition = GridPos;

                return true;
            }
        }
        return false;
    }
    protected Validity IsValid(Vector2 pos)
    {
        if (Mathf.Max(pos.x, pos.y) < 8 && Mathf.Min(pos.x, pos.y) > -1)
        {
            Piece piece = grid.GetPieceAt(pos);
            if (piece)
            {
                if (piece.color == this.color)
                {
                    return Validity.No;
                }
                else {
                    return Validity.Last;
                }
            }
            else
            {
                return Validity.Yes;
            }
        }
        return Validity.No;

    }

}
