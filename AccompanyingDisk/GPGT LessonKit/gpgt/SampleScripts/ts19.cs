
////
//		TS19
////
function accuracyCheck( %scheduledTime, %time , %repeats ) {
   %actualTime = getRealTime() - %scheduledTime;

   echo("Requested Execution Time: " , %time ,
      " :: Actual Execution Time: " , %actualTime , 
      " :: Difference (ms): " , %actualTime - %time);

   if( %repeats) {
      %repeats = %repeats - 1;
      testscheduleAccuracy ( %time ,%repeats);
   }
}

function testScheduleAccuracy( %time , %repeats ) {
   schedule( %time , 0 , accuracyCheck , getRealTime() , %time , %repeats );	
}

function ts19() {

   testScheduleAccuracy( 1 , 10 );
}


ts19();