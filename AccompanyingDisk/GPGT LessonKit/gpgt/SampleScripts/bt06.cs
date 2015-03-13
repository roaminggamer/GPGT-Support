
////
//		BT06
////
function bt06() {

	$a="This is a regular string";
	$b='This is a tagged string';
	echo(" Regular string: " , $a);
	echo("  Tagged string: " , $b);
	echo("Detagged string: " , detag($b) );

}

bt06();