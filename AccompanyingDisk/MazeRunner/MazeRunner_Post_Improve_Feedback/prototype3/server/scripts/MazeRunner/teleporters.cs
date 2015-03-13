//------------------------------------------------------
// Copyright 2000-2005, GarageGames.com, Inc.
// Written, modfied, or otherwise interpreted by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\n\c3--------- Loading Teleporter Datablock Definitions and Scripts ---------");

function rldtp() {
   exec("./teleporters.cs");
}

//-------------------------------------------------------------------------
//             TeleportStation_PD0 Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleData(TeleportStation_PD0)
{
   dragCoefficient      = 0.0;
   gravityCoefficient   = -0.50;
   inheritedVelFactor   = 0.0;
   constantAcceleration = 0.0;
   lifetimeMS           = 400;
   lifetimeVarianceMS   = 100;
   useInvAlpha          = false;
   textureName          = "~/data/GPGTBase/particletextures/smoke";
   colors[0]     = "0.7 0.1 0.1 0.8";
   colors[1]     = "0.7 0.1 0.1 0.4";
   colors[2]     = "0.7 0.1 0.1 0.0";
   sizes[0]      = 0.1;
   sizes[1]      = 0.3;
   sizes[2]      = 0.3;
   times[0]      = 0.0;
   times[1]      = 0.5;
   times[2]      = 1.0;
};

//-------------------------------------------------------------------------
//             TeleportStation_PED0 Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleEmitterData(TeleportStation_PED0)
{
   ejectionPeriodMS = 1;
   periodVarianceMS = 0;
   ejectionVelocity = 2.0;
   ejectionOffset   = 0.5;
   velocityVariance = 0.5;
   thetaMin         = 0;
   thetaMax         = 80;
   phiReferenceVel  = 0;
   phiVariance      = 360;
   overrideAdvance  = false;
   particles        = "TeleportStation_PD0";
};

//-------------------------------------------------------------------------
//             TeleportStation_PD1 Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleData(TeleportStation_PD1 : TeleportStation_PD0)
{
   colors[0]     = "0.1 0.7 0.1 0.8";
   colors[1]     = "0.1 0.7 0.1 0.4";
   colors[2]     = "0.1 0.7 0.1 0.0";
};

//-------------------------------------------------------------------------
//             TeleportStation_PED1 Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleEmitterData(TeleportStation_PED1 : TeleportStation_PED0 )
{
   particles        = "TeleportStation_PD1";
};

//-------------------------------------------------------------------------
//             TeleportStation_PD2 Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleData(TeleportStation_PD2 : TeleportStation_PD0)
{
   colors[0]     = "0.1 0.1 0.7 0.8";
   colors[1]     = "0.1 0.1 0.7 0.4";
   colors[2]     = "0.1 0.1 0.7 0.0";
};

//-------------------------------------------------------------------------
//             TeleportStation_PED2 Datablock Definition
//-------------------------------------------------------------------------
datablock ParticleEmitterData(TeleportStation_PED2 : TeleportStation_PED0 )
{
   particles        = "TeleportStation_PD2";
};


//-------------------------------------------------------------------------
//             TeleportTrigger Datablock Definition
//-------------------------------------------------------------------------
datablock TriggerData(TeleportTrigger)
{
   category     = "TeleportTriggers";
   tickPeriodMS = 100;
};

// ******************************************************************
//					Trigger::AttachEffect() 
// ******************************************************************
//
// 1. Define array containing PED names
// 2. Create and store reference to particle emitter node effect.
// 3. Create and store reference to physical zone effect.
//

// 1
$TeleEmitter[X] = "TeleportStation_PED0";
$TeleEmitter[Y] = "TeleportStation_PED1";
$TeleEmitter[Z] = "TeleportStation_PED2";

function Trigger::AttachEffect( %Obj )
{
   // 2
   %effect = new ParticleEmitterNode() {
      position  = vectorSub(%Obj.getWorldBoxCenter(), "0 0 2");
      rotation  = "1 0 0 0";
      scale     = "1 1 1";
      dataBlock = "basePEND";
      emitter   = $TeleEmitter[%Obj.type];
      velocity  = "1";
   };

   %Obj.myEffect = %effect;

   // 3
   %pzone = new PhysicalZone() {
      position     = vectorAdd( "1 -1 0" , %Obj.getPosition() );
      rotation     = "1 0 0 0";
      scale        = "2 2 4";
      velocityMod  = "0";
      gravityMod   = "1";
      appliedForce = "0 0 0";
      polyhedron   = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
   };
   %Obj.myPZone = %pzone;

}

// ******************************************************************
//					onRemove() 
// ******************************************************************
//
// 1. Destroy both effects (particle and Pzone)
//
function TeleportTrigger::onRemove( %DB, %Obj )
{
   // 1
   if( isObject( %Obj.myEffect ) )
      %Obj.myEffect.delete();

   if( isObject( %Obj.myPZone ) )
      %Obj.myPZone.delete();
}


// ******************************************************************
//					onEnterTrigger() 
// ******************************************************************
//
// 1. Activate the PZone. (Stopping the player.)
// 2. If the trigger is enabled...
// 2a. Check for target stations.
// 2b. Randomly select a target station.
// 2c. Disable the target station.
// 2d. Disable the target station's PZone.
// 2e. Calculate the player's new transform.
// 2f. Start the player fading out, and schedule reverse process.
// 2g. Schedule a move to occur 'after' the fade is over.
// 3. Disable this trigger.

//
function TeleportTrigger::onEnterTrigger(%DB , %Trigger , %Obj)
{

   // 1   
   %Trigger.myPZone.activate();

   // 2
   if( %Trigger.enabled )
   {
      // 2a
      // Catch case where no group/target stations exist
      if( !isObject( %Trigger.getGroup() ) || (%Trigger.getGroup().getCount() < 2 ) )
      {
         error("No group or no stations??");
         return;
      }

      // 2b
      while( %Trigger == ( %targetStation = %Trigger.getGroup().getRandomObject() ) )
      {
      }

      // 2c
      %targetStation.enabled = false;

      // 2d
      %targetStation.myPZone.deactivate();

      // 2e
      %oldTransform = %Obj.getTransform();

      %newPos       = %targetStation.getWorldBoxCenter();
      %newPos       = vectorAdd( "0 0 -0.25", %newPos );

      %newTransform = %newPos SPC getWords( %oldTransform, 3 , 6);

      // 2f
      %Obj.startFade( 750 , 0 , true );            
      %Obj.schedule( 750 , startFade , 750 , 0 , false );

      // 2g
      %Obj.schedule( 750 , setTransform , %newTransform );
      echo("Sending....");

   }   
   // 3
   %Trigger.enabled = false;
}

// ******************************************************************
//					onLeaveTrigger() 
// ******************************************************************
//
// 1. Enable both this trigger and it's PZone.
//
function TeleportTrigger::onLeaveTrigger(%DB , %Trigger , %Obj)
{
   // 1
   %Trigger.enabled = true;
   %Trigger.myPZone.activate();
}



