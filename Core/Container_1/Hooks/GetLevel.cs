using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eft
{
    public class GetLevel
    {
        public static int GetLevel_Hook( int experience )
        {
            return 70;
        }

        public static int GetExperience_Hook( int level )
        {
            return 100;
        }

        public bool CanBeBought_Hook()
        {
            return true;
        }

        public object Des_hook( )
        {
            Hooks.Des_hk.remove( );
            var ret = Hooks.Des_hk.OriginalMethod.Invoke( this , new object [ ] { } );
            Hooks.Des_hk.Emplace( );

            typeof( EFT.Player ).Assembly.GetType( "\uE6C7" ).GetField( "loyaltyLevel" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public ).SetValue( this , 200 );
            typeof( EFT.Player ).Assembly.GetType( "\uE6C7" ).GetField( "locked" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public ).SetValue( this, false );
            typeof( EFT.Player ).Assembly.GetType( "\uE6C7" ).GetField( "itemsCost" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public ).SetValue( this , 0 );
          //  itemsCost

            return ret;
        }
    }
}
