using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using UnityEngine;

namespace eft
{
    public class CanStand
    {
        public bool CanStandAt_Hook( float h )
        {
            if( Settings.Phase )
                return true;


            Hooks.CanStand_hk.remove( );
            var ret = Hooks.CanStand_hk.OriginalMethod.Invoke( this, new object [ ] { h } );
            Hooks.CanStand_hk.Emplace( );
            return (bool) ret;
        }
    }
}