using UnityEngine;
using System.Collections;
using FarseerPhysics.Collision.Shapes;


public class CollisionPresets  {

  enum _entityCategory {
    SCENERY =          0x0001,
    FRIENDLY_SHIP =     0x0002,
    ENEMY_SHIP =        0x0004,
    FRIENDLY_AIRCRAFT = 0x0008,
    ENEMY_AIRCRAFT =    0x0010,
  };
	
	
	
	public static void SetAsEnemyShot(Shape shape) {
		
	}
	
	public static void SetAsEnemy(Shape shape) {
		
	}
}
