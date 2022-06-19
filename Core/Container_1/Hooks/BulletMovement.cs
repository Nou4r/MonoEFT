using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;

namespace eft
{
    public class BulletMovement
    {
        public bool BulletMovement_Hook(Vector3 prev, Vector3 next)
        {
            Hooks.BulletMovement_hk.remove( );
            bool ret = (bool)Hooks.BulletMovement_hk.OriginalMethod.Invoke( this , new object [ ] { prev , next } );
            Hooks.BulletMovement_hk.Emplace( );
            //Class \uEA7D | Hit field: \uE009
            var TimeSinceShot = (float)typeof( EFT.Player ).Assembly.GetType( "\uEA7D" ).GetField( "TimeSinceShot" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public ).GetValue( this );
            var Player = ( EFT.Player )typeof( EFT.Player ).Assembly.GetType( "\uEA7D" ).GetField( "Player" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public ).GetValue( this );
            var hit = ( RaycastHit )typeof( EFT.Player ).Assembly.GetType( "\uEA7D" ).GetField( "\uE009" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic ).GetValue( this );

            if( Player == Globals.Local && TimeSinceShot < 3f ) 
            {
                CheatStructs.Tracer tracer = new CheatStructs.Tracer( );
                tracer.Time = Time.time;
                tracer.StartPos = prev;
                tracer.EndPos = hit.point;

                if( tracer.EndPos.x == 0f && tracer.EndPos.y == 0f && tracer.EndPos.z == 0f )
                    tracer.EndPos = next;

                Globals.TracerList.Add( tracer );
            }

            return ret;
        }

    }
}
