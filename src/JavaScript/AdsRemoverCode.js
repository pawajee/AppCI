window.__CallAtt= -10;
window.offSetDur=0;

//Get website name for custom function call, if custom function exists for a website it will be called and will remove elements specific to that website
var customSite=location.hostname.replace("www.","").replaceAll(".","_");

///////////////////////////////////////<Start loading jQuery>////////////////////////////////////////////////

(function(){
  var newscript = document.createElement('script');
     newscript.type = 'text/javascript';
     newscript.async = true;
     newscript.src = 'https://ajax.googleapis.com/ajax/libs/jquery/1.6.1/jquery.min.js';
  (document.getElementsByTagName('head')[0]||document.getElementsByTagName('body')[0]).appendChild(newscript);
})();
///////////////////////////////////////</Start loading jQuery>////////////////////////////////////////////////

//////////////////////////////////////<Code to remove ads>//////////////////////////////////////////////////////
	window["novelodge_com"]= function(){
		$("#sm-vidazoo-player,.trc_related_container").remove();
	}
	window["letmeread_net"]= function(){
		//$("#sm-vidazoo-player,.trc_related_container").remove();
		$('div[id*="ScriptRoot"]').hide();
	}
	function removeIFrames(){
	  
		//console.log("removing iframe!");
		$("iframe:not('[title]=reCAPTCHA')").remove(); // don't remove google captcha
		$("#raga-taboola, #_pagecontent_ragataboola").remove(); // remove common element in all the websites
		
		//if custom function exists call it
		if(!!window[customSite])
		{
			//calling custom website function
			window[customSite]();
		}
		//keep calling the function, as some website will inject ads agains
		if(__CallAtt > 0) //is it not first call?
		{
			//increase the duration by half second for repeated calls
			offSetDur+=500;
		   setTimeout(function(){removeIFrames();},offSetDur);

		}
		else //if it's a first call
		{
			//first call after 1 second
			__CallAtt++;
			setTimeout(function(){removeIFrames();},1000);

		}
	  // console.log(__CallAtt + " - " + (offSetDur));
		  
	  
		  
	}
	//wait for jquery to get loaded
	function bootStrap()
	{
	  if(!!window.jQuery){ //if jQuery has been loaded then call the code to remove ads
		$=jQuery;
		//console.log("jQuery found!");
		removeIFrames();
		setTimeout(function(){removeIFrames()},1000);
	  }
	  else{ //if jQuery not loaded yet call this function after 1 second again 
		setTimeout(bootStrap,1000);
	  }
	  
	}
//////////////////////////////////////</Code to remove ads>//////////////////////////////////////////////////////



//////////////////////////////////////<Start running the code>///////////////////////////////////////////////////
bootStrap(); //start running the above code
//////////////////////////////////////</Start running the code>//////////////////////////////////////////////////
