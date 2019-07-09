/* ------------------------------------------------------------------------------
*
*  # Form validation
*
*  Specific JS code additions for form_validation.html page
*
*  Version: 1.3
*  Latest update: Feb 5, 2016
*
* ---------------------------------------------------------------------------- */

$(function() {
	var assa = false;

    // Form components
    // ------------------------------

    // Switchery toggles
    var elems = Array.prototype.slice.call(document.querySelectorAll('.switchery'));
    elems.forEach(function(html) {
        var switchery = new Switchery(html);
    });


    // Bootstrap switch
    // $(".switch").bootstrapSwitch();


    // Bootstrap multiselect
    // $('.multiselect').multiselect({
        // checkboxName: 'vali'
    // });


    // Touchspin
    // $(".touchspin-postfix").TouchSpin({
        // min: 0,
        // max: 100,
        // step: 0.1,
        // decimals: 2,
        // postfix: '%'
    // });


    // Select2 select
    $('.select').select2({
        minimumResultsForSearch: Infinity
    });


    // Styled checkboxes, radios
    $(".styled, .multiselect-container input").uniform({ radioClass: 'choice' });


    // Styled file input
    $(".file-styled").uniform({
        fileButtonClass: 'action btn bg-blue'
    });



    // Setup validation
    // ------------------------------

    // Initialize
//    var validator = $(".form-validate-jquery").validate({
//        ignore: 'input[type=hidden], .select2-search__field', // ignore hidden fields
//        errorClass: 'validation-err-label',
//        successClass: 'validation-valid-label',
//        highlight: function(element, errorClass) {
//            $(element).removeClass(errorClass);
//         //$(element).removeClass('error-fill');
            
//        },
//        unhighlight: function(element, errorClass) {
//            $(element).removeClass(errorClass);
            
//            if($($(element).parent()).find( ".select2-selection--single" )){
//            	var ab = $($(element).parent()).find( ".select2-selection--single" );
//            	$(ab).removeClass('error-fill');
//            }
//            if($($(element).parent()).find( ".selectboxit-btn" )){
//            	var cd = $($(element).parent()).find( ".selectboxit-btn" );
//             	$(cd).removeClass('error-fill');
//             } 
//                // $(cv).addClass('error-fill');
//           // $(element).removeClass('error-fill');
//        },

//        // Different components require proper error label placement
//        errorPlacement: function(error, element) {

//            // Styled checkboxes, radios, bootstrap switch
//            if (element.parents('div').hasClass("checker") || element.parents('div').hasClass("choice") || element.parent().hasClass('bootstrap-switch-container') ) {
//                if(element.parents('label').hasClass('checkbox-inline') || element.parents('label').hasClass('radio-inline')) {
//                    error.appendTo( element.parent().parent().parent().parent() );
//                }
//                 else {
//                    error.appendTo( element.parent().parent().parent().parent().parent() );
//                }
//            }

//            // Unstyled checkboxes, radios
//            else if (element.parents('div').hasClass('checkbox') || element.parents('div').hasClass('radio')) {
//                error.appendTo( element.parent().parent().parent() );
                 
//            }

//            // Input with icons and Select2
//            else if (element.parents('div').hasClass('has-feedback') || element.hasClass('select2-hidden-accessible') || element.hasClass('selectbox')) {
//                error.appendTo( element.parent() );
//                //$(element).addClass('error-fill');
//                 var cv = $(element.parent()).find( ".select2-selection--single" );
//                $(cv).addClass('error-fill');
                 
//                 if($(element.parent()).find( ".selectboxit-btn" )){
//                 	$($(element.parent()).find( ".selectboxit-btn" )).addClass('error-fill');
//                 }
//            }

//            // Inline checkboxes, radios
//            else if (element.parents('label').hasClass('checkbox-inline') || element.parents('label').hasClass('radio-inline')) {
//                error.appendTo( element.parent().parent() );
//            }

//            // Input group, styled file input
//            else if (element.parent().hasClass('uploader') || element.parents().hasClass('input-group')) {
//                error.appendTo( element.parent().parent() );
//            }

//            else {
            	 
//                error.insertAfter(element);
//                $(element).addClass('error-fill');
               
//            }
//        },
//        validClass: "validation-valid-label",
//        success: function(label,element) {
//            //label.addClass("validation-valid-label").text("Success.")
//	            label.remove();
//            $(element).removeClass('error-fill');
//        },
//        submitHandler: function(form) {
//        	if(1==1){
//                form.submit();
//                console.log(2);
//        	}else{
//        		$('.form-password').val('');
//				$('#form_mail').val($('.email_form').val());
//                $('#modal_form_inline').modal('show');
//                console.log("Vao day choi")
//        	}
//        	// }else{
//        		// var state_chk = $('.state_select').val();
//        		// //if(state_chk !== '' && $(".cbox-type").is(':checked')){
//        			// var test=true;
//        		// if(test){
//        			// var newaccess = {
//					// email:$('.email_form').val()
//				// };
//// 					
//					// var url = $('body').attr('url-data');
//			 		// $.post(url+'/access_check', {data: newaccess}, function(result){
//			       	// if(result){
//			       		// var rs = JSON.parse(result);
//			       		// console.log(rs);
//			       		// if(rs == '104'){
//			       			// bootbox.confirm("Confirm your update.", function(result){ 
//				        		// if(result){
//				        			// confirmupdate();
//				        		// }
//				        	// });
//			       		// }else if(rs == '102'){
//			       			// bootbox.alert("Email already exists.", function(){ 
//					           // $('.email_form').focus();
//					           // $('.email_form').css('border-color','red');
//					           // $('.email_form').css('background','#ffe3e3');
//					       // });
//			       		// }else if(rs == '105'){
//			       			// window.location.replace(url+'/login');
//			       		// }else if(rs == '103'){
//			       			// bootbox.confirm("Do you to change email?", function(result){ 
//				        		// if(result){
//				        			// bootbox.confirm("Confirm your update.", function(resultX){ 
//						        		// if(resultX){
//						        			// confirmupdate();
//						        		// }
//						        	// });
//				        		// }
//				        	// });
//			       		// }else{
//			       			// bootbox.alert("Found Error.", function(){ 
//					            // $('.email_form').focus();
//					       // });
//			       		// }
//			       	// }else{
//			       		// bootbox.confirm("Do you confirm your registration information.", function(result){ 
//			        		// if(result){
//			        			// $('#form_mail').val($('.email_form').val());
//			        			// $('#modal_form_inline').modal('show');
//			        		// }
//			        	// });
//			       	// }
//			    // });
//        		// }else{
//        			// $('td.chk_type_box').removeClass('notchk_type');
//        			// if(state_chk == ''){
//				        // $('html,body').animate({
//				          // scrollTop: $('.state_').offset().top
//				        // }, 1000);
//				        // $('.state_select').addClass('error-fill');
//				        // $('.state_').append('<label class="state-msg validation-err-label">This country is required.</label>')
//        			// }else{
//        				// $('td.chk_type_box').addClass('notchk_type');
//        				// $('.roomtype-msg').html('This room type is required.');
//	        			// $('.roomtype-msg').show();
//	        			// $('html,body').animate({
//				          // scrollTop: $('.section2').offset().top
//				        // }, 1000);
//        			// }
//        		// }
////         		
////         		
//        	// }
        	
//         // confirm("Press a button!");
//        // form.submit();
//    	},
//        // rules: {
//            // password: {
//                // minlength: 5
//            // },
//            // repeat_password: {
//                // equalTo: "#password"
//            // },
//            // email: {
//                // email: true
//            // },
//            // repeat_email: {
//                // equalTo: "#email"
//            // },
//            // s: {
//                // minlength: 10
//            // },
//            // maximum_characters: {
//                // maxlength: 10
//            // },
//            // minimum_number: {
//                // min: 10
//            // },
//            // maximum_number: {
//                // max: 10
//            // },
//            // number_range: {
//                // range: [10, 20]
//            // },
//            // url: {
//                // url: true
//            // },
//            // date: {
//                // date: true
//            // },
//            // date_iso: {
//                // dateISO: true
//            // },
//            // cardno: {
//                // number: true
//            // },
//            // reserveno: {
//                // number: true
//            // },
//            // digits: {
//                // digits: true
//            // },
//            // creditcard: {
//                // creditcard: true
//            // },
//            // basic_checkbox: {
//                // minlength: 2
//            // },
//            // styled_checkbox: {
//                // minlength: 2
//            // },
//            // switchery_group: {
//                // minlength: 2
//            // },
//            // switch_group: {
//                // minlength: 2
//            // }
//        // }
//    });
	$('.cbox-type').change(function() {
		if($('td.chk_type_box').hasClass('notchk_type')){
			$('td.chk_type_box').removeClass('notchk_type');
			$('.roomtype-msg').hide();
		}
	});
	// $('.data-req').each(function() {
		// var msg_='This filed is required.';
		// var number_ = false;
		// switch ($(this).attr('name')) {
		    // case 'sec1[title]':msg_ = "The title is required.";break;
		    // case 'sec1[last]':msg_ = "The lastname is required.";break;
		    // case 'sec1[first]':msg_ = "The firstname is required.";break;
		    // case 'sec1[gender]':msg_ = "The gender is required.";break;
		    // case 'sec1[address]':msg_ = "The address is required.";break;
		    // case 'sec1[organize]':msg_ = "The organization is required.";break;
		    // case 'sec1[position]':msg_ = "The position is required.";break;
		    // case 'sec1[department]':msg_ = "The department is required.";break;
		    // case 'sec1[telno]':msg_ = "The telephone number is required.";break;
		    // case 'sec1[email]':msg_ = "The email is required.";break;
		    // case 'sec1[meal]':msg_ = "The meal preference is required.";break;
		    // case 'sec2[reserveno]':number_ = true;break;
		    // case 'sec3[namecard]':msg_ = "The name on card is required..";break;
		    // case 'sec3[cardno]':msg_ = "The credit card number is required.";number_ = true;break;
		    // case 'sec4[arr_airline]':msg_ = "The airline is required.";break;
		    // case 'sec4[arr_flight]':msg_ = "The flight no is required.";break;
		    // case 'sec4[arr_date]':msg_ = "The date is required.";break;
		    // case 'sec4[dep_airline]':msg_ = "The airline is required.";break;
		    // case 'sec4[dep_flight]':msg_ = "The flight no is required.";break;
		    // case 'sec4[dep_date]':msg_ = "The date is required.";break;
		    // case 'sec1[t_size]':msg_ = "The shirt size is required.";break;
		    // //case 'sec1[position]':msg_ = "The position is required.";break;
		// }
	    // $(this).rules('add', {
		        // required: true,
		        // number: number_,
		        // messages: {
		            // required:  msg_,
		        // }
		    // });
	// });
	

    // Reset form
    $('#reset').on('click', function() {
        validator.resetForm();
    });
	
	$('.activ1').change(function() {
        $('.size_shirt_wrap').show();
        //$('.t-sizex').addClass('required');
    });
    $('.activ2').change(function() {
        $('.size_shirt_wrap').hide();
        //$('.t-sizex').removeClass('required');
    });
    
	// $('#confirm_information').click(function(){
		// var r = $('#form-password').val();
		// if(r == '' || r == null){
			// $('#form-password').css('border','1px solid red');
			// $('#warn_err').html('The password is required.');
		// }else{
			// if(r.length < 6){
				// $('#form-password').css('border','1px solid red');
				// $('#warn_err').html('Please enter the password at least 6 characters.');
			// }else{
				// $('#assacode').val(r);
				// assa = true;
				// $('#submit-form').click();
			// }
		// }
	// });
	
	
    
    function chk_mail(){
    	var url = $('body').attr('url-data');
    	var newaccess = {
			email:$('.email_form').val()
		};
				
    	$.post(url+'/access_check', {data: newaccess}, function(result){
	       	if(result){
	       		var rs = JSON.parse(result);
	       		console.log(rs);
	       		if(rs == '104'){
	       			
	       			$('#modal_form_inline').modal('hide');
	       			var r = $('#form-password').val();
		        	$('#code_ps').val(r);
					assa = true;
					$('#form_data').submit();
	       			//$('body').addClass('modal-open');
	       			// bootbox.confirm({
	       				// //backdrop ,
					    // message: "ยืนยันการอัพเดตข้อมูล",
					    // buttons: {
					        // confirm: {
					            // label: 'ยืนยัน',
					        // },
					        // cancel: {
					            // label: 'ยกเลิก',
					            // className: 'btn-danger'
					        // }
					    // },
					    // callback: function (result) {
					        // if(result){
					        	// var r = $('#form-password').val();
					        	// $('#code_ps').val(r);
								// assa = true;
								// $('#submit-form').click();
			        		// }else{
			        			// $('body').css('overflow','auto');
			        		// }
					    // }
// 	       				 
		        		// // if(result){
							// // var r = $('#form-password').val();
							// // $('#code_ps').val(r);
							// // assa = true;
							// // $('#submit-form').click();
		        		// // }
		        	// });
	       		}else if(rs == '102'){
	       			$('#modal_form_inline').modal('hide');
	       			bootbox.alert("An account with this email already exists.", function(){ 
			           $('.email_form').focus();
			           $('.email_form').addClass('required error-fill');
			           $('body').css('padding-right','0');
			       });
	       		}else if(rs == '105'){
	       			window.location.replace(url+'/login');
	       		}else if(rs == '103'){
	       			$('#modal_form_inline').modal('hide');
	       			bootbox.confirm("Are you sure you want to change the email?", function(result){ 
		        		if(result){
		        			var r = $('#form-password').val();
							$('#code_ps').val(r);
							assa = true;
							$('#submit-form').click();
		        		}
		        	});
	       		}else{
	       			bootbox.alert("Found Error.", function(){ 
			            $('.email_form').focus();
			       });
	       		}
	       	}else{
	       		$('#modal_form_inline').modal('hide');
	       		var r = $('#form-password').val();
	        	$('#code_ps').val(r);
				assa = true;
				$('#form_data').submit();
	       		//$('body').css('overflow','hidden');
	       		// bootbox.confirm({
	       			// backdrop : true,
				    // message: "ยืนยันข้อมูลการลงทะเบียน",
				    // buttons: {
				        // confirm: {
				            // label: 'ยืนยัน',
				        // },
				        // cancel: {
				            // label: 'ยกเลิก',
				            // className: 'btn-danger'
				        // }
				    // },
				    // callback: function (result) {
				        // if(result){
				        	// var r = $('#form-password').val();
				        	// $('#code_ps').val(r);
							// assa = true;
							// $('#submit-form').click();
		        		// }
				    // }
				// });
	       	}
	    });
    }
	
});

function confirmupdate(){
	$('.head-modal-pwd').html('');
	$('.modal-pwd-subtitle').html('For your security, you must re-enter your password to continue.');
	
    $('#form-password').css('border-color','-');
    $('#form-password').css('background','-');
        
	$('#form_mail').val($('.email_form').val());
	$('#modal_form_inline').modal('show');
}
