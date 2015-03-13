
////
//		TS00
////
function ts00() {

   //
   // The following code demonstrates the issue the occurs when giving 
   // objects the same name.
   //

   %obj0   = new SimObject( test ); // a SimObject named 'test'

   %isSame = ( %obj0 == test.getID() );
   echo( "%obj0 == test.getID() => ",  %isSame ); // will echo ==> 1

   %obj1   = new SimObject( test ); // has same name as above instance

   %isSame = ( %obj0 == test.getID() );
   echo( "%obj0 == test.getID() => ", %isSame ); // will echo ==> 0

   %isSame = ( %obj1 == test.getID() );
   echo(  "%obj1 == test.getID() => ", %isSame ); // will echo ==> 1


}

ts00();