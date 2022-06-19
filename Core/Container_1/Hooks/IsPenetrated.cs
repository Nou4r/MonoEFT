using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using UnityEngine;

namespace eft
{
    public class IsPenetrated
    {
        public bool IsPenetrated_Hook(object shot, Vector3 hitPoint)
        {
            bool ret;
            if( Settings.Ricochet )
                ret = false;
            else
            {
                Hooks.Penetrated_hk.remove( );
                var obj = Hooks.Penetrated_hk.OriginalMethod.Invoke( this , new object [ ] { shot , hitPoint } );
                Hooks.Penetrated_hk.Emplace( );
                ret = ( bool )obj;
            }

            return ret;
        }
    
    }
}
