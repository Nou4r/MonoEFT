using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EFT;

namespace eft
{
    public static class Visuals
    {
        public static void Run()
        {
            if( !Globals.World || !Globals.Local )
                return;

            foreach(var Player in Globals.World.RegisteredPlayers)
            {
                if( Player != Globals.Local ) 
                {
                    if(Settings.Visuals)
                    {
                        if( !Player.HealthController.IsAlive )
                            continue;

                        Vector3 HeadPos = CheatCam.w2s( Player.PlayerBones.Head.position );

                        var position = CheatCam.w2s( Player.Transform.position );

                        var distance = Mathf.Round( Vector3.Distance( Globals.Cam.transform.position , position ) );

            
                        var shoulder = CheatCam.w2s( Player.PlayerBones.LeftShoulder.position );
                        var height_offset = Mathf.Abs( HeadPos.y - shoulder.y );
                        var BoxHeight = Mathf.Abs( HeadPos.y - position.y ) + height_offset * 3;
                        var BoxWitdh = BoxHeight * 0.4f;


                        var BoxPosX = position.x - BoxWitdh / 2f;
                        var BoxPosY = HeadPos.y - height_offset * 2f;

                        if(Settings.Box)
                        {
                            Rendering.Rect( BoxPosX , BoxPosY , BoxWitdh , BoxHeight , Color.cyan );
                        }

                        float health = 0f;
                        float healthmax = 0f;
                        for( int i = 0 ; i < 6 ; i++ ) {
                            var bodyPartHealth = Player.HealthController.GetBodyPartHealth( ( EBodyPart ) i, false );
                            health += bodyPartHealth.Current;
                            healthmax += bodyPartHealth.Maximum;
                        }

                        if( Settings.ShowHealth ) {
                            var healthbox = health / healthmax * ( BoxHeight - 2f );
                            Rendering.RectFilled( BoxPosX - 7f, BoxPosY, 4f, BoxHeight , new Color32( 0 , 0 , 0 , 180 ) );
                            Rendering.RectFilled( BoxPosX - 6f, BoxPosY + BoxHeight - healthbox - 1f, 2f, healthbox, Color.green );
                        }


                        if( Settings.Name )
                        {
                            var playerName = Player.Profile.Nickname;
                            var Distance = $"{distance}";
                            Rendering.Label( BoxPosX , BoxPosY - 30 , 200 , 50 , Distance , new Color32( 108 , 168 , 189 , 120 ) , true );
                            Rendering.Label( BoxPosX , BoxPosY - 20, 200 , 50 , playerName , Color.white , true );
                        }

                        if( Settings.Chams1 ) 
                        {
                            if(Player != Globals.Local)
                            {
                                var skins = Player.PlayerBody.BodySkins.Values;
                                foreach( var skin in skins )
                                {
                                    foreach( var renderer in skin.GetRenderers( ) )
                                    {
                                        var material = renderer.material;
                                        
                                        material.shader = Globals.Bundle.LoadAsset<Shader>( "chams.shader" );
                                        material.SetColor( "_ColorVisible" , new Color32( 255 , 255 , 255 , 255 ) );
                                        material.SetColor( "_ColorBehind" , new Color32( 0 , 255 , 229 , 255 ) );
                                    }
                                }
                            }

                        }
                    }
                }

                if( Settings.WeaponChams )
                {
                    var component = Globals.Local.GetComponentsInChildren<Renderer>( );

                    foreach( var render in component )
                    {
                        var material = render.material;
                        if( !material )
                            continue;
                        
                        var name = material.name;

                       // EFT.UI.ConsoleScreen.Log( $"{name}" );

                        if( name.Contains( "weapon") || name.Contains( "ammo" ) || name.Contains( "sight" ) || name.Contains( "grip" ) || name.Contains( "mount" )|| name.Contains( "item" ) || name.Contains( "mag" ) || name.Contains( "tactical" ) || name.Contains( "scope" ) || name.Contains( "barrel" ) || name.Contains( "patron" ) || name.Contains( "muzzle" )) {
                            material.shader = Globals.Bundle.LoadAsset<Shader>( "chams.shader" );
                            material.SetColor( "_ColorVisible" , new Color32( 0 , 255 , 229 , 255 ) );
                            material.SetColor( "_ColorBehind" , new Color32( 0 , 255 , 229 , 255 ) );
                        }

                    }
                }

                if(Settings.HitMarker)
                {
                    for( int i = 0 ; i < Globals.HitList.Count ; i++ ) 
                    {
                        var hitMarker = Globals.HitList [ i ];
                      
                        if( Mathf.Abs( hitMarker.Time - Time.time ) > 2f )
                            Globals.TracerList.RemoveAt( i );

                        var HitPos = CheatCam.w2s( hitMarker.HitPos );

                        Rendering.Label( HitPos.x , HitPos.y , 200 , 50 , "x" , Color.red , true , true );
                    }
                }

                if( Settings.RicochetPrediction )
                {
                    var ShotDirection = Globals.Local.ProceduralWeaponAnimation._shotDirection;
                    Globals.Local.ProceduralWeaponAnimation._shotDirection = Vector3.down;
                    var WeaponDir = Globals.Local.GetComponent<Player.FirearmController>( ).WeaponDirection;
                    Globals.Local.ProceduralWeaponAnimation._shotDirection = ShotDirection;

                    RaycastHit hit;
                    if( Physics.Raycast( Globals.Local.PlayerBones.Fireport.Original.position , WeaponDir , out hit , 9999f , 1082202128 ) )
                    {
                        var Fireport = CheatCam.w2s( Globals.Local.PlayerBones.Fireport.Original.position );
                        var HitPoint = CheatCam.w2s( hit.point );

                        if( HitPoint.z > 0.01f )
                            Rendering.Line( new Vector2( HitPoint.x , HitPoint.y ) , new Vector2( Fireport.x , Fireport.y ) , 1 , Color.red );
                    }
                }
            }
        }
    }
}
