using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using UnityEngine;

namespace eft
{
    public class SilentAim
    {
        public object SilentAim_Hook(object ammo, Vector3 origin, Vector3 direction, int fireIndex , Player player , EFT.InventoryLogic.Item weapon , float speedFactor , int fragmentIndex )
        {
            if(Settings.SilentAim)
            {
                if(Globals.Target != null)
                {
                    direction = ( Globals.Target.PlayerBones.Head.position - origin ).normalized;
                }
            }

            Hooks.SilentAim_hk.remove( );
            var ret = Hooks.SilentAim_hk.OriginalMethod.Invoke( this , new object [ ] { ammo , origin , direction , fireIndex , player , weapon , speedFactor , fragmentIndex } );
            Hooks.SilentAim_hk.Emplace( );
            return ret;
        }
          

    }
}
