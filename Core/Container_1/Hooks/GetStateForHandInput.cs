using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using UnityEngine;

namespace eft
{
    public class GetStateForHandInput
    {
        public bool GetStateForHandInput_Hook()
        {
            if( Settings.ShotRunCancel )
                return true;

            Hooks.Always_sprint_hk.remove( );
            var ret = Hooks.Always_sprint_hk.OriginalMethod.Invoke( this , new object [ ] { } );
            Hooks.Always_sprint_hk.Emplace( );

            return ( bool )ret;
        }
    }
}
