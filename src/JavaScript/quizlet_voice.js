if (!window.jQuery)
{
	(function() {
  // Load the script
  const script = document.createElement("script");
  script.src = 'https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js';
  script.type = 'text/javascript';
  script.addEventListener('load', () => {
    console.log(`jQuery ${$.fn.jquery} has been loaded successfully!`);
    // use jQuery below
  });
  document.head.appendChild(script);
})();
	
}
function getAnswer(){
	
	let $=jQuery;
		//var c=$('.UIDiv .SpellQuestionView-inputWrapper'),com=c[0];
	var c=$('.UIDiv .SpellQuestionView-replayAudio'),com=c[0];
	var ReActProp=null;
	for(key in com)
	{
        //console.log(key);
		if(key.startsWith('__reactFiber'))
		{
			ReActProp=com[key];
			break;			
		}
	}
	if(!!ReActProp)
	{
		//console.log(ReActProp.child.memoizedProps.children._owner.memoizedProps.studyStep.metadata.studiableItemId);
		//debugger;
		var studiableItemId=ReActProp.memoizedProps.children._owner.memoizedProps.question.metadata.studiableItemId;
		
		//ReActProp.child.memoizedProps.children._owner.memoizedProps.studyStep.metadata.studiableItemId;
		
		var items=__NEXT_DATA__.props.pageProps.studyModesCommon.studiableDocumentData.studiableItems; //[0].cardSides[0].media[0].plainText
		var item=items.find((item) => item.id== studiableItemId);
		if(!!item)
		{
			return item.cardSides[0].media[0].plainText;	
		}
	}
}
function TTS(text)
{
	var vc="",voice;
		vc="Microsoft Libby Online (Natural) - English (United Kingdom)"
	
	var msg = new SpeechSynthesisUtterance();
	var voice = window.speechSynthesis.getVoices().find( (v)=> v.name==vc );
	if(!voice)
	{
		voice=window.speechSynthesis.getVoices().find( (v)=> v.name=="Microsoft Eva Mobile - English (United States)" );
	}
	//console.log("text to convert:"+text);
	msg.voice=voice;
	msg.text=text;
	speechSynthesis.speak(msg);
}
function onBodyClick(e){
	//console.log("onBodyClick");

	let $=jQuery;

		var ans=getAnswer();
		if(!!ans)
		{
			TTS(ans);	
		}
}

jQuery('body').click((e)=>{
	let $=jQuery;
	let el=$(e.target);
	
	
	/*	debugger;*/
	if((el.hasClass("UIButton-wrapper" ) && el.closest(".SpellQuestionView-replayAudio").length >0) )
	{
		onBodyClick(e);
	}
});
jQuery('body').keydown((e)=>{
	let $=jQuery;
	let el=$(e.target);
	if(e.which ==27 ){//&& (el.hasClass("AutoExpandTextarea-textarea" ) && e.target.tagName=="TEXTAREA")){
		onBodyClick(e);
	}
});

function setObserver()
{
	const div_section = document.querySelector('.SpellViewController');
	const observer = new MutationObserver((mutationsList, observer) => {
	//	console.log("callback that runs when observer is triggered");
		let $=jQuery;
		for(const mutation of mutationsList) {
			if ( mutation.type === 'childList'  ) {
				
			//	console.log('A child node has been added or removed.');
				const nodes = mutation.addedNodes;
				nodes.forEach(node => {
					//node.addEventListener('mouseover', eventMouseOver);
					var rg="";

					//console.log(node );
					if(!(node instanceof String) && $(node).hasClass('SpellViewController-emptyView')){
						
						callTTS();
					}
					else if ($(node).hasClass("SpellCheckpointView"))
					{
						console.log($(node).attr('class'));
						setupObserver();
						
					}
				});
			}
		}
	});
	
	observer.observe(div_section, { 
		childList: true, 
		subtree: true }
	);
}
function callTTS()
{
	clearTimeout(callTTS.__timeout);
	callTTS.__timeout= setTimeout(onBodyClick,500);
	//callTTS.__timeout= setTimeout(setObserver,500);
}
function setupObserver()
{
	setupObserver.__CheckingQuestionView=true;
	if($('.SpellQuestionView').length==0)
	{
		setTimeout(setupObserver,500);
		console.log("setupObserver called");
	}
	else
	{
		setObserver();
	}
}
setObserver();
