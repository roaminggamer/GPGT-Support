
////
//		TS01
////
function ts01() {

   %obj = new Player( ) 
   {
      datablock = basePlayer;
   };

   echo( %obj.getClassName() ); // will echo ==> Player

   echo ( %obj.getDatablock().getClassName() ); // will echo ==> PlayerData

   echo ( %obj.getDatablock().getName() ); // will echo ==> basePlayer

   %obj.delete();

}

ts01();