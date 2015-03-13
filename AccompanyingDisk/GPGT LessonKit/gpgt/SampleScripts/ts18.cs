
////
//		TS18
////
function ts18() {

   %obj = new ScriptObject( test );

   %obj.schedule( 1000 , doit , "Hello world!" );

   %obj.delete();

}

ts18();