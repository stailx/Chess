using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    private Vector2[] deplacements = { new Vector2(1, 1), new Vector2(1, 0), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, 0), new Vector2(0,-1) };
    private bool HasAlreadyMooved = false;
    public override List<Vector2> GetPossibleMoove()
    {
        List<Vector2> listMoove = new List<Vector2>();
        Vector2 Pos = this.transform.position;
        foreach (Vector2 deplacement in deplacements)
        {
            Vector2 NewPos = Pos + deplacement;
            Validity v = IsValid(NewPos);
            if(v==Validity.Yes || v==Validity.Last)
            { listMoove.Add(NewPos); }
        }
        return listMoove;
    }
}
