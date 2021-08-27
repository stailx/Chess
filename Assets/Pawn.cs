using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    public Mesh whiteQueen;
    public Mesh blackQueen;
    private Vector2 deplacement;
    private bool HasAlreadyMooved = false;
    Vector2[] yOffsets = { new Vector2(0,-1), new Vector2(0, 1) };
    public override void Start()
    {
        base.Start();
        if (color == Color.White)
            deplacement = new Vector2(1, 0);
        else
            deplacement = new Vector2(-1, 0);

    }
    public override bool Moove(Vector2 GridPos)
    {
        foreach (Vector2 pos in GetPossibleMoove())
        {
            if (GridPos == pos)
            {
                grid.Moove(this, GridPos);
                this.gameObject.transform.localPosition = GridPos;
                HasAlreadyMooved = true;
                if (pos.x == 0 || pos.x == 7)
                {
                    Promote();
                }
                return true;
            }
        }
        return false;
    }
    private void Promote()
    {
        Queen queen=this.gameObject.AddComponent<Queen>();
        queen.color = this.color;
        if (this.color == Color.White)
            this.GetComponent<MeshFilter>().sharedMesh = whiteQueen;
        else
            this.GetComponent<MeshFilter>().sharedMesh = blackQueen;
        Destroy(this);
    }
    public override List<Vector2> GetPossibleMoove()
    {
        List<Vector2> listMoove = new List<Vector2>();
        Vector2 Pos = this.transform.position;
        Vector2 candidatePos = Pos + deplacement;
       
        Piece piece = grid.GetPieceAt(candidatePos);
        if (piece==null)
        {
            listMoove.Add(candidatePos);
        }
        if (!HasAlreadyMooved)
        {
            listMoove.Add(candidatePos + deplacement);
        }

        foreach (Vector2 yOffset in yOffsets)
        {
            Piece Eat = grid.GetPieceAt(candidatePos + yOffset);
            if (Eat && Eat.color!=this.color)
            {
                listMoove.Add(candidatePos + yOffset);
            }
        }

        return listMoove;
    }
}
