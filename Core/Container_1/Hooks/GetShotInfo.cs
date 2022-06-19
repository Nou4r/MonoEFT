using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;


namespace eft
{
    public class GetShotInfo
    {
        public object GetShotInfo_Hook(object shot)
        {
            Hooks.ShotInfo_hk.remove( );
            var ret = Hooks.ApplyShot_hk.OriginalMethod.Invoke( null , new object [ ] { shot } );
            Hooks.ShotInfo_hk.Emplace( );

            if(Settings.Hitsounds)
                Globals.Cam.GetComponent<AudioSource>( ).PlayOneShot( Globals.Hitclip );

            return ret;
        }

    }
}
