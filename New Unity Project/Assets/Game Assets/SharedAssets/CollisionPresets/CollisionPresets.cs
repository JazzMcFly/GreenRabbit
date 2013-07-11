using UnityEngine;
using System.Collections;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;


public class CollisionPresets  {

  public enum EntityCategory {
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
	
	public static void SetFixtureCollision(Fixture fix, EntityCategory cat) {
		switch (cat) {
		case EntityCategory.SCENERY:
			SetAsScenery(fix);
			break;
		case EntityCategory.PLAYER:
			SetAsPlayer(fix);
			break;
		case EntityCategory.PLAYER_SHOT:
			SetAsPlayerShot(fix);
			break;
		case EntityCategory.ENEMY:
			SetAsEnemy(fix);
			break;
		case EntityCategory.ENEMY_SHOT:
			SetAsEnemyShot(fix);
			break;
		default :
			break;
		}
	}
}
