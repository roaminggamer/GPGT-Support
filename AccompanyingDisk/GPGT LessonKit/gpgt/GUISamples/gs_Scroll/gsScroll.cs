//------------------------------------------------------
// Copyright 2000-2006, GarageGames.com, Inc.
// Written by Ed Maurina, Hall Of Worlds, LLC
//------------------------------------------------------
echo("\c3--------- Loading GuiScrollCtrl Samples ---------");
//--------------------------------------------------------------------------
// gsScroll.cs
//--------------------------------------------------------------------------
//--------------------------------------------------------------------------

// Use this package to capture the scrollToBottom() and GuiScrollCtrl() calls
package AutoScrollPackage
{
   function GuiScrollCtrl::scrollToBottom( %this ) 
   {
      Parent::scrollToBottom( %this );
      %this.lastSchedule = %this.schedule( 1000, scrollToTop );
   }

   function GuiScrollCtrl::scrollToTop( %this ) 
   {
      Parent::scrollToTop( %this );
      %this.lastSchedule = %this.schedule( 1000, scrollToBottom );
   }
};
activatePackage( AutoScrollPackage );

function AutoScroll::onWake( %this ) {
   %this.lastSchedule = %this.schedule( 500, scrollToBottom );
}

function AutoScroll::onSleep( %this ) {
   cancel( %this.lastSchedule );
}


// This will never fire
function GuiScrollCtrl::onAction( %this ) {
	echo("GUIScrollCtrl::onAction("@%this@")");
}

//--------------------------------------------------------------------------
//--------------------------------------------------------------------------

exec("./gsScroll.gui");

//--------------------------------------------------------------------------
//--------------------------------------------------------------------------


function TestScrollMLText::onWake( %this ) {
	%this.setValue(""); // Clear it

	%file = new FileObject();

	%fileName = expandFileName( "./TestScrollMLTextContent.txt" );

	echo( "Attempt to open " , %fileName );

	%fileIsOpen = %file.openForRead( %fileName );

	echo( "Open for read " , (%fileIsOpen ? "succeeded" : "failed" ) );

	if( %fileIsOpen ) {
		while(!%file.isEOF()) {

			%currentLine = %file.readLine();

			echo(%currentLine);

			%this.addText( %currentLine, force );

		}
	}

	if( %this.isVisible() ) %this.forceReflow();

	%file.close();
	%file.delete();

}



