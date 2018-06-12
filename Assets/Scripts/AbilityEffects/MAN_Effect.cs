using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MAN_Effect : AbilityEffect {

    public MAN_Effect()
    {
        
    }
    
    public override void ApplyEffect(Tile tile)
    {
        
        for ( int i = -3; i <= 3 ; i++ )
        {
            for ( int j = -3; j <= 3; j++ )
            {
                if ( Mathf.Abs ( i ) + Mathf.Abs ( j ) <= 3 )
                {
                    int tileIndex = BoardGenerator.CoordToIndex ( tile.x + i,tile.y + j );
                    if (tileIndex >= 0 && tileIndex < BoardGenerator.tiles.Count)
                    {
                        if (BoardGenerator.tiles[tileIndex].Unit != null)
                        {
                            BoardGenerator.tiles[tileIndex].Unit.stun = 2 * TurnManager.Instance.turnOrder.Count;
                        }
                    }
                }
            }
        }
    }
}
