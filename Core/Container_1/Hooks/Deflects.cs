using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eft
{
    public class Deflects
    {
        public bool Deflects_Hook(object shot)
        {
            //Class \uEA7D Method \uE00E
            var m_method = typeof( EFT.Player ).Assembly.GetType( "\uEA7D" ).GetMethod( "\uE00E" , System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic );
            int num = 0;
            int test = ( int )m_method.Invoke( null , new object [ ] { this , num } );

            bool result;
            if(Settings.Ricochet)
            {
                result = ( test < 4 );
            }else
            {
                Hooks.Deflects_hk.remove( );
                var obj = Hooks.Deflects_hk.OriginalMethod.Invoke( this , new object [ ] { shot } );
                Hooks.Deflects_hk.Emplace( );
                result = ( bool )obj;
            }
            return result;       
        }
    }
}
