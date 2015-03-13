//------------------------------------------------------
// Copyright 2000-2005, GarageGames.com, Inc.
// Written, modfied, or otherwise interpreted by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\n\c3--------- Loading Coin Datablock Definition and Scripts ---------");

function rldcoin() {
exec("./coins.cs");
}

datablock ItemData( Coin : BaseItem )
{
   shapeFile = "~/data/MazeRunner/Shapes/items/coin.dts";
   category  = "GameItems";
   sticky    = true; 
   lightType = NoLight; 
   mass      = 1.0;
   respawn   = false;
};

// ******************************************************************
//					onAdd() 
// ******************************************************************
//
// 1. Force coin to be static and to use the item class' rotation animation.
//
function Coin::onAdd( %DB , %Obj )
{
   Parent::onAdd( %DB , %Obj );
   
   // 1
   %Obj.static			= true; 
   %Obj.rotate			= true;
}
