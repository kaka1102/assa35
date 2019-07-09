$(document).ready(function(){
	var hr = '00';
	var min = '00';
	//var hr='00';
    var ex_hr = '00';
    //var min = '00';
    var c_hr=false;
    var c_min=false;
    var datasec = '';
    var sec='';
    
    function timeformat(hm){
    	console.log(hm);
		var a = hm.split(':'); // split it at the colons
		var hrx = a[0];
		var minx = a[1];
		//console.log(hrx+' '+minx);
		$(datasec+' .naa_hour td[data-hr="'+hrx+'"]').addClass('naa_active');
		$(datasec+' .naa_minute td[data-min="'+minx+'"]').addClass('naa_active');
		
    }
	$('.naa_time_input').click(function(){
		c_hr=false;
		c_min=false;
		exsec = datasec;
		sec = $(this).attr('data');
		datasec  = '.'+sec;
		
		var value = $(this).val();
		console.log(value);
		if($(this).val() !== ''){
			timeformat(value);
		}else{
			var now = new Date();
			var hrv = now.getHours();
			var minv = now.getMinutes();
			//timeformat(hrv+":"+minv);
		}
		
		if(exsec !== datasec && exsec !== ''){
			$(exsec+'.naa_time').addClass('hide');
		}
		$(datasec).removeClass('hide');
		
	});
	
	$(datasec+' .naa_hour td.naa_time_picker').click(function(){
		c_hr = true;
		hr = $(this).attr('data-hr');
		$(datasec+' .naa_hour td').removeClass('naa_active');
		$(datasec+' .naa_hour td[data-hr="'+hr+'"]').addClass('naa_active');
		 $('.naa_time_input[data="'+sec+'"]').val(hr+':'+min);
		 if(c_hr && c_min){
           $(datasec+'.naa_time').addClass('hide');
       	}
	});
	
	$(datasec+' .naa_minute td.naa_time_picker').click(function(){
		c_min = true;
		min = $(this).attr('data-min');
		$(datasec+' .naa_minute td').removeClass('naa_active');
		$(datasec+' .naa_minute td[data-min="'+min+'"]').addClass('naa_active');
		 $('.naa_time_input[data="'+sec+'"]').val(hr+':'+min);
		 if(c_hr && c_min){
           $(datasec+'.naa_time').addClass('hide');
       	}
	});
	
	$('html').click(function(e) {
         if($(datasec).hasClass('hide')){
             
         }else{
             var tg = e.target;
             //show time
             if($(tg).hasClass('naa_time_picker')){
                 
             }else{
                 $('.naa_time').addClass('hide');
             }
              
         }
     }); 
});


// $('.naa_hour td.naa_time_picker').click(function(){
	// alert('dd');
	 	// c_hr = true;
     	// ex_hr = hr;
     	// hr = $(this).attr('data-hr');
     	// console.log(datasec);
	   // $(attr+' .naa_hour td[data-hr="'+ex_hr+'"]').removeClass('naa_active');
	   // $(attr+' .naa_hour td[data-hr="'+hr+'"]').addClass('naa_active');
	   // //$('#time1').val(hr+':'+min);
//        
// });