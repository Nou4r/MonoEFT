using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace eft
{
    public static class CheatCam
    {
        public static Vector3 w2s(Vector3 pos )
        {
            var cam = Globals.Cam;
            if( !cam )
                return new Vector3( 0 , 0 , 0 );

            var w2s_point = Globals.Cam.WorldToScreenPoint( pos );
            w2s_point.y = Screen.height - w2s_point.y;

            if( w2s_point.z < 0.01f )
                return new Vector3( 0 , 0 , 0 );

            return w2s_point;
        }
    }
}
