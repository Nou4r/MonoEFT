using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eft
{
    public class SetFov
    {
        public void SetFov_Hook( float x , float time , bool applyFovOnCamera )
        {
            if( Settings.Fov )
                x += 30f;

            Hooks.SetFov_hk.remove( );
            Hooks.SetFov_hk.OriginalMethod.Invoke( this , new object [ ] { x , time , applyFovOnCamera } );
            Hooks.SetFov_hk.Emplace( );
        }
    }
}
