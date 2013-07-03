using UnityEngine;
using System.Collections;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;


public class CollisionPresets  {

  enum _entityCategory {
    SCENERY =   	0x0001, //Category.Cat1
    PLAYER =    	0x0002, //Category.Cat2
    PLAYER_SHOT =   0x0004, //Category.Cat3
    ENEMY =		 	0x0008, //Category.Cat4
    ENEMY_SHOT =    0x0010, //Category.Cat5
  };
	
	public static void SetAsScenery(Fixture fix) {
		fix.CollisionCategories = Category.Cat1;
		fix.CollidesWith = Category.Cat2 | Category.Cat3 | Category.Cat5; //PLAYER | PLAYER_SHOT | ENEMY_SHOT
	}
	
	public static void SetAsPlayer(Fixture fix) {
		fix.CollisionCategories = Category.Cat2;	
		fix.CollidesWith = Category.Cat1 | Category.Cat4 | Category.Cat5; //SCENERY | ENEMY | ENEMY_SHOT
	}
	
	public static void SetAsPlayerShot(Fixture fix) {
		fix.CollisionCategories = Category.Cat3;	
		fix.CollidesWith = Category.Cat1 | Category.Cat4; //SCENERY | ENEMY
	}

	public static void SetAsEnemy(Fixture fix) {
		fix.CollisionCategories = Category.Cat4;
		fix.CollidesWith = Category.Cat2 | Category.Cat3; //PLAYER | PLAYER_SHOT
	}	
	
	public static void SetAsEnemyShot(Fixture fix) {
		fix.CollisionCategories = Category.Cat5;
		fix.CollidesWith = Category.Cat2 | Category.Cat1; //SCENERY | PLAYER
	}
	

}
