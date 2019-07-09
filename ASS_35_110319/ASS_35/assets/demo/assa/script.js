$(document).ready(function(){
	$(".selectbox").selectBoxIt({
        autoWidth: false
    });
    
	$( "select" ).change(function() {
	  if($(this).val() !== ''){
	  	var t = $(this).parent().find( ".select2-selection--single" );
        $(t).removeClass('error-fill');
        var remove_lb = $(this).parent().find('label.validation-err-label');
        remove_lb.remove();
	  }
	});
	$( "#salut" ).change(function() {
	  if($(this).val()== 'OTH'){
	  	$('.specify_oth').removeClass('hide');
	  	$('#spc_oth_salut').addClass('required');
	  }else{
	  	if(!$('.specify_oth').hasClass('hide')){
	  		$('.specify_oth').addClass('hide');
	  		$('#spc_oth_salut').removeClass('required');
	  	}else{
	  		//$('#spc_oth_salut').removeAttr('required');
	  	}
	  }
	});
	
	
	
	$('.arr_connect').click(function(){
		if($(this).val() == 1){
			$('#arr_con_y').removeAttr('disabled');
			$('#arr_con_n').attr('disabled','disabled');
		}else{
			$('#arr_con_n').removeAttr('disabled');
			$('#arr_con_y').attr('disabled','disabled');
		}
	});
	
	$('.dep_connect').click(function(){
		if($(this).val() == 1){
			$('#dep_con_y').removeAttr('disabled');
			$('#dep_con_n').attr('disabled','disabled');
		}else{
			$('#dep_con_n').removeAttr('disabled');
			$('#dep_con_y').attr('disabled','disabled');
		}
	});
	
	
	//$('.byplane').click(function(){
	//	$('.flightcontent').show();
	//});
	//$('.bycar').click(function(){
	//	$('.flightcontent').hide();
	//	//$('.flight_input').val('');
	//});
	
    
	
	//$('.state_select').change(function(){
	//	var a = $(this).val();
	//	var b = ["VI1", "VI2", "SPK"];
	//	var c = ["VI1","VI2"];
	//	if(jQuery.inArray(a,b) == -1){
	//		$('.section4').show();
	//		$('.section2').show();
	//		//$('.troom1').addClass('required');
	//		//$('#date_checkin').addClass('required');
	//		//$('#date_checkout').addClass('required');
			
	//			$('.section3').show();
			
	//		//}
	//	    // the element is not in the array
	//	}else{
	//		if(a == 'SPK'){
				
	//			//$('.travel_').addClass('required');
	//			//$('.section3').show();
	//			$('.section2').show();
	//			//$('.troom1').addClass('required');
	//			//$('#date_checkin').addClass('required');
	//			//$('#date_checkout').addClass('required');
	//			// console.log('dd');
	//			// $('.section2 ').hide();
	//			var xx = $('[name="sec3[travelby]"]:checked').val();
	//		//if (typeof xx !== "undefined") {
	//			if(typeof xx !== "undefined" && xx == 2){
	//				$('.section3').show();
	//			}
	//			// var yy = $('[name="sec5[a1]"]:checked').val();
	//			// if(typeof yy !== "undefined" && yy == 1){
	//				// $('.t-sizex').addClass('required');
	//			// }
				
	//		}else if(a == 'VI2'){
				
	//			//$('.travel_').addClass('required');
	//			//$('.section3').show();
	//			$('.section2').show();
	//			//$('.troom1').addClass('required');
	//			//$('#date_checkin').addClass('required');
	//			//$('#date_checkout').addClass('required');
	//			// console.log('dd');
	//			// $('.section2 ').hide();
	//			var xx = $('[name="sec3[travelby]"]:checked').val();
	//		//if (typeof xx !== "undefined") {
	//			if(typeof xx !== "undefined" && xx == 2){
	//				$('.section3').show();
	//			}
	//			$('.section2').hide();
	//			//$('.troom1').removeClass('required');
	//			//$('#date_checkin').removeClass('required');
	//			//$('#date_checkout').removeClass('required');
	//		}else{
				
	//			//$('.travel_').removeClass('required');
	//			$('.section3').hide();
	//			$('.section2').hide();
	//			//$('.troom1').removeClass('required');
	//			//$('#date_checkin').removeClass('required');
	//			//$('#date_checkout').removeClass('required');
	//		}
			
	//		$('.section4').hide();
	//		//$('.secactv').removeClass('required');
	//		//$('.t-sizex').removeClass('required');
	//		//$('.secactv').attr('checked', false);
	//		//$('[name^=sec5]').attr('checked', false);
	//		//$('#size_shirt').val('');
	//		// $("[name^=sec5]").each(function() {
	//		  	// $($(this).parent()).removeClass('checked');
	//		// });
	//	}
	//});
	
	
	
	
	
	
});

