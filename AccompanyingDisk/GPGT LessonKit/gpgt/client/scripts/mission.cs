//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Mission start / end events sent from the server
//----------------------------------------------------------------------------
function clientCmdMissionStart(%seq)
{
   // echo("\c4 clientCmdMissionStart()");
}

function clientCmdMissionEnd(%seq)
{
   // echo("\c4 clientCmdMissionEnd()");

   alxStopAll();

   // Disable mission lighting if it's going, this is here
   // in case the mission ends while we are in the process
   // of loading it.
   $lightingMission = false;
   $sceneLighting::terminateLighting = true;
}
