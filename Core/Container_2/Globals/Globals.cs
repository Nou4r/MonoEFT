using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
namespace eft
{
    public static class Globals
    {
        public static AssetBundle Bundle;
        public static GUISkin MenuSkin;
        public static Camera Cam;
        public static EFT.Player Local;
        public static EFT.GameWorld World;
		public static EFT.Player Target;
		public static AudioClip Hitclip;
		public static List<CheatStructs.Tracer> TracerList = new List<CheatStructs.Tracer>( );
		public static List<CheatStructs.HitMarker> HitList = new List<CheatStructs.HitMarker>( );
		public static Vector3 test;
		public static float CalcFov( global::UnityEngine.Vector3 pos )
		{
			var position = Camera.main.transform.position;
			var forward = Camera.main.transform.forward;
			var normalized = ( pos - position ).normalized;
			return Mathf.Acos( Mathf.Clamp( Vector3.Dot( forward , normalized ) , -1f , 1f ) ) * 57.29578f;
		}


		public static Vector3 BestPos(EFT.Player player, Vector3 aimpos )
        {
			var position = Local.Fireport.position;
			var vector = Local.Fireport.position + Cam.transform.forward * 2f;
			var vector2 = Local.Fireport.position + Cam.transform.up * 2f;
			var vector3 = Local.Fireport.position + Cam.transform.up * 2f;
			var vector4 = Local.Fireport.position + Cam.transform.right * 2f;
			var vector5 = Local.Fireport.position + Cam.transform.right * 2f;
			Vector3 result;
			if( is_visible( player.gameObject , position , aimpos ) )
			{
				result = position;
			}
			else
			{
			
				if( is_visible( player.gameObject , vector , aimpos ) )
				{
					result = vector;
				}
				else
				{
					if( is_visible( player.gameObject , vector4 , aimpos ) )
					{
						result = vector4;
					}
					else
					{
						if( is_visible( player.gameObject , vector5 , aimpos ) )
						{
							result = vector5;
						}
						else
						{
							if( is_visible( player.gameObject , vector2 , aimpos ) )
							{
								result = vector2;
							}
							else
							{ 
								if( is_visible( player.gameObject , vector3 , aimpos ) )
								{
									result = vector3;
								}
								else
								{
									result = position;
								}
							}
						}
					}
				}
			}
			return result;
		}
        public static bool w2s( global::UnityEngine.Vector3 pos , ref global::UnityEngine.Vector3 screen )
        {
            var vector = Cam.WorldToScreenPoint( pos );
            vector.y = Screen.height - vector.y;
            screen = vector;
            return screen.z > 0.01f;
        }
		public static bool is_visible( GameObject obj , Vector3 Pos , Vector3 Position, out RaycastHit raycastHit )
		{
			return Physics.Linecast( Pos , Position , out raycastHit , -2142957568 ) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject;
		}

		public static bool is_visible( GameObject obj , Vector3 Pos , Vector3 Position )
        {
            RaycastHit raycastHit;
            return Physics.Linecast( Pos , Position , out raycastHit , -2142957568 ) && raycastHit.collider && raycastHit.collider.gameObject.transform.root.gameObject == obj.transform.root.gameObject;
        }
    }
}
