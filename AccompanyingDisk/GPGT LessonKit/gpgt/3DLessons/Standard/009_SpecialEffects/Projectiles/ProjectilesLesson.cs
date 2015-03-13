//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
//-----------------------------------------------------------------------------
// *************************** LOAD DATABLOCKS FOR LESSON
//-----------------------------------------------------------------------------
exec("./ProjectilesLessonDBs.cs");

//-----------------------------------------------------------------------------
// *************************** LOAD METHODS and FUNCTIONS FOR LESSON
//-----------------------------------------------------------------------------
exec("./ProjectilesLessonMethods.cs");

//-----------------------------------------------------------------------------
// *************************** DEFINE THE *STANDARD* LESSON METHODS 
//-----------------------------------------------------------------------------

function ProjectilesLesson::onAdd(%this) 
{
	DefaultLessonPrep();
}

function ProjectilesLesson::onRemove(%this) 
{
}

function ProjectilesLesson::callStandaloneProjectile( %this, %projectileDB, %startPos, %pointingVector, %velocity )
{
   standaloneProjectile( %projectileDB, %startPos, %pointingVector, %velocity );
   %this.schedule( 500, callStandaloneProjectile, %projectileDB, %startPos, %pointingVector, %velocity );  
}

function ProjectilesLesson::ExecuteLesson(%this) 
{
	//
	// Non-Ballistic Projectile
	//
	%startPos		= CalculateObjectDropPosition( North10.getPosition() , "0 0 2" );
   %this.schedule( 500, callStandaloneProjectile, "BlueEnergyProjectile", %startPos, "0 1 0", 10 );  

	//
	// Long Delay (Bouncy) Projectile
	//
	%startPos		= CalculateObjectDropPosition( South10.getPosition() , "0 0 4" );
   %this.schedule( 500, callStandaloneProjectile, "BlueEnergyProjectileLongDelay", %startPos, "0 -1 -1.2", 8 );  

	//
	// Ballistic Projectile
	//
	%startPos		= CalculateObjectDropPosition( West10.getPosition() , "0 0 2" );
   %this.schedule( 500, callStandaloneProjectile, "BlueEnergyProjectileBallistic", %startPos, "-1 0 1", 10 );  
}

