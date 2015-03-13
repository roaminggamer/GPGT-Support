//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading Template Sample ---------");

//--------------------------------------------------------------------------
// gsTemplate.cs
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Profiles
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Scripts
//--------------------------------------------------------------------------

//--------------------------------------------------------------------------
// Load Interface Definition
//---------------------------------------------------------------
exec("./gsStackControl.gui");

function guiStackControlExample::onWake( %this )
{
   if( isObject( ButtonStackScroll ) ) return;
   
   %this.schedule( 250, buildVerticalStack );
}

function guiStackControlExample::onSleep( %this )
{
   if( isObject( ButtonStackScroll ) )    
      ButtonStackScroll.schedule( 0, delete );
}


// The following method creates a vertical stack of 100 buttons, using the
// guiStackControl GUI.

function guiStackControlExample::buildVerticalStack( %this )
{
   %width  = getWord( %this.Extent, 0);
   %height = getWord( %this.Extent, 1);
   
   new guiScrollCtrl ( ButtonStackScroll )
   {
      position = ((%width - 120) / 2) SPC 5;
      extent   = "120" SPC %height-10;   
      hScrollBar = "alwaysOff";      
      vScrollBar = "alwaysOn";
         
      new guiStackControl( ButtonStack ) 
      {
         extent = "100 100";
      };
   };
      
   %this.add( ButtonStackScroll );

   for( %count = 0; %count < 100; %count++ )
   {
      %button = new guiButtonCtrl()
      {
         text = "button" SPC %count;
         extent = "100 30";
      };
      ButtonStack.add( %button );
   }
}