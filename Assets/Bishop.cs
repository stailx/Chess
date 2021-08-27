using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    private Vector2[] deplacements = { new Vector2(1, 1), new Vector2(-1, 1), new Vector2(1, -1), new Vector2(-1, -1) };
    public override List<Vector2> GetPossibleMoove()
    {
        List<Vector2> listMoove = new List<Vector2>();
        Vector2 Pos = this.transform.position;
        foreach (Vector2 deplacement in deplacements)
        {
            int i = 1;
            while (i<8)
            {
                Vector2 newPos = Pos + i * deplacement;
                Validity v = (IsValid(newPos));
                if (v == Validity.Yes)
                {
                    listMoove.Add(newPos);
                    i++;
                }
                else if (v == Validity.No)
                {
                    break;
                }
                else
                {
                    listMoove.Add(newPos);
                    break;
                }

            }
        }
        return listMoove;
    }
}
