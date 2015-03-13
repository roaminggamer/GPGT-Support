//------------------------------------------------------
// Copyright 2000-2005, GarageGames.com, Inc.
// Written, modfied, or otherwise interpreted by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\n\c3--------- Loading FireBall Block Datablock Definitions and Scripts ---------");
function rldfire() {
exec("./fireballs.cs");
}

//-------------------------------------------------------------------------
//             FireBall Datablock Definitions
//-------------------------------------------------------------------------
datablock AudioDescription( MazeRunnerNonLooping3DADDB )
{
   volume            = 1.0;
   isLooping         = false;
   is3D              = true;
   ReferenceDistance = 10.0;
   MaxDistance       = 30.0;
   type              = $SimAudioType; 
};

datablock AudioProfile(MazeRunnerFireballExplosionSound)
{
   filename    = "~/data/GPGTBase/sound/GenericExplosionSound.ogg";
   description = MazeRunnerNonLooping3DADDB;
};

datablock AudioProfile(MazeRunnerFireballFiringSound)
{
   filename    = "~/data/GPGTBase/sound/GenericFiringSound.ogg";
   description = MazeRunnerNonLooping3DADDB;
};


//-------------------------------------------------------------------------
//             FireBallParticle Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleData(FireBallParticle : baseSmokePD0 )
{
   dragCoeffiecient     = 0.0;
   gravityCoefficient   = 0.0;
   inheritedVelFactor   = 0.0;
   lifetimeMS           = 350;
   lifetimeVarianceMS   = 50;
   spinRandomMin = -30.0;
   spinRandomMax = 30.0;
   colors[0]     = "1 0.7 0.7 1.0";
   colors[1]     = "1 0.7 0.7 1.0";
   colors[2]     = "1 0.7 0.7 0";
   sizes[0]      = 0.5;
   sizes[1]      = 0.7;
   sizes[2]      = 1.0;
   times[0]      = 0.0;
   times[1]      = 0.3;
   times[2]      = 1.0;
};

//-------------------------------------------------------------------------
//             FireBallEmitter Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleEmitterData(FireBallEmitter)
{
   ejectionPeriodMS = 20;
   periodVarianceMS = 5;
   ejectionVelocity = 0.25;
   velocityVariance = 0.10;
   thetaMin         = 0.0;
   thetaMax         = 180.0;  
   particles = FireBallParticle;
};


//-------------------------------------------------------------------------
//             FireBallExplosionParticle Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleData(FireBallExplosionParticle : baseSmokePD0 )
{
   lifetimeMS           = 750;
   lifetimeVarianceMS   = 200;

   colors[0]     = "1 0.2 0.2 1.0";
   colors[1]     = "1.0 0.6 0.2 0.0";
   sizes[0]      = 1.5;
   sizes[1]      = 3.5;

};

//-------------------------------------------------------------------------
//             FireBallExplosionEmitter Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleEmitterData(FireBallExplosionEmitter)
{
   ejectionPeriodMS = 7;
   periodVarianceMS = 0;
   ejectionVelocity = 1;
   velocityVariance = 1;
   ejectionOffset   = 0;
   thetaMin         = 0;
   thetaMax         = 60;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvances = false;
   particles = "FireBallExplosionParticle";
};


//-------------------------------------------------------------------------
//             FireBallExplosion Datablock Definition
//-------------------------------------------------------------------------
datablock ExplosionData(FireBallExplosion)
{
   lifeTimeMS = 2000;
   particleEmitter = FireBallExplosionEmitter;
   particleDensity = 50;
   particleRadius = 0.2;
   faceViewer     = true;
   
   soundProfile = "MazeRunnerFireballExplosionSound";

   // Dynamic light
   lightStartRadius = 0;
   lightEndRadius   = 6;
   lightStartColor  = "1 0.2 1";
   lightEndColor    = "1 0.6 0.2";
};

//-------------------------------------------------------------------------
//             FireBallProjectile Datablock Definition
//-------------------------------------------------------------------------
datablock ProjectileData(FireBallProjectile)
{
   projectileShapeName = "~/data/MazeRunner/Shapes/Projectiles/projectile.dts";

   explosion			      = FireBallExplosion;

   particleEmitter		= FireBallEmitter;

   armingDelay			= 0;

   lifetime				= 5000;
   fadeDelay			= 4800;

   isBallistic			= false;
};

//-------------------------------------------------------------------------
//             FireBallBlock Datablock Definition
//-------------------------------------------------------------------------
datablock StaticShapeData( FireBallBlock )
{
   category				= "FireBallBlocks";
   shapeFile			= "~/data/MazeRunner/Shapes/MazeBlock/blockA.dts";
   emap					= false;

   isInvincible		= true;
};

function FireBallBlock::onAdd(%this, %obj)
{
   %obj.setSkinName( fire );
}

// ******************************************************************
//					firePass() 
// ******************************************************************
//
// 1. Iterate over the set and call doFire() for each entry.
// 2. Schedule another pass in 1500 ms
//
function SimSet::firePass( %theSet ) 
{
   // 1
   %theSet.forEach( doFire , true );

   // 2
   %theSet.schedule( 1500  , firePass );
}

// ******************************************************************
//					doFire() 
// ******************************************************************
//
// 1. If the block still has a valid bullet (fireball) wait for next pass.
// 2. Check to see if random direction was requested.
// 3. Fire in the direction we decided upon (either requested or random).
//
function StaticShape::doFire( %marker ) {
    //echo("\c3Firing.... %type == ", %marker.type);

   // 1
   if( isObject( %marker.bullet ) ) return;

   // 2
   %firePath = ( %marker.type == 9 ) ? getRandom( 0 , 9 ) :  %marker.type ;

    // 3
    switch( %firePath ) 
    {
    //
    // NORTH
    //
    case 0: 
        %marker.shootFireBall( FireBallProjectile , "0 1 0" , 20 );		
    //
    // NORTH EAST
    //
    case 1: 
        %marker.shootFireBall( FireBallProjectile , "1 1 0" , 20 );		
    //
    // EAST
    //
    case 2: 
        %marker.shootFireBall( FireBallProjectile , "1 0 0" , 20 );		
    //
    // SOUTH EAST
    //
    case 3: 
        %marker.shootFireBall( FireBallProjectile , "1 -1 0" , 20 );		
    //
    // SOUTH
    //
    case 4: 
        %marker.shootFireBall( FireBallProjectile , "0 -1 0" , 20 );		
    //
    // SOUTH WEST
    //
    case 5: 
        %marker.shootFireBall( FireBallProjectile , "-1 -1 0" , 20 );		
    //
    // WEST
    //
    case 6: 
        %marker.shootFireBall( FireBallProjectile , "-1 0 0" , 20 );		
    //
    // NORTH WEST
    //
    case 7: 
        %marker.shootFireBall( FireBallProjectile , "-1 1 0" , 20 );		
    //
    // DOWN
    //
    case 8: 
        %marker.shootFireBall( FireBallProjectile , "0 0 -1" , 20 );		
    }
}

// ******************************************************************
//					shootFireBall() 
// ******************************************************************
//
// 1. Create a projectile and fire in the direction and with velocity requested.
//
function StaticShape::shootFireBall( %marker, %projectile , %pointingVector , %velocity) 
{
   // 1
    %bullet = new Projectile() {
        dataBlock        = %projectile;
        initialVelocity  = vectorScale( vectorNormalize(%pointingVector) , %velocity );
        initialPosition  = %marker.getWorldBoxCenter();
        sourceObject     = -1;
        sourceSlot       = -1;
        theMarker        = %marker;
    };

    %marker.bullet = %bullet;
    MissionCleanup.add(%bullet);
    %marker.playAudio( 0 , MazeRunnerFireballFiringSound );
}



// ******************************************************************
//					FireBallProjectile::onCollision() 
// ******************************************************************
function FireBallProjectile::onCollision( %projectileDB , %projectileObj , %collidedObj, %fade, %vec, %speed) 
{
   if (!$Game::Running) return;
//   echo("onCollision( ", %projectileDB ," , ",%projectileObj," , ",%collidedObj," , ",%fade," , ",%vec," , ",%speed," )");
   
//   echo(%collidedObj.getClassName());
   
   if (%collidedObj.getClassName() $= "Player") 
   {
      %collidedObj.loseALife();
   }

   //%Offset = vectorSub( %vec , $Game::Player.getWorldBoxCenter() );
   //%Len = vectorLen( %offset );

   //if( %len < 1.7 )
   //{
   //   //echo("Hit Player! ",%len);
   //   //echo("%Player center ",$Game::Player.getWorldBoxCenter());
   //   //echo("    Hit offset ",%Offset);
   //    $Game::Player.loseALife();
   //}

}
