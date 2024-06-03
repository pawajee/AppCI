function delay(ms) {
  return new Promise(function(resolve, reject){
     setTimeout(resolve,ms);
  });
}

delay(3000).then(() => alert('runs after 3 seconds'));


	// resolve runs the first function in .then
	promise.then(
	  result => alert(result), // shows "done!" after 1 second
	  error => alert(error) // doesn't run
	);

	let promise = new Promise((resolve, reject) => {
	  setTimeout(() => reject(new Error("Whoops!")), 1000);
	});

	// .catch(f) is the same as promise.then(null, f)
	promise.catch(alert); // shows "Error: Whoops!" after 1 second

  // runs when the promise is settled, doesn't matter successfully or not
  .finally(() => stop loading indicator)
  // so the loading indicator is always stopped before we go on
  .then(result => show result, err => show error)

//---------------------------------------------------------

function loadScript(src) {
  return new Promise(function(resolve, reject) {
    let script = document.createElement('script');
    script.src = src;

    script.onload = () => resolve(script);
    script.onerror = () => reject(new Error(`Script load error for ${src}`));

    document.head.append(script);
  });
}
//*********************************************************
let promise = loadScript("https://cdnjs.cloudflare.com/ajax/libs/lodash.js/4.17.11/lodash.js");
promise.then(
  script => alert(`${script.src} is loaded!`),
  error => alert(`Error: ${error.message}`)
);

promise.then(script => alert('Another handler...'));
//---------------------------------------------------------
