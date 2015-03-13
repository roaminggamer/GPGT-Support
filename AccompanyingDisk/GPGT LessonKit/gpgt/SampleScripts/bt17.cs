////
//		BT17
////
function bt17() {
    
	// Note:  The first part of this code is not in the book.
	// Because the player has not been created, we can't get its
	// ID.  So, for the purpose of this sample, we are making a
	// temporary simObject() in the player's place.

	$player_id = new simObject( "myGuy" );


	//
	// BEGIN BOOK CODE
	//

	// new_var will not be created because we are only ‘reading’ it
	echo( $player_id.new_var ); 

	// new_var2 will be created and initialized to "Hello"
	$player_id.new_var2 = "Hello";

	echo( $player_id.new_var2 ); 

	//
	// END BOOK CODE
	//

	if ( isObject( $player_id ) ) 
	    $player_id.delete();
}

bt17();