using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace eft
{
    public static class Hooks
    {
		private static bool is_hooked = true;

		public static DumbHook Penetrated_hk;
        public static DumbHook CreateShot_hk;
		public static DumbHook SetFov_hk;
		public static DumbHook Always_sprint_hk;
		public static DumbHook SilentAim_hk;
		public static DumbHook BulletMovement_hk;
		public static DumbHook Deflects_hk;
		public static DumbHook ApplyShot_hk;
		public static DumbHook CanStand_hk;
		public static DumbHook HasGround_hk;
		public static DumbHook Console_hk;
		public static DumbHook Level_hk;
		public static DumbHook Xp_hk;
		public static DumbHook Bought;
		public static DumbHook Des_hk;
		public static DumbHook ShotInfo_hk;
        public static DumbHook CanStartNewSearchOperation_hk;

		public static Type Find( string type )
		{
			foreach( Type type2 in System.Reflection.Assembly.GetAssembly( typeof( AICell ) ).GetTypes( ) )
			{
				if( type == type2.Name || type == type2.FullName )
				{
					return type2;
				}
			}
			return null;
		}

		public static void run()
        {


			if( Globals.World || Globals.Local )
            {
				if( is_hooked )
                {
					SilentAim_hk = new DumbHook( );
					SilentAim_hk.Init( typeof( EFT.Ballistics.BallisticsCalculator ).GetMethod( "CreateShot" ) , typeof( SilentAim ).GetMethod( "SilentAim_Hook" ) );
					SilentAim_hk.Emplace( );

					var CreateShotMethod = Find( "\uEA7D" ).GetMethod( "\uE004" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic );
					CreateShot_hk = new DumbHook( );
					CreateShot_hk.Init( CreateShotMethod , typeof( CreateShot ).GetMethod( "CreateShot_hook" ) );
					CreateShot_hk.Emplace( );

					var SetFovMethod = Find( "\uE776" ).GetMethod( "SetFov" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public );
					SetFov_hk = new DumbHook( );
					SetFov_hk.Init( SetFovMethod , typeof( SetFov ).GetMethod( "SetFov_Hook" ) );
					SetFov_hk.Emplace( );

					Always_sprint_hk = new DumbHook( );
					Always_sprint_hk.Init( typeof( EFT.Player ).GetMethod( "get_StateIsSuitableForHandInput" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public ), typeof( GetStateForHandInput ).GetMethod( "GetStateForHandInput_Hook" ) );
					Always_sprint_hk.Emplace( );

					var BulletMovementMethod = Find( "\uEA7D" ).GetMethod( "\uE00F" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic );
					BulletMovement_hk = new DumbHook( );
					BulletMovement_hk.Init( BulletMovementMethod , typeof( BulletMovement ).GetMethod( "BulletMovement_Hook" ) );
					BulletMovement_hk.Emplace( );

					var DelflectsMethod = Find( "\uEA7D" ).GetMethod( "\uE00D", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic ); 
					Deflects_hk = new DumbHook( );
					Deflects_hk.Init( DelflectsMethod , typeof( Deflects ).GetMethod( "Deflects_Hook" ) ); 
					Deflects_hk.Emplace( );

					Penetrated_hk = new DumbHook( );
					Penetrated_hk.Init( typeof( EFT.Ballistics.BallisticCollider ).GetMethod( "IsPenetrated" ) , typeof( IsPenetrated ).GetMethod( "IsPenetrated_Hook" ) );
					Penetrated_hk.Emplace( );

					var CanStandMethod = Find( "\uE61C" ).GetMethod( "CanStandAt" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public );
					CanStand_hk = new DumbHook( );
					CanStand_hk.Init( CanStandMethod , typeof( CanStand ).GetMethod( "CanStandAt_Hook" ) );
					CanStand_hk.Emplace( );

					var HasGroundMethod = Find( "\uE61C" ).GetMethod( "HasGround" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public );
					HasGround_hk = new DumbHook( );
					HasGround_hk.Init( HasGroundMethod , typeof( GroundCheck ).GetMethod( "HasGround_hook" ) );
					HasGround_hk.Emplace( );

					//ApplyShot_hk = new DumbHook( );
					//ApplyShot_hk.Init( typeof( EFT.Player ).GetMethod( "ApplyShot" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public ) , typeof( ApplyShot ).GetMethod( "ApplyShit_Hook" ) );
					//ApplyShot_hk.Emplace( );

					var GetShotInfoMethod = Find( "\uE5F7" ).GetMethod( "GetShotInfo" , System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public );
					ShotInfo_hk = new DumbHook( );
					ShotInfo_hk.Init( GetShotInfoMethod , typeof( GetShotInfo ).GetMethod( "GetShotInfo_Hook" ) );
					ShotInfo_hk.Emplace( );

				    var canStartNewSearchOperationMethod = Find( "\uE96E" ).GetMethod( "CanStartNewSearchOperation" , BindingFlags.Instance | BindingFlags.Public );
				    CanStartNewSearchOperation_hk = new DumbHook( );
					CanStartNewSearchOperation_hk.Init( canStartNewSearchOperationMethod, typeof(CanStartNewSearchOperation).GetMethod( "CanStartNewSearchOperation_hook" ) );
					CanStartNewSearchOperation_hk.Emplace(  );

				    //SetRoundIntoWeapon AddHit ToWeaponMalfunctionState //UpdateProfile //ForceLogout //ShowScreen 
					// uE672 uE001 | OnEnemyKill | \uE65E LogDamage | AddDoorExperience | \uE61F SetCharacterMovementSpeed | \uE4EC MeetsRequirements

					is_hooked = false;
				}
			
            }
				
        }

    }
}
