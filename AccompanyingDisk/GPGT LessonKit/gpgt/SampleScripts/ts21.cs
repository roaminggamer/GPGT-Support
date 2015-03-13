
////
//		TS21
////
function printTokens( %tokenString ) {

   %tmpTokens = %tokenString;

   while( "" !$= %tmpTokens ) {

      %tmpTokens = nextToken( %tmpTokens , "myToken" , ";" );

      echo( %myToken );

   }
}


function ts21() {

   printTokens( "This;is;a;sample;string;of;tokens;." );

}

ts21();