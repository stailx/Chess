using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    private Vector2[] deplacements = { new Vector2(2, 1), new Vector2(2, -1), new Vector2(1, -2), new Vector2(1, 2) , new Vector2(-2, 1), new Vector2(-2, -1), new Vector2(-1, -2), new Vector2(-1, 2) };
    public override List<Vector2> GetPossibleMoove()
    {
        List<Vector2> listMoove = new List<Vector2>();
        Vector2 Pos = this.transform.position;
        foreach (Vector2 deplacement in deplacements)
        {
            Vector2 newPos = Pos + deplacement;
            Validity v = (IsValid(newPos));
            if (v != Validity.No)
            {
                listMoove.Add(newPos);
            }
        }
        return listMoove;
    }
}
