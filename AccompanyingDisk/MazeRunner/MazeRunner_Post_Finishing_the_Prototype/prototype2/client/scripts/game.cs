//-----------------------------------------------------------------------------
// Torque Game Engine 
// Copyright (C) GarageGames.com, Inc.
//-----------------------------------------------------------------------------

//----------------------------------------------------------------------------
// Game start / end events sent from the server
//----------------------------------------------------------------------------

function clientCmdGameStart(%seq)
{
   PlayerListGui.zeroScores();
}

function clientCmdGameEnd(%seq)
{
   // Stop local activity... the game will be destroyed on the server
   alxStopAll();

   // Display the end-game screen
   Canvas.setContent(MainMenuGui);
}
