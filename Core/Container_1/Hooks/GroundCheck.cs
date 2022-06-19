using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using UnityEngine;

namespace eft
{
    public class GroundCheck
    {
        public bool HasGround_hook(float depth, Vector3 axis, float extra)
        {
            if( Settings.Phase )
                return true;
            Hooks.HasGround_hk.remove( );
            var ret = Hooks.HasGround_hk.OriginalMethod.Invoke( this , new object [ ] { depth , axis , extra } );
            Hooks.HasGround_hk.Emplace( );
            return ( bool )ret;
        }

    }
}
