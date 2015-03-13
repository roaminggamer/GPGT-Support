//------------------------------------------------------
// Copyright 2000-2005, GarageGames.com, Inc.
// Written, modfied, or otherwise interpreted by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\n\c3--------- Loading Mission Level Loader Scripts ---------");
function rldll() {
   exec("./LevelLoader.cs");
}

$BaseElevation    = 90; // Starting placement elevation (do not modify in scripts)
$LevelIncrement   = 4;  // Elevation Increment (do not modify in scripts)

$CurrentElevation = $BaseElevation ; // Working elevation ( we modify this variable ).

//
// The following globals are used to simplify comparisons.
//
// Instead of examining a token and checking for A, B, C, D, etc.
// We can now pass the token as an index to the BLOCKCLASS,
// OBSTACLECLASS, etc. arrays and get back a string identify
// what 'class' of game elements the token is identifying.
//
$BLOCKCLASS[A] = NORMAL;
$BLOCKCLASS[B] = NORMAL;
$BLOCKCLASS[C] = NORMAL;
$BLOCKCLASS[D] = NORMAL;
$BLOCKCLASS[E] = NORMAL;
$BLOCKCLASS[F] = NORMAL;
$BLOCKCLASS[G] = NORMAL;
$BLOCKCLASS[H] = NORMAL;
$BLOCKCLASS[I] = NORMAL;
$BLOCKCLASS[J] = NORMAL;
$BLOCKCLASS[0] = FADE;
$BLOCKCLASS[1] = FADE;
$BLOCKCLASS[2] = FADE;
$BLOCKCLASS[3] = FADE;
$BLOCKCLASS[4] = FADE;
$BLOCKCLASS[5] = FADE;
$BLOCKCLASS[6] = FADE;
$BLOCKCLASS[7] = FADE;
$BLOCKCLASS[8] = FADE;
$BLOCKCLASS[9] = FADE;

$OBSTACLECLASS[0] = FIREBALL;
$OBSTACLECLASS[1] = FIREBALL;
$OBSTACLECLASS[2] = FIREBALL;
$OBSTACLECLASS[3] = FIREBALL;
$OBSTACLECLASS[4] = FIREBALL;
$OBSTACLECLASS[5] = FIREBALL;
$OBSTACLECLASS[6] = FIREBALL;
$OBSTACLECLASS[7] = FIREBALL;
$OBSTACLECLASS[8] = FIREBALL;
$OBSTACLECLASS[9] = FIREBALL;
$OBSTACLECLASS[X] = TELEPORT;
$OBSTACLECLASS[Y] = TELEPORT;
$OBSTACLECLASS[Z] = TELEPORT;

$PICKUPCLASS[C] = COIN;

$DROPCLASS[P]   = DROP;



// ******************************************************************
//					BuildLevel() 
// ******************************************************************
//
// 1. Reset the working elevation.
// 2. Create a new file object and attempt to open a level file.
// 3. Read the 'next mission' line from the level file.
// 4. Create all the SimGroups we will use to organize the level contents.
// 5. Parse the file until we hit the end-of-file
// 6. Parse the layer 'task'.
// 6a. Adjust Elevation Up
// 6b. Adjust Elevation Down
// 6c. Prepare to lay down a layer
// 6d. Bad layer task defintion
// 7. Parse the layer 'type'.
// 7a. Put blocks in level
// 7b. Put obstacles in level
// 7c. Put pickups in level
// 7d. Drop the player into the game
// 7e. Bad layer type defintion
// 8. Close the file and delete the file object.
// 9. Start any fadeblocks and fireball blocks we have
// 
//



function BuildLevel( %levelNum ) {
   
   // 1
   $CurrentElevation = $BaseElevation ;

   // 2
   %file = new FileObject();
   %fileName = expandFileName( "~/data/Missions/LevelMaps/Level" @ %levelNum @ ".txt");
   if("" $= %fileName ) 
   {
      return false;
   }
   %fileIsOpen = %file.openForRead( %fileName );
   if( ! %fileIsOpen ) return false;

   // 3
   $Game::NextLevelMap         = %file.readLine();

   if ( ($Game::NextLevelMap $= "") )
   {
      %file.close();
      %file.delete();
      return false;
   }

   // 4
   if( isObject( gameLevelGroup )  )
      gameLevelGroup.delete();

   MissionGroup.add( new SimGroup( gameLevelGroup ) );

   gameLevelGroup.add( new SimGroup( mazeBlocksGroup ) );
   gameLevelGroup.add( new SimGroup( fadeGroup ) );   
   gameLevelGroup.add( new SimGroup( FireBallMarkersGroup ) );   
   gameLevelGroup.add( new SimGroup( TeleportStationGroupX ) );   
   gameLevelGroup.add( new SimGroup( TeleportStationGroupY ) );   
   gameLevelGroup.add( new SimGroup( TeleportStationGroupZ ) );   
   gameLevelGroup.add( new SimGroup( TeleportStationEffectsGroup ) );   
   gameLevelGroup.add( new SimGroup( CoinsGroup ) );   
   

   // 5
   while(!%file.isEOF() ) 
   {
      %layerTask = %file.readLine();

      // 6
      switch$( %layerTask )
      {
         // 6a
         case "LAYER_UP":
            $CurrentElevation +=  $LevelIncrement ;
            continue;

         // 6b
         case "LAYER_DOWN":
            $CurrentElevation -=  $LevelIncrement ;
            continue;

         // 6c
         case "LAYER_DEFINE":
            %layerType = %file.readLine();

         // 6d
         default:
            error(" BuildLevel() Bad layer task ==> ", %layerTask );
            %file.close();
            %file.delete();
            return false;
      }

      // 7
      switch$( %layerType )
      {
         // 7a
         case "BLOCKS":
            %layerArray = loadLayerDefinition( %file );
            layOutBlocks( %layerArray );

         // 7b
         case "OBSTACLES":
            %layerArray = loadLayerDefinition( %file );
            layOutObstacles( %layerArray );

         // 7c
         case "PICKUPS":
            // 
            %layerArray = loadLayerDefinition( %file );
            layOutPickups( %layerArray );

         // 7d
         case "PLAYERDROPS":
            %layerArray = loadLayerDefinition( %file );
            playerDrop( %layerArray );

         // 7e
         default:
            error(" BuildLevel() Bad layer type ==> ", %layerType );
            %layerArray.delete();
            %file.close();
            %file.delete();
            return false;
      }
   }

   // 8 
   %file.close();
   %file.delete();

   // 9
   if( fadeGroup.getCount() )
      fadeGroup.schedule( 5000 , fadePass );

   if( FireBallMarkersGroup.getCount() )
      FireBallMarkersGroup.schedule( 5000 , firePass );   
}

// ******************************************************************
//					loadLayerDefinition() 
// ******************************************************************
//
// 1. Create a array object to store the level definition in.
// 2. Read every line in the file into our array oabject, one line per entry.
// 3. Check that the proper number of lines was read in.
//
function loadLayerDefinition( %file ) 
{
   // 1
   %layerArray = new ScriptObject( arrayObject );

   %count = 0;

   // 2
   while(!%file.isEOF() && (%count < 16) ) 
   {
      %layerArray.addEntry( %file.readLine() );

      //echo("%level[",%count,"] ==", %layerArray.getEntry( %count ) );
      
      %count++;
   }

   // 3
   if( %count != 16 )
   {
      error(" loadLayerDefinition()- Insufficient lines in layer definition!");
   }

   return %layerArray;
}

// ******************************************************************
//					layOutBlocks() 
// ******************************************************************
//
// 1. Iterate over the arrayObject, line by line, token by token.
// 2. Select class of object to create.
// 2a. Create a normal block.
// 2a. Creaet a fade block.
// 3. Destroy the array object.
//
function layOutBlocks( %layerArray )
{
   // 1
   for(%x = 0; %x < 16; %x++) 
   {
      %row = 16;

      for(%y = 0; %y < 16; %y++)       
      {
         %row--;
         %actX = (%x * 4) - 30;
         %actY = (%y * 4) - 30;

         %layer = %layerArray.getEntry( %row );

         %blockType = getSubStr( %layer , %x , 1 );

         // 2
         switch$($BLOCKCLASS[%blockType])
         {
            // 2a
            case NORMAL:
               %block =  new TSStatic() 
               {
                  shapeName	= "~/data/MazeRunner/Shapes/MazeBlock/block" @ %blockType @ ".dts";
                  position	   = %actX SPC %actY SPC $CurrentElevation;
                  scale		   = "1 1 1";
               };
               mazeBlocksGroup.add(%block);

            // 2b
            case FADE:
               %block = new StaticShape() {
                  position  = %actX SPC %actY SPC $CurrentElevation;
                  rotation  = "1 0 0 0";
                  scale     = "1 1 1";
                  dataBlock = "FadeBlock";
                  timer     = (%blockType + 1) * 1000;
                  action    = "waitToFadeOut";
                  maxTime   = (%blockType + 1) * 1000;
               };     
               fadeGroup.add(%block);
         }
      }
   }

   // 3
   %layerArray.delete();
}


// ******************************************************************
//					layOutObstacles() 
// ******************************************************************
//
// 1. Iterate over the arrayObject, line by line, token by token.
// 2. Select class of object to create.
// 2a. Create a fireballl block.
// 2a. Creaet a teleport station.
// 3. Destroy the array object.
//
//
function layOutObstacles( %layerArray )
{
   // 1
   for(%x = 0; %x < 16; %x++) 
   {
      %row = 16;

      for(%y = 0; %y < 16; %y++)       
      {
         %row--;
         %actX = (%x * 4) - 30;
         %actY = (%y * 4) - 30;

         %layer = %layerArray.getEntry( %row );

         %obstacleType = getSubStr( %layer , %x , 1 );

         // 2
         switch$($OBSTACLECLASS[%obstacleType])
         {
            // 2a
            case FIREBALL:
               //echo("\c2 Loading fireball");
               %fireballMarker = new StaticShape() {
                  position  = %actX SPC %actY SPC $CurrentElevation;
                  rotation  = "1 0 0 0";
                  scale     = "1 1 1";
                  dataBlock = "FireBallBlock";
                  type      = %obstacleType;
               };     
               %fireballMarker.setSkinName( fire );

               FireBallMarkersGroup.add(%fireballMarker);

            // 2b
            case TELEPORT:
               echo("\c2 Loading teleport station");
               %teleportStation = new Trigger() {
                  position  = ( %actX - 2 ) SPC ( %actY + 2 ) SPC $CurrentElevation;
                  rotation  = "1 0 0 0";
                  scale     = "4 4 4";
                  polyhedron = "0.0000000 0.0000000 0.0000000 1.0000000 0.0000000 0.0000000 0.0000000 -1.0000000 0.0000000 0.0000000 0.0000000 1.0000000";
                  dataBlock = "TeleportTrigger";
                  type      = %obstacleType;
                  group     = (TeleportStationGroup @ %obstacleType).getID();
                  enabled   = true;
               };     
               %teleportStation.attachEffect();

               (TeleportStationGroup @ %obstacleType).add(%teleportStation);

               TeleportStationEffectsGroup.add( %teleportStation.myEffect );
               TeleportStationEffectsGroup.add( %teleportStation.myPZone );
               
         }
      }
   }
   
   // 3
   %layerArray.delete();
}

// ******************************************************************
//					layOutPickups() 
// ******************************************************************
//
// 1. Iterate over the arrayObject, line by line, token by token.
// 2. Select class of object to create.
// 2a. Create a coin.
// 3. Destroy the array object.
//
function layOutPickups( %layerArray )
{
   // 1
   for(%x = 0; %x < 16; %x++) 
   {
      %row = 16;

      for(%y = 0; %y < 16; %y++)       
      {
         %row--;
         %actX = (%x * 4) - 30;
         %actY = (%y * 4) - 30;

         %layer = %layerArray.getEntry( %row );

         %blockType = getSubStr( %layer , %x , 1 );

         // 2
         switch$($PICKUPCLASS[%blockType])
         {
            //2a
            case COIN:
               %coin =  new item() 
               {
                  position	   = %actX SPC %actY SPC $CurrentElevation;
                  dataBlock	= Coin;
                  static		= 1;
                  scale		   = "1 1 1";
               };

               CoinsGroup.add(%coin);
               //%coin.applyImpulse(%coin.getWorldBoxCenter(), "0 0 1");
         }
      }
   }
   
   //3
   %layerArray.delete();
}

// ******************************************************************
//					playerDrop() 
// ******************************************************************
//
// 1. Iterate over the arrayObject, line by line, token by token.
// 2. Select class of object to create.
// 2a. Drop the player in (after a short pause).
// 3. Destroy the array object.
//
function playerDrop( %layerArray )
{
   // 1
   for(%x = 0; %x < 16; %x++) 
   {
      %row = 16;

      for(%y = 0; %y < 16; %y++)       
      {
         %row--;
         %actX = (%x * 4) - 30;
         %actY = (%y * 4) - 30;

         %layer = %layerArray.getEntry( %row );

         %blockType = getSubStr( %layer , %x , 1 );

         // 2
         switch$($DROPCLASS[%blockType])
         {
            // 2a
            case DROP:
               $Game::Player.setTransform ( pickSpawnPoint() );
               $Game::Player.spawnPointTransform = (%actX SPC %actY SPC $CurrentElevation);
               $Game::Player.schedule( 2800 , setVelocity , "0 0 0" );
               $Game::Player.schedule( 3000 , setTransform , %actX SPC %actY SPC $CurrentElevation );
               %layerArray.delete();
               return;

         }
      }
   }
   // 3
   %layerArray.delete();
}

