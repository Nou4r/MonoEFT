using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace eft
{
    public class ApplyShot
    {
        public object ApplyShit_Hook( object damageInfo, EBodyPart eBodyPart , object shotid )
        {
          
                
              Hooks.ApplyShot_hk.remove( );
              var ret = Hooks.ApplyShot_hk.OriginalMethod.Invoke( this , new object [ ] { damageInfo , eBodyPart , shotid } );
              Hooks.ApplyShot_hk.Emplace( );
              return ret;
            
        }
    }
}
