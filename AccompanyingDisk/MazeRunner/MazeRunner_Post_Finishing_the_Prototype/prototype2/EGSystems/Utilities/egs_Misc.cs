//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written or Modified by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\n\c3--------- Loading GPGT Miscellaneous Utilities ---------");
function rldmisc() {
exec("./egs_Misc.cs");
}

function isGameBase( %obj ) {
	return ( %obj.getType() == $TypeMasks::GameBaseObjectType );
}

//
// Returns scriptObject containing eight of scriptObjects, each having a 
// field named 'position' containing the location of a compass point about the
// shape.  Compass points are in a circle about the  shape, where the circle
// lies on the outer edges of the shape's world box.  The circle's radius can
// be increased by specifying an %offset.
//
function sceneObject::GetCompassPoints( %Obj, %Offset) 
{
	%worldBox       = %Obj.getWorldBox();
	%worldBoxCenter = %Obj.getWorldBoxCenter();

	echo("World Box Center =>", %worldBoxCenter);
	echo("       World Box =>", %worldBox);

	%x0 = getWord( %worldBox , 0 );
	%y0 = getWord( %worldBox , 1 );
	%z0 = getWord( %worldBox , 2 );
	%x1 = getWord( %worldBox , 3 );
	%y1 = getWord( %worldBox , 4 );
	%z1 = getWord( %worldBox , 5 );

	%xc = getWord( %worldBoxCenter , 0 );
	%yc = getWord( %worldBoxCenter , 1 );
	%zc = getWord( %worldBoxCenter , 2 );

	%offset2 =  mSqrt( 2 * (%offset * %offset) );

	%point0 = %x0 - %offset  SPC %y0 - %offset  SPC %z0;
	%point1 = %x0 - %offset2 SPC %yc            SPC %z0;
	%point2 = %x0 - %offset  SPC %y1 + %offset  SPC %z0;
	%point3 = %xc            SPC %y1 + %offset2 SPC %z0;
	%point4 = %x1 + %offset  SPC %y1 + %offset  SPC %z0;
	%point5 = %x1 + %offset2 SPC %yc            SPC %z0;
	%point6 = %x1 + %offset  SPC %y0 - %offset  SPC %z0;
	%point7 = %xc            SPC %y0 - %offset2 SPC %z0;

	%theSet = new simSet();


	for(%count = 0; %count < 8; %count++) {
		echo("Point[", %count, "] == ", %point[%count]);
		%tmpSO = new scriptObject();

		%tmpSO.position = %point[%count];
		%theSet.add(%tmpSO);
	}

	return %theSet;
}




// Note: This function will not move TSStatic() objects.
//       Their positions must be set once and only once,
//       on construcyion.  Use CalculateObjectDropPosition() instead.
//

function DropObject( %object , %offsetVector ) {
   %mask	= 
      $TypeMasks::TerrainObjectType			|
      $TypeMasks::InteriorObjectType			|
      $TypeMasks::StaticShapeObjectType		|
      $TypeMasks::PlayerObjectType			|
      $TypeMasks::ItemObjectType				|
      $TypeMasks::VehicleObjectType			|
      $TypeMasks::VehicleBlockerObjectType	|
      $TypeMasks::StaticTSObjectType;

   %oldPosition = %object.getPosition();
   %subTerrain  = getWords(%oldPosition, 0, 1) @ " -1";

   %newPosition = containerRayCast( %oldPosition , %subTerrain , %mask , 0 );

   	//echo("Ray hit => " @ %newPosition);

   %newPosition = getWords(%newPosition, 1 , 3);


   // Add user specified offset
   if("" !$= %offsetVector) {
      %newPosition = VectorAdd(%newPosition, %offsetVector);
   }

   //	echo(%object.getName() @ " => oldPostion = " @ %oldPosition);
   //	echo(%object.getName() @ " => newPosition = " @ %newPosition);


   // Update this object's position.
   //
   // Q: Why not just do a %obj.position = "x y z"; ??
   // A: This will move the object, but only setTransform()
   //    will correctly update the location/orientation of
   //    the object's bounding/world box.  
   //

   %transform = %object.getTransform();

   %transform = setWord(%transform, 0, getWord(%newPosition, 0));
   %transform = setWord(%transform, 1, getWord(%newPosition, 1));
   %transform = setWord(%transform, 2, getWord(%newPosition, 2));

   %object.setTransform(%transform);

}

function DropObjectFromMarker( %object , %marker , %offsetVector ) {
   %object.setTransform(%marker.getTransform());
   DropObject( %object, %offsetVector ) ;
}


// Note: This function is used to calculate the final position as if a
//       drop were done.

function CalculateObjectDropPosition( %oldPosition , %offsetVector ) {
   %mask	= 
      $TypeMasks::TerrainObjectType			|
      $TypeMasks::InteriorObjectType			|
      $TypeMasks::StaticShapeObjectType		|
      $TypeMasks::PlayerObjectType			|
      $TypeMasks::ItemObjectType				|
      $TypeMasks::VehicleObjectType			|
      $TypeMasks::VehicleBlockerObjectType	|
      $TypeMasks::StaticTSObjectType;

   %subTerrain  = getWords(%oldPosition, 0, 1) @ " -1";

   %newPosition = containerRayCast( %oldPosition , %subTerrain , %mask , 0 );

   %newPosition = getWords(%newPosition, 1 , 3);

   // Add user specified offset
   if("" !$= %offsetVector) {
      %newPosition = VectorAdd(%newPosition, %offsetVector);
   }

   return %newPosition;
}


function getObjLeftVector( %Obj ) 
{
  return getLeftVector( %Obj.getForwardVector() );
}

function getObjRightVector( %Obj ) 
{
  return getRightVector( %Obj.getForwardVector() );
}


function getLeftVector( %vec ) 
{
   %cross    = vectorCross( %vec , "0 0 1");
   %leftVec  = vectorNormalize( %cross );

   %leftVec  = setWord( %leftVec, 0 , mfloatlength( getWord( %leftVec , 0 ) , 3 ) );
   %leftVec  = setWord( %leftVec, 1 , mfloatlength( getWord( %leftVec , 1 ) , 3 ) );
   return %leftVec;
}

function getRightVector( %vec ) 
{
   %rightVec  = vectorScale( getLeftVector(%vec) , -1 );
   return %rightVec;
}


//
// This method is provided to allow you to dump a text file into 
// an existing ML Text control, instead of writing the same code
// over, and over, ...
//
function GuiMLTextCtrl::fillFromFile( %theControl , %textFile , %clear ) {
   
   if( %clear )
   {
      %theControl.setValue(""); // Clear it
   }
   
   	%file = new FileObject();
   	
   %fileName = expandFileName( %textFile );
   		
   %fileIsOpen = %file.openForRead( %fileName );
   
   if( %fileIsOpen ) 
   {
      while(!%file.isEOF()) 
      {
         %currentLine = %file.readLine();
         
         %theControl.addText( %currentLine, true );
      }
   }
   
   if( %theControl.isVisible() ) %theControl.forceReflow();

   %file.close();
   
   %file.delete();
}



function badWordTest()
{
   addBadWord( "poop" );
   
   %testPhrase = "This is the poop!";
   
   %filteredPhrase = filterString( %testPhrase , "@#$*!" );
   
   echo("Contains bad words? ==> " , containsBadWords( %testPhrase ) );
   
   echo("Before filtering: ", %testPhrase );
   
   echo("After  filtering: ", %filteredPhrase );

}


function strcmptest() 
{
   echo("Lexicographic comparisons are not the same as arithmetic comparisons...");
    
   echo("100 - 10 == 90, but strcmp( \"100\" , \"10\" ) == " ,  
        strcmp( "100" , "10" ) );
         
   echo("\n", "Don't forget about case-senstivity...");
   
   echo("strcmp( \"ABC\" , \"abc\" )  == " , 
        strcmp( "ABC" , "abc" ) , ", but " );
   
   echo("stricmp( \"ABC\" , \"abc\" ) == " , 
        stricmp( "ABC" , "abc" ) );
}

function removeTest()
{
   
%fields = "Torque" TAB "So" TAB "Totally" TAB "Rocks!";

// %fields will contain three fields: "Torque", "Totally", and "Rocks!".
%fields = removeField( %fields, 1 );

echo( %fields );

   
}

function testTokens( )
{

   %myTokens = "A,E,I,O,U";

   while( "" !$= %myTokens )
   {
      %myTokens = nextToken( %myTokens , "theToken" , "," ); 
    
      echo( %theToken );

    }
}

function doTestWrite( %fileName )
{
   $Game::FileHandle = new FileObject();

   if(! $Game::FileHandle.openforWrite( %fileName ) ) 
   {
      $Game::FileHandle.delete();
      error("Failed to open file!  Could not save.");
      return false; 
   }
   
   $Game::FileHandle.writeLine( "test");
   
   $Game::FileHandle.close();
   $Game::FileHandle.delete();   
  
}
function tildetest0()
{
   echo(expandFileName("~/test.txt"));
}

function tildetest2()
{
   echo(findfirstFile("creator/test.tx*"));
}
function tildetest3()
{
   echo(findFirstFile("*test.txt"));
   echo(findnextfile("*test.txt"));
}
