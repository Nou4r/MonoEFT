using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using UnityEngine;


namespace eft
{
    public class CanStartNewSearchOperation
    {
        public bool CanStartNewSearchOperation_hook( )
        {
            if( Settings.MultiSearch )
                return true;

            Hooks.CanStartNewSearchOperation_hk.remove(  );
            var ret = Hooks.CanStartNewSearchOperation_hk.OriginalMethod.Invoke( this, new object [ ] { } );
            Hooks.CanStartNewSearchOperation_hk.Emplace(  );
            return (bool) ret;
        }
    }
}
