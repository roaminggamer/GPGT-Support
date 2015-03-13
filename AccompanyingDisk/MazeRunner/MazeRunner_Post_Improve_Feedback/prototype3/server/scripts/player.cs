//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

// Load dts shapes and merge animations
exec("~/data/BlueGuy/player.cs");


datablock PlayerData(BlueGuy)
{
   renderFirstPerson = false;
   emap = true;
   
   className = Armor;
   shapeFile = "~/data/BlueGuy/player.dts";
   cameraMaxDist = 3;
   computeCRC = true;
  
   canObserve = true;
   cmdCategory = "Clients";

   cameraDefaultFov = 90.0;
   cameraMinFov = 5.0;
   cameraMaxFov = 120.0;
   
   minLookAngle = -1.4;
   maxLookAngle = 1.4;
   maxFreelookAngle = 3.0;

   mass = 90;
   drag = 0.3;
   maxdrag = 0.4;
   density = 10;

   runForce = 48 * 90;
   runEnergyDrain = 0;
   minRunEnergy = 0;
   maxForwardSpeed = 14;
   maxBackwardSpeed = 13;
   maxSideSpeed = 13;

   jumpForce = 8.3 * 90;
   jumpEnergyDrain = 0;
   minJumpEnergy = 0;
   jumpDelay = 15;

   recoverDelay = 9;
   recoverRunForceScale = 1.2;

   minImpactSpeed = 1000;

   boundingBox = "1.2 1.2 2.3";
   pickupRadius = 0.75;
   
   runSurfaceAngle  = 70;
   jumpSurfaceAngle = 80;

   minJumpSpeed = 20;
   maxJumpSpeed = 30;

   horizMaxSpeed = 68;
   horizResistSpeed = 33;
   horizResistFactor = 0.35;

   upMaxSpeed = 80;
   upResistSpeed = 25;
   upResistFactor = 0.3;
   

   groundImpactMinSpeed    = 10000;
};

