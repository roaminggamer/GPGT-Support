
////
//		BT16
////
function bt16() {

	// Note:  The first part of this code is not in the book.
	// Because the player has not been created, we can't get its
	// ID.  So, for the purpose of this sample, we are making a
	// temporary simObject() in the player's place.

	%obj = new simObject( "myGuy" );

	%obj.position = "0.0 0.0 0.0";

	//
	// BEGIN BOOK CODE
	//

	$player_name = "myGuy";
	$player_id   = $player_name.getID();

	echo( $player_name.position );
	echo( $player_name.getID() );
	echo( "myGuy".getid() );
	echo( myGuy.getid() );

	//
	// END BOOK CODE
	//

	if ( isObject( %obj ) ) 
	    %obj.delete();
}

bt16();