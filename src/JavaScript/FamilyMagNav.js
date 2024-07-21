var loadingJQ= false;
function bootStrap(){
	
	
	if(loadingJQ && !window.jQuery)
	{
		console.log('delaying and waiting for jquery to get loaded');
		setTimeout(bootStrap,500);
		return;		
	}
	if(!window.jQuery)
	{
		console.log('loading jquery...');
		loadingJQ=true;
		(function(){
		  var newscript = document.createElement('script');
			 newscript.type = 'text/javascript';
			 newscript.async = true;
			 newscript.src = 'https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js';
		  (document.getElementsByTagName('head')[0]||document.getElementsByTagName('body')[0]).appendChild(newscript);
		})();
		setTimeout(bootStrap,500);
		return;
	}
	console.log('jQuery is loaded');	
	$(document).unbind('keydown').keydown(function(e){
		console.log('keydown');
		if(!!e.keyCode){
			console.log('keydown');
			
		}
		if(e.keyCode==37)
		{
			next();
		}
		else if(e.keyCode==39)
		{
			prev();
			
		}
	});

	function prev(e){
		//	$('#epaper_image').attr('src')
		
		var sel1=$('#epaper_image')[0].src.replace('large','small');
		var sel=$(`.item a img[src$="${sel1}"]`);
		var nxt= sel.closest('.owl-item').prev();
		console.log($('.item a img',nxt)[0].src);
		var imgT=$('.item a img',nxt)[0].src.replace('small','large');
		$('#epaper_image')[0].src=imgT;
		if(!!e) e.preventDefault();
		return false;
	}

	function next(e){
		
			var sel1=$('#epaper_image')[0].src.replace('large','small');
		var sel=$(`.item a img[src$="${sel1}"]`);
		var nxt= sel.closest('.owl-item').next();
		console.log($('.item a img',nxt)[0].src);
		var imgT=$('.item a img',nxt)[0].src.replace('small','large');
		$('#epaper_image')[0].src=imgT;
		if(!!e) e.preventDefault();
		return false;
	}
	$('.prev_div').unbind('click').click(prev);
	$('.next_div').unbind('click').click(next);
}
bootStrap();
