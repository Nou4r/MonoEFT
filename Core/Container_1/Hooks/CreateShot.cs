using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using EFT.InventoryLogic;
using UnityEngine;

namespace eft
{
    public class CreateShot
    {
        public void CreateShot_hook( global::EFT.InventoryLogic.Item ammo , int fragmentIndex , int seed , UnityEngine.Vector3 origin , UnityEngine.Vector3 direction , float initialSpeed , float speed , float bulletMassKg , float bulletDiameterM , float damage , float penetrationPower , float penetrationChance , float ricochetChance , float fragmentationChance , float deviationChance , int minFragmentsCount , int maxFragmentsCount , global::EFT.Ballistics.BallisticCollider defaultBallisticCollider , object randoms , float ballisticCoefficient , global::EFT.Player player , global::EFT.InventoryLogic.Item weapon , int fireIndex , object parent )
        {
			
			if( Globals.Target != null )
			{
				if( Settings.Ricochet && parent != null )
				{				
					var Target = Globals.Target;
					var pos = Globals.BestPos( Target , Target.PlayerBones.Head.position );
					var _weapon = Globals.Local.HandsController.Item as Weapon;

					if( Target && _weapon != null ) 
					{
						direction = ( Target.PlayerBones.Head.position - origin ).normalized;
					}
				}
			}


			Hooks.CreateShot_hk.remove( );
			object [ ] parameters = new object [ ]
				{
					ammo,
					fragmentIndex,
					seed,
					origin,
					direction,
					initialSpeed,
					speed,
					bulletMassKg,
					bulletDiameterM,
					damage,
					penetrationPower,
					penetrationChance,
					ricochetChance,
					fragmentationChance,
					deviationChance,
					minFragmentsCount,
					maxFragmentsCount,
					defaultBallisticCollider,
					randoms,
					ballisticCoefficient,
					player,
					weapon,
					fireIndex,
					parent
				};
			Hooks.CreateShot_hk.OriginalMethod.Invoke( this , parameters );
			Hooks.CreateShot_hk.Emplace( );
		}
    }
}
