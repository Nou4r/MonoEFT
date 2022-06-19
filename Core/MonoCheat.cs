using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;
using Comfort.Common;
using EFT.InventoryLogic;
using EFT.UI.Ragfair;

namespace eft
{
    public class MonoCheat : MonoBehaviour
    {
        private static bool Console_hooked = false;
        private static bool FullbrightCalled = false;
        private void Start()
        {
            Globals.Bundle = AssetBundle.LoadFromMemory( System.IO.File.ReadAllBytes( "C:\\Asset\\assets.assets" ) );
            Globals.Hitclip = Globals.Bundle.LoadAsset<AudioClip>( "bonk.mp3" );
            Rendering.font = Globals.Bundle.LoadAsset < Font >( "RUBIK-REGULAR.ttf" );
            Rendering.material = new Material( Shader.Find( "Hidden/Internal-Colored" ) )
            {
                hideFlags = HideFlags.HideAndDontSave
            };
 
            Rendering.material.SetInt( "_SrcBlend" , 5 );
            Rendering.material.SetInt( "_DstBlend" , 0xA );
            Rendering.material.SetInt( "_Cull" , 0 );
            Rendering.material.SetInt( "_ZWrite" , 0 );
        }

        private void Update()
        {
            if( Input.GetKeyDown( KeyCode.Insert ) )
                Menu.open = !Menu.open;

 
            Globals.Cam = Camera.main;

            if( Singleton<EFT.GameWorld>.Instantiated && !Globals.World )
                Globals.World = Singleton<EFT.GameWorld>.Instance;
            
            if(!Globals.Local)
            {
                foreach(var player in Globals.World.RegisteredPlayers)
                {
                    if( player.IsYourPlayer )
                        Globals.Local = player;
                }
            }    

            if(Settings.Fanta)
            {
                Globals.Cam.GetComponent<ThermalVision>( ).On = true;
            } 
            else
                Globals.Cam.GetComponent<ThermalVision>( ).On = false;


            if( Settings.RemoveRecoil ) 
            {
                Globals.Local.ProceduralWeaponAnimation.Breath.Intensity = 0f;
                Globals.Local.ProceduralWeaponAnimation.Mask = EFT.Animations.EProceduralAnimationMask.Breathing;
            }

            if(Settings.NoVisor)
                Globals.Cam.GetComponent<VisorEffect>( ).Intensity = 0f;

            if(Settings.Stamina)
            {
                var Params = Globals.Local.Physical.StaminaParameters;
                if( Params == null )
                    return;

                Params.AimDrainRate = 0f;
                Params.SprintDrainRate = 0f;
                Params.JumpConsumption = 0f;
                Params.ProneConsumption = 0f;
                Params.AimConsumptionByPose = Vector3.zero;
                Params.OverweightConsumptionByPose = Vector3.zero;
                Params.CrouchConsumption = Vector2.zero;
                Params.StandupConsumption = Vector2.zero;
                Params.WalkConsumption = Vector2.zero;
                Params.OxygenRestoration = 100000f;
                Params.ExhaustedMeleeSpeed = 100000f;
                Params.BaseRestorationRate = Params.Capacity;
            }

            if(Settings.LootThroughWalls)
            {
                var Instance = EFTHardSettings.Instance;
                Instance.LOOT_RAYCAST_DISTANCE = 4f;
            }

            if(Settings.FullBright)
            {
                if( !FullbrightCalled ) 
                {
                    var FullBrightObject = new GameObject("");
                    var Light = FullBrightObject.AddComponent<Light>( );
                    Light.color = Color.white;
                    Light.intensity = 0.5f;

                    FullbrightCalled = true;
                }
            }

            if(Settings.SkyMod)
            {
                /*
                var Instance = TOD_Sky.Instance;
                if( Instance == null )
                    return;

                var gradient = new Gradient( );
                var gradientColorKey = new GradientColorKey [ 2 ];
                gradientColorKey [ 0 ].color = Color.cyan;
                gradientColorKey [ 0 ].time = 0.0f;
                gradientColorKey [ 1 ].color = Color.blue;
                gradientColorKey [ 1 ].time = 1.0f;
                var alphakey = new GradientAlphaKey [ 2 ];
                alphakey [ 0 ].alpha = 1.0f;
                alphakey [ 0 ].time = 0.0f;
                alphakey [ 1 ].alpha = 0.0f;
                alphakey [ 1 ].time = 1.0f;

                gradient.SetKeys( colorKeys: gradientColorKey , alphaKeys: alphakey );

                Instance.Day.SkyColor = gradient;
                Instance.Night.SkyColor = gradient;
                */
            }

            if( Settings.FreeCam )
            {
                if( Input.GetKey( KeyCode.C ) )
                    Globals.Local.PointOfView = EPointOfView.FreeCamera;
                else
                    Globals.Local.PointOfView = EPointOfView.FirstPerson;
            }
      

            if(Settings.RapidFire)
            {
                if(Globals.Local.HandsController.Item is Weapon weapon)
                {
                    weapon.GetItemComponent<FireModeComponent>( ).FireMode = Weapon.EFireMode.fullauto;
                    weapon.Template.bFirerate = 2;
                    weapon.Template.isBoltCatch = false;
                    weapon.Template.BoltAction = false;          
                }
            }

            if( Settings.HighJump ) 
            {
                Globals.Local.Skills.StrengthBuffJumpHeightInc.Value = 0.7f;
            }

            if(Settings.Speedhack)
            {
                if( Input.GetKey( KeyCode.LeftShift ) )
                    Time.timeScale = 1.5f;
                else
                    Time.timeScale = 1f;              
            }

            if( Settings.AlwaysSprint ) 
            {
                var MovementContext = Globals.Local.MovementContext;
                MovementContext.AddStateSpeedLimit( 0 , Player.ESpeedLimit.BarbedWire );
                MovementContext.AddStateSpeedLimit( 0 , Player.ESpeedLimit.Aiming );
                MovementContext.EnableSprint( true );
            }

            if(Settings.TestSpeedhack)
            {
                var MovementContext = Globals.Local.MovementContext;
                if(Input.GetKey(KeyCode.LeftShift))
                {
                    Globals.Local.Transform.position += 4f * Time.deltaTime * Globals.Cam.transform.forward;
                }
            }

            if( Settings.Phase ) 
            {
                bool noclip_toggle = false;
                LayerMask mask = ~0;
                typeof( Player ).Assembly.GetType( "\uE61C" ).GetField( "\uE01A" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.SetField ).SetValue( Globals.Local.MovementContext , mask );
                typeof( Player ).Assembly.GetType( "\uE61C" ).GetField( "\uE01B" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.SetField ).SetValue( Globals.Local.MovementContext , mask );
                typeof( Player ).Assembly.GetType( "\uE61C" ).GetField( "\uE01C" , System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.SetField ).SetValue( Globals.Local.MovementContext , mask );
                float direction = 0f;

                if(Input.GetKey(KeyCode.F))
                {
                    if(Input.GetKey(KeyCode.Space))
                    {
                        direction = -1f;
                        Globals.Local.Transform.position += Globals.Local.Transform.up / 3 * 0.050f;
                    }

                    if(Input.GetKey(KeyCode.LeftControl))
                    {
                        direction = 1f;
                        Globals.Local.Transform.position -= Globals.Local.Transform.up / 3 * 0.050f;
                    }

                    Globals.Local.MovementContext.GrounderSetActive( false );
                    Globals.Local.MovementContext.IsGrounded = false;
                }

                Globals.Local.MovementContext.FreefallTime = direction;
                Globals.Local.MovementContext.InertiaSettings.MoveTime = 0;
                Globals.Local.MovementContext.InertiaSettings.MinDirectionBlendTime = 0;
                Globals.Local.MovementContext.InertiaSettings.PenaltyPower = 0;
                Globals.Local.MovementContext.InertiaSettings.DurationPower = 0;
                Globals.Local.MovementContext.InertiaSettings.BaseJumpPenaltyDuration = 0;
                Globals.Local.MovementContext.InertiaSettings.FallThreshold = 99999;

                noclip_toggle = true;
            }

            if( Settings.InstanceExamine) {
            }

            float fov = 120f; //hardcoded fov p
            if( Settings.Aimbot )
            {
                if( Settings.SilentAim )
                {
                    foreach( var Player in Globals.World.RegisteredPlayers ) {
                        if( Player == Globals.Local || !Player.ActiveHealthController.IsAlive ) 
                            continue;

                        float bestFov = Globals.CalcFov( Player.PlayerBones.Head.position );
                        if( bestFov < fov )
                        {
                            Globals.Target = Player;
                            fov = bestFov;
                        }
                    }
                }
            }

            if( Settings.Ricochet )
            {
                for( int i = 0 ; i < Globals.World._sharedBallisticsCalculator.Shots.Count ; i++ )
                {
                    var shot = Globals.World._sharedBallisticsCalculator.Shots [ i ];
                    if( shot.TimeSinceShot > 3f )
                        shot.IsForwardHit = true;
                }
            }

            Hooks.run( );
        }

        private void OnGUI()
        {
         
            Visuals.Run( );


            if( Menu.open )
            {
                Cursor.lockState = CursorLockMode.None;
                GUI.skin.label.normal.textColor = new Color32( 0 , 0 , 0 , byte.MaxValue );
                Menu.rect = GUI.Window( 0 , Menu.rect , new GUI.WindowFunction( Menu.Draw ) , "" );
            }

            Rendering.Label( Screen.width / 2 - 100 , 100 , 200 , 50 , "Halalcheats ( Insert )" , Color.cyan , true , true );

            if( Globals.Local ) {
                Rendering.Label( Screen.width / 2 , Screen.height / 2 , 200 , 50 , "x" , Color.red , true );

                if( Globals.Local.HandsController.Item is EFT.InventoryLogic.Weapon weapon )
                {
                    var mag = weapon.GetCurrentMagazine( );
                    if( mag == null )
                        return;
                    Rendering.Label( Screen.width / 2 - 10 , 700 , 200 , 50 , $"{mag.Count} | {weapon.SelectedFireMode}" , Color.white , true );
                }

                if( Settings.Ricochet )
                {                    
                    Rendering.Label( 40 , 500 , 200 , 50 , "Ricochet Aimbot Enabled" , Color.red , true );                  
                }
                if( Settings.TestSpeedhack )
                {                
                    Rendering.Label( 40 , 510 , 200 , 50 , "Speedhack ( Left Shift )" , Color.red , true );            
                }

                if(Settings.SilentAim)
                {
                    var HeadPos = CheatCam.w2s( Globals.Target.PlayerBones.Head.position );
                    Rendering.Line( new Vector2( Screen.width / 2 , Screen.height / 2 + 2 ) , new Vector2( HeadPos.x , HeadPos.y ) , 1 , new Color32( 255 , 255 , 255 , 30 ) );                  
                }

                if( Settings.Phase )
                {
                        if( Input.GetKey( KeyCode.F ) )
                            Rendering.Label( Screen.width / 2 - 10 , Screen.height / 2 + 50 , 200 , 50 , "[ Noclipping ] " , Color.red , true );               
                }
            }

            if( Settings.BulletTracers )
            {
                for( int i = 0 ; i < Globals.TracerList.Count ; i++ )
                {
                    var tracer = Globals.TracerList [ i ];

                    if( Mathf.Abs( tracer.Time - Time.time ) > 2f )
                        Globals.TracerList.RemoveAt( i );

                    Rendering.MatrixLine( tracer.StartPos , tracer.EndPos , new Color32( 255 , 255 , 255 , 30 ) );
                }
            }         
    
        }
    }
}
