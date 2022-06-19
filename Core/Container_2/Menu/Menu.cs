using System;
using UnityEngine;

namespace eft
{
    public static class Menu
    {
        public static bool open = true;
        public static int tab;
        public static bool BoundPhase;
        public static Rect rect = new Rect( ( float )( Screen.width / 2 - 200 ) , ( float )( Screen.height / 2 - 200 ) , 500f , 600f );
        public static void Draw( int id )
        {
            tab = UnityEngine.GUILayout.Toolbar( tab , new GUIContent [ ]
            {
                new GUIContent("Combat"),
                new GUIContent("Visuals"),
                new GUIContent("Misc"),
                new GUIContent("Removals"),
                new GUIContent("Exploits")
            } , Array.Empty<GUILayoutOption>( ) ) ;

            switch(tab)
            {
                case 0:
                    CombatTab( );
                    break;
                case 1:
                    VisualTab( );
                    break;
                case 2: 
                    MiscTab( );
                    break;
                case 3:
                    RemovalsTab( );
                    break;
                case 4:
                    ExploitsTab( );
                    break;
            }

            GUI.DragWindow( );
        }

        private static void CombatTab()
        {
            GUILayout.Label( "Aimbot" );
            GUILayout.Space( 2f );

            Settings.Aimbot = GUILayout.Toggle( Settings.Aimbot , "Enabled" , Array.Empty<GUILayoutOption>( ) );
            Settings.MemoryAim = GUILayout.Toggle( Settings.MemoryAim , "Memory Aim" , Array.Empty<GUILayoutOption>( ) );
            Settings.SilentAim = GUILayout.Toggle( Settings.SilentAim , "Silent Aim" , Array.Empty<GUILayoutOption>( ) );
        }

        private static void VisualTab()
        {
            GUILayout.Label( "Players" );
            GUILayout.Space( 2f );

            Settings.Visuals = GUILayout.Toggle( Settings.Visuals , "Enabled" , Array.Empty<GUILayoutOption>( ) );
            Settings.Name = GUILayout.Toggle( Settings.Name , "Name" , Array.Empty<GUILayoutOption>( ) );
            Settings.Box = GUILayout.Toggle( Settings.Box , "Box" , Array.Empty<GUILayoutOption>( ) );
            Settings.ShowHealth = GUILayout.Toggle( Settings.ShowHealth , "Healthbar" , Array.Empty<GUILayoutOption>( ) );
            Settings.Skeleton = GUILayout.Toggle( Settings.Skeleton , "Skeleton" , Array.Empty<GUILayoutOption>( ) );
            Settings.Chams1 = GUILayout.Toggle( Settings.Chams1 , "Chams ( 1 )" , Array.Empty<GUILayoutOption>( ) );
            Settings.Chams2 = GUILayout.Toggle( Settings.Chams2 , "Chams ( 2 )" , Array.Empty<GUILayoutOption>( ) );
            GUILayout.Label( "LocalPlayer" , Array.Empty<GUILayoutOption>( ) );
            GUILayout.Space( 2f );
            Settings.LocalPlayerChams = GUILayout.Toggle( Settings.LocalPlayerChams , "Apply Chams to Localplayer" , Array.Empty<GUILayoutOption>( ) );
            Settings.WeaponChams = GUILayout.Toggle( Settings.WeaponChams , "Weapon Chams" , Array.Empty<GUILayoutOption>( ) );
            Settings.BulletTracers = GUILayout.Toggle( Settings.BulletTracers , "Bullet Tracers" , Array.Empty<GUILayoutOption>( ) );
            Settings.HitMarker = GUILayout.Toggle( Settings.HitMarker , "Hitmarkers" , Array.Empty<GUILayoutOption>( ) );
            Settings.ThirdPerson = GUILayout.Toggle( Settings.ThirdPerson , "Third Person" , Array.Empty<GUILayoutOption>( ) );
            Settings.Fov = GUILayout.Toggle( Settings.Fov , "More Fov" , Array.Empty<GUILayoutOption>( ) );
            Settings.Fanta = GUILayout.Toggle( Settings.Fanta , "Fanta Moment" , Array.Empty<GUILayoutOption>( ) );
            Settings.RicochetPrediction = GUILayout.Toggle( Settings.RicochetPrediction , "Ricochet Indicator" , Array.Empty<GUILayoutOption>( ) );
            GUILayout.BeginArea( new Rect( 250 , 80f , 150f , 200f ) );
            GUILayout.Label( "World" , Array.Empty<GUILayoutOption>( ) );
            Settings.SkyMod = GUILayout.Toggle( Settings.SkyMod , "Sky modification" , Array.Empty<GUILayoutOption>( ) );
            GUILayout.EndArea( );
        }

        private static void MiscTab()
        {
            GUILayout.Label( "Misc" );
            GUILayout.Space( 2f );

            Settings.Hitsounds = GUILayout.Toggle( Settings.Hitsounds , "Hitsounds" , Array.Empty<GUILayoutOption>( ) );
            Settings.AlwaysSprint = GUILayout.Toggle( Settings.AlwaysSprint, "Always Sprint" , Array.Empty<GUILayoutOption>( ) );

        }

        private static void RemovalsTab()
        {
            GUILayout.Label( "Removals" );
            GUILayout.Space( 2f );

            Settings.AutoGun = GUILayout.Toggle( Settings.AutoGun , "Automatic Gun" , Array.Empty<GUILayoutOption>( ) );
            Settings.Stamina = GUILayout.Toggle( Settings.Stamina , "Stamina" , Array.Empty<GUILayoutOption>( ) );
            Settings.RemoveRecoil = GUILayout.Toggle( Settings.RemoveRecoil , "Remove Recoil" , Array.Empty<GUILayoutOption>( ) );
            Settings.RemoveSpread = GUILayout.Toggle( Settings.RemoveSpread , "Remove Spread" , Array.Empty<GUILayoutOption>( ) );
            GUILayout.Label( "Visual" );
            GUILayout.Space( 2f );
            Settings.NoVisor = GUILayout.Toggle( Settings.NoVisor , "No Visor" , Array.Empty<GUILayoutOption>( ) );
            Settings.FullBright = GUILayout.Toggle( Settings.FullBright , "Fullbright" , Array.Empty<GUILayoutOption>( ) );
        }

        private static void ExploitsTab()
        {
            GUILayout.Label( "Weapon" );
            Settings.Ricochet = GUILayout.Toggle( Settings.Ricochet , "Ricochet" , Array.Empty<GUILayoutOption>( ) );
            Settings.ShootThroughWall = GUILayout.Toggle( Settings.ShootThroughWall , "Manipulation" , Array.Empty<GUILayoutOption>( ) );
            Settings.RapidFire = GUILayout.Toggle( Settings.RapidFire , "Rapid Fire" , Array.Empty<GUILayoutOption>( ) );
            Settings.RiochetTest = GUILayout.Toggle( Settings.RiochetTest , "Ricochet 5 walls" , Array.Empty<GUILayoutOption>( ) );


            GUILayout.Label( "Movement" );
            Settings.ShotRunCancel = GUILayout.Toggle( Settings.ShotRunCancel , "Shoot while Running" , Array.Empty<GUILayoutOption>( ) );
            Settings.Phase = GUILayout.Toggle( Settings.Phase , "Phase" , Array.Empty<GUILayoutOption>( ) );
            Settings.HighJump = GUILayout.Toggle( Settings.HighJump , "Jump High" , Array.Empty<GUILayoutOption>( ) );

            Settings.Flyhack = GUILayout.Toggle( Settings.Flyhack , "Flyhack" , Array.Empty<GUILayoutOption>( ) );
            Settings.Speedhack = GUILayout.Toggle( Settings.Speedhack , "Speedhack" , Array.Empty<GUILayoutOption>( ) );
            Settings.TestSpeedhack = GUILayout.Toggle( Settings.TestSpeedhack , "Speedhack (Test)" , Array.Empty<GUILayoutOption>( ) );

            GUILayout.Label( "Other" );
            Settings.MultiSearch = GUILayout.Toggle( Settings.MultiSearch, "Multi Search", Array.Empty < GUILayoutOption >( ) );
            Settings.LootThroughWalls = GUILayout.Toggle( Settings.LootThroughWalls, "Loot Through Walls", Array.Empty < GUILayoutOption >( ) );
            Settings.InstanceExamine = GUILayout.Toggle( Settings.InstanceExamine, "Instant Examine", Array.Empty < GUILayoutOption >( ) );
        }
    }
}
