
////
//		TS16
////
function ts16() {

   %obj = new ScriptObject( test );

   schedule( 1000 , %obj , echo, "Hello world!" );
   %obj.delete();

}


ts16();