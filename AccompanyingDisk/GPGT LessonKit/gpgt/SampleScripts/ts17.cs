
////
//		TS17
////
function test::doit( %this , %val ) {
   echo(%this.getName(), " says ", %val );
}

function ts17() {

   %obj = new ScriptObject( test );

   %obj.schedule( 1000 , doit , "Hello world!" );

   %obj.schedule( 2000 , delete );

}

ts17();